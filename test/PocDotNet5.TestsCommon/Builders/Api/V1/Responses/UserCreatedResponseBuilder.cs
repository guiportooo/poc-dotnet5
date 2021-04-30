namespace PocDotNet5.TestsCommon.Builders.Api.V1.Responses
{
    using System;
    using System.Collections.Generic;
    using AutoBogus;
    using PocDotNet5.Api.V1.Schemas.Responses;

    public sealed class UserCreatedResponseBuilder : AutoFaker<UserCreatedResponse>
    {
        public UserCreatedResponseBuilder()
        {
            RuleFor(x => x.FirstName, f => f.Person.FirstName);
            RuleFor(x => x.LastName, f => f.Person.LastName);
            RuleFor(x => x.Email, f => f.Person.Email);
            RuleFor(x => x.DateOfBirth, f => f.Person.DateOfBirth);
            RuleFor(x => x.Active, true);
        }

        public UserCreatedResponseBuilder WithFirstName(string firstName)
        {
            RuleFor(x => x.FirstName, firstName);
            return this;
        }

        public UserCreatedResponseBuilder WithLastName(string lastName)
        {
            RuleFor(x => x.LastName, lastName);
            return this;
        }

        public UserCreatedResponseBuilder WithEmail(string email)
        {
            RuleFor(x => x.Email, email);
            return this;
        }

        public UserCreatedResponseBuilder WithDateOfBirth(DateTime dateOfBirth)
        {
            RuleFor(x => x.DateOfBirth, dateOfBirth);
            return this;
        }

        public UserCreatedResponseBuilder Active()
        {
            RuleFor(x => x.Active, true);
            return this;
        }

        public UserCreatedResponseBuilder Inactive()
        {
            RuleFor(x => x.Active, false);
            return this;
        }

        public UserCreatedResponse One() => Generate();

        public IEnumerable<UserCreatedResponse> Many(int count) => Generate(count);
    }
}