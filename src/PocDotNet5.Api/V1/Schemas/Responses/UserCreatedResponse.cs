namespace PocDotNet5.Api.V1.Schemas.Responses
{
    using System;

    public record UserCreatedResponse(string FirstName, string LastName, string Email, DateTime DateOfBirth, bool Active);
}