using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasConversion(
            userId => userId.Value,
            value => new UserId(value));

        builder.Property(u => u.Name)
            .HasConversion(
                name => name.Value,
                value => Name.Create(value)!)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value)!)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(u => u.Youtube)
            .HasConversion(
                youtube => youtube.Value,
                value => Youtube.Create(value)!)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(u => u.Linkedin)
            .HasConversion(
                linkedin => linkedin.Value,
                value => Linkedin.Create(value)!)
            .HasMaxLength(512)
            .IsRequired();

        builder.HasIndex(c => c.Email)
            .IsUnique();
    }
}
