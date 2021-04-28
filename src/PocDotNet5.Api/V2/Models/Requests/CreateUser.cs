namespace PocDotNet5.Api.V2.Models.Requests
{
    using System;

    public record CreateUser(string FirstName, string LastName, string Email, DateTime DateOfBirth);
}