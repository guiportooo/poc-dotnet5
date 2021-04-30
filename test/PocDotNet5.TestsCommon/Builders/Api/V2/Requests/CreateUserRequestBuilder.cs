namespace PocDotNet5.TestsCommon.Builders.Api.V2.Requests
{
    using System;
    using System.Collections.Generic;
    using AutoBogus;
    using PocDotNet5.Api.V2.Schemas.Requests;

    public sealed class CreateUserRequestBuilder : AutoFaker<CreateUserRequest>
    {
        public CreateUserRequestBuilder()
        {
            RuleFor(x => x.FirstName, f => f.Person.FirstName);
            RuleFor(x => x.LastName, f => f.Person.LastName);
            RuleFor(x => x.Email, f => f.Person.Email);
            RuleFor(x => x.DateOfBirth, f => f.Person.DateOfBirth);
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