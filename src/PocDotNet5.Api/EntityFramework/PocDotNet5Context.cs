using Microsoft.EntityFrameworkCore;
using PocDotNet5.Api.Domain.Entities;

namespace PocDotNet5.Api.EntityFramework
{
    public class PocDotNet5Context : DbContext
    {
        public DbSet<User> Users { get; set; }

        public PocDotNet5Context(DbContextOptions<PocDotNet5Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder) =>
            builder.ApplyConfigurationsFromAssembly(typeof(PocDotNet5Context).Assembly);
    }
}