namespace PocDotNet5.IntegrationTests.Specs.Api.V1.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;
    using TestsCommon.Builders.Api.Domain.Entities;
    using TestsCommon.Builders.Api.V1;
    using TestsCommon.Builders.Api.V1.Requests;

    public class UsersControllerTests : ApiTest
    {
        private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

        [Test]
        public async Task Should_return_not_found_when_user_with_id_does_not_exist()
        {
            const int id = 999;
            var response = await _httpClient.GetAsync($"api/v1/users/{id}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task Should_return_ok_with_user_when_user_with_id_exists()
        {
            const string firstName = "FirstName";
            const string lastName = "LastName";
            const string email = "valid@email.com";
            var dateOfBirth = DateTime.Today.AddYears(-30);

            var user = new UserBuilder()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithEmail(email)
                .WithDateOfBirth(dateOfBirth)
                .Active()
                .One();

            _pocDotNet5Context.Add(user);
            await _pocDotNet5Context.SaveChangesAsync();

            var response = await _httpClient.GetAsync($"api/v1/users/{user.Id}");

            var userCreatedResponse = JToken.Parse(await response.Content.ReadAsStringAsync());

            var expectedUserCreatedResponse = JToken.Parse($@"
            {{
                'firstName': '{firstName}',
                'lastName': '{lastName}',
                'email': '{email}',
                'dateOfBirth': '{dateOfBirth}',
                'active': true
            }}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            userCreatedResponse.Should().BeEquivalentTo(expectedUserCreatedResponse);
        }

        [Test]
        public async Task Should_return_unprocessable_entity_when_trying_to_create_invalid_user()
        {
            var fiveYearsAgo = DateTime.Today.AddYears(-5);

            var createUserRequest =
                $"{{\"firstName\":\"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\",\"email\":\"@invalid.email\",\"dateOfBirth\":\"{fiveYearsAgo:yyyy-MM-ddThh:mm:ss.fffZ}\"}}";

            var response = await _httpClient.PostAsync("api/v1/users",
                new StringContent(createUserRequest, Encoding.UTF8, "application/json"));

            var validationErrorsResponse = JToken.Parse(await response.Content.ReadAsStringAsync());

            var expectedValidationErrorsResponse = JToken.Parse(@"
            {
                'errors': [
                    {
                        'field': 'Email',
                        'message': 'Please inform a valid email for the user.'
                    },
                    {
                        'field': 'LastName',
                        'message': 'The LastName field is required.'
                    },
                    {
                        'field': 'LastName',
                        'message': 'Please inform the user\'s last name.'
                    },
                    {
                        'field': 'FirstName',
                        'message': 'The user\'s first name cannot have more than 20 characters'
                    },
                    {
                        'field': 'DateOfBirth',
                        'message': 'The user must be 18 or older.'
                    }
                ]
            }");

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            validationErrorsResponse.Should().BeEquivalentTo(expectedValidationErrorsResponse);
        }

        [Test]
        public async Task Should_create_valid_user_and_return_created()
        {
            const string firstName = "FirstName";
            const string lastName = "LastName";
            const string email = "valid@email.com";
            var dateOfBirth = DateTime.Today.AddYears(-30);

            var createUserRequest = new CreateUserRequestBuilder()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithEmail(email)
                .WithDateOfBirth(dateOfBirth)
                .One();

            var json = JsonSerializer.Serialize(createUserRequest);

            var response = await _httpClient.PostAsync("api/v1/users",
                new StringContent(json, Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdUser = await _pocDotNet5Context.Users.FirstOrDefaultAsync();

            var expectedCreatedUser = new UserBuilder()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithEmail(email)
                .WithDateOfBirth(dateOfBirth)
                .Active()
                .One();

            createdUser.Should().BeEquivalentTo(expectedCreatedUser, opt =>
                opt
                    .Excluding(x => x.Id)
                    .Excluding(x => x.UpdatedAt));
        }
    }
}