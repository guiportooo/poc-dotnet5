namespace PocDotNet5.TestsCommon.Builders.Api.Domain.Queries
{
    using System;
    using System.Collections.Generic;
    using AutoBogus;
    using PocDotNet5.Api.Domain.Queries;

    public sealed class UserQueryResultBuilder : AutoFaker<UserQueryResult>
    {
        private const int MinimumAge = 18;

        public UserQueryResultBuilder()
        {
            RuleFor(x => x.FirstName, f => f.Person.FirstName);
            RuleFor(x => x.LastName, f => f.Person.LastName);
            RuleFor(x => x.FullName, f => f.Person.FullName);
            RuleFor(x => x.Email, f => f.Person.Email);
            RuleFor(x => x.DateOfBirth, DateTime.Today.AddYears(-MinimumAge));
            RuleFor(x => x.Active, true);
        }

        public UserQueryResultBuilder WithFirstName(string firstName)
        {
            RuleFor(x => x.FirstName, firstName);
            return this;
        }

        public UserQueryResultBuilder WithLastName(string lastName)
        {
            RuleFor(x => x.LastName, lastName);
            return this;
        }

        public UserQueryResultBuilder WithFullName(string fullName)
        {
            RuleFor(x => x.FullName, fullName);
            return this;
        }
        
        public UserQueryResultBuilder WithEmail(string email)
        {
            RuleFor(x => x.Email, email);
            return this;
        }

        public UserQueryResultBuilder WithDateOfBirth(DateTime dateOfBirth)
        {
            RuleFor(x => x.DateOfBirth, dateOfBirth);
            return this;
        }

        public UserQueryResultBuilder Active()
        {
            RuleFor(x => x.Active, true);
            return this;
        }

        public UserQueryResultBuilder Inactive()
        {
            RuleFor(x => x.Active, false);
            return this;
        }

        public UserQueryResult One() => Generate();

        public IEnumerable<UserQueryResult> Many(int count) => Generate(count);
    }
}