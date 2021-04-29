namespace PocDotNet5.UnitTests.Api.V2.Builders
{
    using System;
    using System.Collections.Generic;
    using AutoBogus;
    using PocDotNet5.Api.V2.Schemas.Requests;

    public class CreateUserBuilder : AutoFaker<CreateUser>
    {
        public CreateUserBuilder()
        {
            RuleFor(x => x.FirstName, x => x.Person.FirstName);
            RuleFor(x => x.LastName, x => x.Person.LastName);
            RuleFor(x => x.Email, x => x.Person.Email);
            RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth);
        }

        public CreateUserBuilder WithFirstName(string firstName)
        {
            RuleFor(x => x.FirstName, firstName);
            return this;
        }

        public CreateUserBuilder WithLastName(string lastName)
        {
            RuleFor(x => x.LastName, lastName);
            return this;
        }

        public CreateUserBuilder WithEmail(string email)
        {
            RuleFor(x => x.Email, email);
            return this;
        }

        public CreateUserBuilder WithDateOfBirth(DateTime dateOfBirth)
        {
            RuleFor(x => x.DateOfBirth, dateOfBirth);
            return this;
        }

        public CreateUser One() => Generate();

        public IEnumerable<CreateUser> Many(int count) => Generate(count);
    }
}