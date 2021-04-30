namespace PocDotNet5.IntegrationTests
{
    using System;
    using System.IO;
    using Api;
    using Api.Data.EntityFramework;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;

    [SetUpFixture]
    public class TestEnvironment
    {
        public static WebApplicationFactory<Startup> Factory;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                        {
                            //Configure mock services
                        })
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseEnvironment("Test");
                });

            using var migrationsScope = Factory.Services.CreateScope();
            var pocDotNet5Context = migrationsScope.ServiceProvider.GetService<PocDotNet5Context>();

            if (pocDotNet5Context == null) throw new ArgumentNullException(nameof(pocDotNet5Context), "Could not create PocDotNet5Context.");
            
            pocDotNet5Context.Database.EnsureDeleted();
            pocDotNet5Context.Database.Migrate();
            
            // Add initial seed
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => Factory.Dispose();
    }
}