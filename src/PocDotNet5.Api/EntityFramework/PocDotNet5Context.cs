namespace PocDotNet5.Api.EntityFramework
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class PocDotNet5Context : DbContext
    {
        public DbSet<User> Users => Set<User>();

        public PocDotNet5Context(DbContextOptions<PocDotNet5Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder) =>
            builder.ApplyConfigurationsFromAssembly(typeof(PocDotNet5Context).Assembly);
    }
}