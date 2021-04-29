namespace PocDotNet5.UnitTests.Api.Domain.Builders.Entities
{
    using System;
    using System.Collections.Generic;
    using AutoBogus;
    using PocDotNet5.Api.Domain.Entities;

    public sealed class UserBuilder : AutoFaker<User>
    {
        private const int MinimumAge = 18;

        public UserBuilder()
        {
            RuleFor(x => x.Id, 0);
            RuleFor(x => x.FirstName, f => f.Person.FirstName);
            RuleFor(x => x.LastName, f => f.Person.LastName);
            RuleFor(x => x.Email, f => f.Person.Email);
            RuleFor(x => x.DateOfBirth, DateTime.Today.AddYears(-MinimumAge));
            RuleFor(x => x.UpdatedAt, DateTime.Now);
            RuleFor(x => x.Active, true);

            CustomInstantiator(f => new User(f.Person.FirstName,
                f.Person.LastName,
                f.Person.Email,
                DateTime.Today.AddYears(-MinimumAge)));
        }

        public UserBuilder WithId(int id)
        {
            RuleFor(x => x.Id, id);
            return this;
        }

        public UserBuilder WithFirstName(string firstName)
        {
            RuleFor(x => x.FirstName, firstName);
            return this;
        }

        public UserBuilder WithLastName(string lastName)
        {
            RuleFor(x => x.LastName, lastName);
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            RuleFor(x => x.Email, email);
            return this;
        }

        public UserBuilder WithDateOfBirth(DateTime dateOfBirth)
        {
            RuleFor(x => x.DateOfBirth, dateOfBirth);
            return this;
        }

        public UserBuilder UpdatedAt(DateTime updatedAt)
        {
            RuleFor(x => x.UpdatedAt, updatedAt);
            return this;
        }

        public UserBuilder Active()
        {
            RuleFor(x => x.Active, true);
            return this;
        }

        public UserBuilder Inactive()
        {
            RuleFor(x => x.Active, false);
            return this;
        }

        public User One() => Generate();

        public IEnumerable<User> Many(int count) => Generate(count);
    }
}