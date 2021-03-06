namespace PocDotNet5.Api.V1.Validations
{
    using System;
    using FluentValidation;
    using Schemas.Requests;

    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Please inform the user's first name.")
                .MaximumLength(20)
                .WithMessage("The user's first name cannot have more than 20 characters");
            
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Please inform the user's last name.")
                .MaximumLength(20)
                .WithMessage("The user's last name cannot have more than 20 characters");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Please inform a valid email for the user.");

            RuleFor(x => x.DateOfBirth)
                .LessThanOrEqualTo(DateTime.Today.AddYears(-18))
                .WithMessage("The user must be 18 or older.");
        } 
    }
}