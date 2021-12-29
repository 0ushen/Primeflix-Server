using Primeflix.Application.Common.Interfaces;
using Primeflix.Infrastructure.Identity;
using Primeflix.Infrastructure.Persistence;
using Primeflix.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Primeflix.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("PrimeflixDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IDomainEventService, DomainEventService>();

        services.AddTransient<IIdentityService, IdentityService>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                config.Authority = "https://localhost:6001";
                config.Audience = "PrimeflixApi";
                config.RequireHttpsMetadata = false;
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiReader", policy => policy.RequireClaim("scope", "api.read"));
        });

        return services;
    }
}
