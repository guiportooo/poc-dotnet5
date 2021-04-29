namespace PocDotNet5.Api.V2.Schemas.Requests
{
    using System;

    public record CreateUserRequest(string FirstName, string LastName, string Email, DateTime DateOfBirth);
}