namespace PocDotNet5.Api.V1.Schemas.Requests
{
    using System;

    public record CreateUser(string FirstName, string LastName, string Email, DateTime DateOfBirth);
}