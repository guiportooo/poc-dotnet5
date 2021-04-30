namespace PocDotNet5.TestsCommon.Builders.Api.Domain.Commands
{
    using System;
    using System.Collections.Generic;
    using AutoBogus;
    using PocDotNet5.Api.Domain.Commands;

    public sealed class CreateUserCommandBuilder : AutoFaker<CreateUserCommand>
    {
        private const int MinimumAge = 18;

        public CreateUserCommandBuilder()
        {
            RuleFor(x => x.FirstName, f => f.Person.FirstName);
            RuleFor(x => x.LastName, f => f.Person.LastName);
            RuleFor(x => x.Email, f => f.Person.Email);
            RuleFor(x => x.DateOfBirth, DateTime.Today.AddYears(-MinimumAge));

            CustomInstantiator(f => new CreateUserCommand(f.Person.FirstName,
                f.Person.LastName,
                f.Person.Email,
                DateTime.Today.AddYears(-MinimumAge)));
        }

        public CreateUserCommandBuilder WithFirstName(string firstName)
        {
            RuleFor(x => x.FirstName, firstName);
            return this;
        }

        public CreateUserCommandBuilder WithLastName(string lastName)
        {
            RuleFor(x => x.LastName, lastName);
            return this;
        }

        public CreateUserCommandBuilder WithEmail(string email)
        {
            RuleFor(x => x.Email, email);
            return this;
        }

        public CreateUserCommandBuilder WithDateOfBirth(DateTime dateOfBirth)
        {
            RuleFor(x => x.DateOfBirth, dateOfBirth);
            return this;
        }

        public CreateUserCommand One() => Generate();

        public IEnumerable<CreateUserCommand> Many(int count) => Generate(count);
    }
}