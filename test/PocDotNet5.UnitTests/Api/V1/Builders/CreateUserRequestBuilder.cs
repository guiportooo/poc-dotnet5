namespace PocDotNet5.UnitTests.Api.V1.Builders
{
    using System;
    using System.Collections.Generic;
    using AutoBogus;
    using PocDotNet5.Api.V1.Schemas.Requests;

    public class CreateUserRequestBuilder : AutoFaker<CreateUserRequest>
    {
        public CreateUserRequestBuilder()
        {
            RuleFor(x => x.FirstName, x => x.Person.FirstName);
            RuleFor(x => x.LastName, x => x.Person.LastName);
            RuleFor(x => x.Email, x => x.Person.Email);
            RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth);
        }

        public CreateUserRequestBuilder WithFirstName(string firstName)
        {
            RuleFor(x => x.FirstName, firstName);
            return this;
        }

        public CreateUserRequestBuilder WithLastName(string lastName)
        {
            RuleFor(x => x.LastName, lastName);
            return this;
        }

        public CreateUserRequestBuilder WithEmail(string email)
        {
            RuleFor(x => x.Email, email);
            return this;
        }

        public CreateUserRequestBuilder WithDateOfBirth(DateTime dateOfBirth)
        {
            RuleFor(x => x.DateOfBirth, dateOfBirth);
            return this;
        }

        public CreateUserRequest One() => Generate();

        public IEnumerable<CreateUserRequest> Many(int count) => Generate(count);
    }
}