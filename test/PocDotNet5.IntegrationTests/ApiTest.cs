namespace PocDotNet5.IntegrationTests
{
    using System.Net.Http;
    using NUnit.Framework;

    public class ApiTest : DatabaseTest
    {
        protected HttpClient _httpClient;

        [SetUp]
        public void SetUpHttpClient() => _httpClient = TestEnvironment.Factory.CreateClient();
    }
}