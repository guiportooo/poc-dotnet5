namespace PocDotNet5.Api.V2.Schemas.Responses
{
    using System;

    public record UserCreated(string FullName, string Email, DateTime DateOfBirth);
}