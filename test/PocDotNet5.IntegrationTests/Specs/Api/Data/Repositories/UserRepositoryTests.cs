namespace PocDotNet5.IntegrationTests.Specs.Api.Data.Repositories
{
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using PocDotNet5.Api.Domain.Repositories;
    using TestsCommon.Builders.Api.Domain.Entities;

    public class UserRepositoryTests : DatabaseTest
    {
        [Test]
        public async Task Should_find_user_by_id()
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

            var userRepository = GetService<IUserRepository>();

            var returnedUser = await userRepository.FindAsync(user.Id);

            returnedUser.Should().BeEquivalentTo(user, cfg => cfg
                .Excluding(x => x.Id)
                .Excluding(x => x.UpdatedAt));
        }

        [Test]
        public async Task Should_add_user()
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

            var userRepository = GetService<IUserRepository>();
            await userRepository.AddAsync(user);

            var createdUser = await _pocDotNet5Context.Users.FirstOrDefaultAsync();

            createdUser.Should().BeEquivalentTo(user, cfg => cfg
                .Excluding(x => x.Id)
                .Excluding(x => x.UpdatedAt));
        }
    }
}