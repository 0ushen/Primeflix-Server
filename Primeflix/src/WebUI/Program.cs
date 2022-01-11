using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Primeflix.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Primeflix.Application.Common.Interfaces;

namespace Primeflix.WebUI;

public class Program
{
    public async static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                var seederService = services.GetRequiredService<ISeederService>();

                if (context.Database.IsSqlServer())
                {
                    context.Database.Migrate();
                }

                await ApplicationDbContextSeed.SeedSampleDataAsync(seederService);
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                throw;
            }
        }

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                if (!context.HostingEnvironment.IsProduction())
                    return;

                var builtConfig = config.Build();
                var secretClient = new SecretClient(
                    new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                    new DefaultAzureCredential());
                config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
            })
            .ConfigureWebHostDefaults(webBuilder => 
                webBuilder.UseStartup<Startup>());
}
