using System;

namespace PocDotNet5.Api.Models.Requests
{
    public record CreateUser(string FirstName, string LastName, string Email, DateTime DateOfBirth);
}