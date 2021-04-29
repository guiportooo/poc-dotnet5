namespace PocDotNet5.Api.V1.Schemas.Requests
{
    using System;

    public record CreateUserRequest(string FirstName, string LastName, string Email, DateTime DateOfBirth);
}