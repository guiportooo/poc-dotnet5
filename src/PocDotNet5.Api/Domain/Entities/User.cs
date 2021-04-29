namespace PocDotNet5.Api.Domain.Entities
{
    using System;

    public class User
    {
        private const int minimumAge = 18;

        public User(string firstName,
            string lastName,
            string email,
            DateTime dateOfBirth)
        {
            if (IsYoungerThanMinimumAge(dateOfBirth))
                throw new InvalidOperationException($"The user must be {minimumAge} or older.");

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
            UpdatedAt = DateTime.Now;
            Active = true;
        }

        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime DateOfBirth { get; }
        public DateTime UpdatedAt { get; }
        public bool Active { get; }
        public string FullName => $"{FirstName} {LastName}";

        private static bool IsYoungerThanMinimumAge(DateTime dateOfBirth) =>
            dateOfBirth.Date > DateTime.Today.AddYears(-minimumAge);
    }
}