namespace PocDotNet5.UnitTests.Specs.Api.Domain.Entities
{
    using System;
    using FluentAssertions;
    using NUnit.Framework;
    using PocDotNet5.Api.Domain.Entities;

    public class UserTests
    {
        private const int minimumAge = 18;

        [TestCase(17)]
        [TestCase(16)]
        public void Should_not_create_user_younger_than_minimumAge(int age)
        {
            const string firstName = "FirstName";
            const string lastName = "LastName";
            const string email = "valid@email.com";
            var dateOfBirth = DateTime.Now.AddYears(-age);

            Action createUser = () => new User(firstName, lastName, email, dateOfBirth);

            var expectedErrorMessage = $"The user must be {minimumAge} or older.";

            createUser.Should().Throw<InvalidOperationException>().WithMessage(expectedErrorMessage);
        }

        [Test]
        public void Should_create_user()
        {
            const string firstName = "FirstName";
            const string lastName = "LastName";
            const string email = "valid@email.com";
            var dateOfBirth = DateTime.Now.AddYears(-18);

            var user = new User(firstName, lastName, email, dateOfBirth);

            user.FirstName.Should().Be(firstName);
            user.LastName.Should().Be(lastName);
            user.Email.Should().Be(email);
            user.DateOfBirth.Should().Be(dateOfBirth);
            user.UpdatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
            user.Active.Should().BeTrue();
        }

        [Test]
        public void Should_return_full_name()
        {
            const string firstName = "FirstName";
            const string lastName = "LastName";
            const string email = "valid@email.com";
            var dateOfBirth = DateTime.Now.AddYears(-18);

            var user = new User(firstName, lastName, email, dateOfBirth);

            const string expectedFullName = "FirstName LastName";

            user.FullName.Should().Be(expectedFullName);
        }
    }
}