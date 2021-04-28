namespace PocDotNet5.Api.Domain.Entities
{
    using System;

    public class User
    {
        public int Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public DateTime DateOfBirth { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public bool Active { get; protected set; }

        public User(string firstName,
            string lastName,
            string email,
            DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
            UpdatedAt = DateTime.Now;
            Active = true;
        }
    }
}