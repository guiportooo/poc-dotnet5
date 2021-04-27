using System;

namespace PocDotNet5.Api.Models.Responses
{
    public record UserCreated(string FirstName, string LastName, string Email, DateTime BirthDate, bool Active);
}