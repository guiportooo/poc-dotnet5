namespace PocDotNet5.Api.V2.Models.Responses
{
    using System;

    public record UserCreated
    {
        public string Name { get; }
        public string Email { get; }
        public DateTime DateOfBirth { get; }

        public UserCreated(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            Name = $"{firstName} {lastName}";
            Email = email;
            DateOfBirth = dateOfBirth;
        }
    }
}