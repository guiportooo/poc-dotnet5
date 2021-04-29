namespace PocDotNet5.UnitTests.Api.Domain.Queries
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Builders.Entities;
    using Builders.Queries;
    using FakeItEasy;
    using FakeItEasy.AutoFake;
    using FluentAssertions;
    using NUnit.Framework;
    using PocDotNet5.Api.Domain.Entities;
    using PocDotNet5.Api.Domain.Mappings;
    using PocDotNet5.Api.Domain.Queries;
    using PocDotNet5.Api.Domain.Repositories;

    public class GetUserQueryTests
    {
        private AutoFaker _faker;
        private GetUserQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(config => config.AddProfile(new UserMappings()));
            var mapper = mapperConfig.CreateMapper();
            _faker = new AutoFaker(config => config.Use(mapper));
            _handler = _faker.CreateInstance<GetUserQueryHandler>();
        }

        [Test]
        public async Task Should_return_query_result()
        {
            const int id = 123;
            const string firstName = "FirstName";
            const string lastName = "LastName";
            const string fullName = "FirstName LastName";
            const string email = "valid@email.com";
            var dateOfBirth = DateTime.Today.AddYears(-30);
        
            var user = new UserBuilder()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithEmail(email)
                .WithDateOfBirth(dateOfBirth)
                .Active()
                .One();
        
            var userRepository =  _faker.Get<IUserRepository>();
            A.CallTo(() => userRepository.FindAsync(id)).Returns(Task.FromResult(user));
            
            var expectedUserQueryResult = new UserQueryResultBuilder()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithFullName(fullName)
                .WithEmail(email)
                .WithDateOfBirth(dateOfBirth)
                .Active()
                .One();
        
            var query = new GetUserQuery(id);
        
            var returnedResult = await _handler.Handle(query, CancellationToken.None);
            
            returnedResult.Should().BeEquivalentTo(expectedUserQueryResult);
        }
    }
}