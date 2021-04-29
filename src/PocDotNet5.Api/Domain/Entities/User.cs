namespace PocDotNet5.Api.Domain.Entities
{
    using System;

    public class User
    {
        private const int MinimumAge = 18;

        public User(string firstName,
            string lastName,
            string email,
            DateTime dateOfBirth)
        {
            if (IsYoungerThanMinimumAge(dateOfBirth))
                throw new InvalidOperationException($"The user must be {MinimumAge} or older.");

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
            UpdatedAt = DateTime.Now;
            Active = true;
        }

        public int Id { get; set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set;}
        public string Email { get; protected set;}
        public DateTime DateOfBirth { get; protected set;}
        public DateTime UpdatedAt { get; protected set;}
        public bool Active { get; protected set;}
        public string FullName => $"{FirstName} {LastName}";

        private static bool IsYoungerThanMinimumAge(DateTime dateOfBirth) =>
            dateOfBirth.Date > DateTime.Today.AddYears(-MinimumAge);
    }
}