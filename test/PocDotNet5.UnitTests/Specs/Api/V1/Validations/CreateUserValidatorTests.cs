namespace PocDotNet5.UnitTests.Specs.Api.V1.Validations
{
    using System;
    using FluentValidation;
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using PocDotNet5.Api.V1.Validations;
    using TestsCommon.Builders.Api.V1;
    using TestsCommon.Builders.Api.V1.Requests;

    public class CreateUserValidatorTests
    {
        private CreateUserRequestValidator _requestValidator;

        [SetUp]
        public void Setup() => _requestValidator = new CreateUserRequestValidator();

        [TestCase("")]
        [TestCase(null)]
        public void Should_have_error_when_user_does_not_have_first_name(string emptyFirstName)
        {
            const string errorMessage = "Please inform the user's first name.";

            var request = new CreateUserRequestBuilder()
                .WithFirstName(emptyFirstName)
                .One();

            _requestValidator
                .TestValidate(request)
                .ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage(errorMessage)
                .WithSeverity(Severity.Error);
        }

        [TestCase("aaaaaaaaaaaaaaaaaaaaa")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Should_have_error_when_users_first_name_has_more_than_20_characters(string longFirstName)
        {
            const string errorMessage = "The user's first name cannot have more than 20 characters";

            var request = new CreateUserRequestBuilder()
                .WithFirstName(longFirstName)
                .One();

            _requestValidator
                .TestValidate(request)
                .ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage(errorMessage)
                .WithSeverity(Severity.Error);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Should_have_error_when_user_does_not_have_last_name(string emptyLastName)
        {
            const string errorMessage = "Please inform the user's last name.";

            var request = new CreateUserRequestBuilder()
                .WithLastName(emptyLastName)
                .One();

            _requestValidator
                .TestValidate(request)
                .ShouldHaveValidationErrorFor(x => x.LastName)
                .WithErrorMessage(errorMessage)
                .WithSeverity(Severity.Error);
        }

        [TestCase("aaaaaaaaaaaaaaaaaaaaa")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Should_have_error_when_users_last_name_has_more_than_20_characters(string longLastName)
        {
            const string errorMessage = "The user's last name cannot have more than 20 characters";

            var request = new CreateUserRequestBuilder()
                .WithLastName(longLastName)
                .One();

            _requestValidator
                .TestValidate(request)
                .ShouldHaveValidationErrorFor(x => x.LastName)
                .WithErrorMessage(errorMessage)
                .WithSeverity(Severity.Error);
        }

        [TestCase("invalidemail")]
        [TestCase("invalid.email")]
        [TestCase("invalid@@email.com")]
        [TestCase("@invalid@email.com")]
        public void Should_have_error_when_user_does_not_have_a_valid_email(string invalidEmail)
        {
            const string errorMessage = "Please inform a valid email for the user.";

            var request = new CreateUserRequestBuilder()
                .WithEmail(invalidEmail)
                .One();

            _requestValidator
                .TestValidate(request)
                .ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage(errorMessage)
                .WithSeverity(Severity.Error);
        }

        [Test]
        public void Should_have_error_when_user_is_younger_than_18()
        {
            var dateOfBirth = DateTime.Today.AddYears(-17);
            const string errorMessage = "The user must be 18 or older.";

            var request = new CreateUserRequestBuilder()
                .WithDateOfBirth(dateOfBirth)
                .One();

            _requestValidator
                .TestValidate(request)
                .ShouldHaveValidationErrorFor(x => x.DateOfBirth)
                .WithErrorMessage(errorMessage)
                .WithSeverity(Severity.Error);
        }

        [Test]
        public void Should_have_no_errors_when_user_is_valid()
        {
            const string firstName = "FirstName";
            const string lastName = "LastName";
            const string email = "valid@email.com";
            var dateOfBirth = DateTime.Today.AddYears(-18);

            var request = new CreateUserRequestBuilder()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithEmail(email)
                .WithDateOfBirth(dateOfBirth)
                .One();

            _requestValidator
                .TestValidate(request)
                .ShouldNotHaveAnyValidationErrors();
        }
    }
}