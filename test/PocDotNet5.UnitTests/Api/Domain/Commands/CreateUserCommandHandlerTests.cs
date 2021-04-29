namespace PocDotNet5.UnitTests.Api.Domain.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Builders.Commands;
    using FakeItEasy;
    using FakeItEasy.AutoFake;
    using FluentAssertions;
    using NUnit.Framework;
    using PocDotNet5.Api.Domain.Commands;
    using PocDotNet5.Api.Domain.Entities;
    using PocDotNet5.Api.Domain.Mappings;
    using PocDotNet5.Api.Domain.Repositories;

    public class CreateUserCommandHandlerTests
    {
        private AutoFaker _faker;
        private CreateUserCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(config => config.AddProfile(new UserMappings()));
            var mapper = mapperConfig.CreateMapper();
            _faker = new AutoFaker(config => config.Use(mapper));
            _handler = _faker.CreateInstance<CreateUserCommandHandler>();
        }

        [Test]
        public async Task Should_create_user_and_return_id()
        {
            const int id = 123;
            var command = new CreateUserCommandBuilder().One();

            var userRepository = _faker.Get<IUserRepository>();
            A.CallTo(() => userRepository.AddAsync(A<User>.Ignored)).Invokes((User user) => user.Id = id);
            
            var createdId = await _handler.Handle(command, CancellationToken.None);
            
            createdId.Should().Be(id);
        }
    }
}