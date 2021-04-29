namespace PocDotNet5.Api.Data.EntityFramework.Mappings
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(x => x.Email)
                .IsRequired();

            builder
                .Property(x => x.DateOfBirth)
                .IsRequired();
            
            builder
                .Property(x => x.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("getdate()");
            
            builder
                .Property(x => x.Active)
                .IsRequired();
        }
    }
}