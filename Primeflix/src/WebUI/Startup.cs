using Primeflix.Application;
using Primeflix.Application.Common.Interfaces;
using Primeflix.Infrastructure;
using Primeflix.Infrastructure.Persistence;
using Primeflix.WebUI.Filters;
using Primeflix.WebUI.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace Primeflix.WebUI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.AddInfrastructure(Configuration);

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllersWithViews(options => options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services.AddOpenApiDocument(configure =>
        {
            configure.Title = "Primeflix API";
            configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}."
            });

            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });

        services.AddCors(options =>
        {
            options.AddPolicy("AngularClient",
                builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()   
                           .WithOrigins("http://localhost:4200");
                });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHealthChecks("/health");

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseSwaggerUi3(settings =>
        {
            settings.Path = "/api";
            settings.DocumentPath = "/api/specification.json";
        });

        app.UseRouting();
        app.UseCors("AngularClient");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");
        });
    }
}
