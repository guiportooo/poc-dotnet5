namespace PocDotNet5.IntegrationTests
{
    using System.Threading.Tasks;
    using Api.Data.EntityFramework;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using Respawn;

    public class DatabaseTest
    {
        private static readonly Checkpoint _databaseReset = new()
        {
            TablesToIgnore = new[] {"__EFMigrations"}
        };

        protected static IServiceScope _scope;
        protected static PocDotNet5Context _pocDotNet5Context;
        protected static T GetService<T>() => _scope.ServiceProvider.GetService<T>();

        [SetUp]
        public async Task SetUpScope()
        {
            _scope = TestEnvironment
                .Factory
                .Services
                .CreateScope();

            _pocDotNet5Context = TestEnvironment
                .Factory
                .Services
                .CreateScope()
                .ServiceProvider
                .GetService<PocDotNet5Context>();

            var configuration = (ConfigurationRoot) TestEnvironment.Factory.Services.GetService(typeof(IConfiguration));
            var connectionString = configuration.GetConnectionString("PocDotNet5");
            await _databaseReset.Reset(connectionString);
        }

        [TearDown]
        public void TearDownScope() => _scope.Dispose();
    }
}