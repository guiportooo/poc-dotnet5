namespace PocDotNet5.Api.V1.Models.Requests
{
    using System;

    public record CreateUser(string FirstName, string LastName, string Email, DateTime DateOfBirth);
}