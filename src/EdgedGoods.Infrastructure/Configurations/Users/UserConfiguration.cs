using EdgedGoods.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdgedGoods.Infrastructure.Configurations.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    private const int MaxPhoneNumberLength = 11;
    private const int MaxNameLength = 50;
    private const int MaxEmailLength = 50;
    
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.Property(x => x.Id)
            .HasConversion(
                v => v.Value.ToString(),
                v => new UserId(Guid.Parse(v)));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(MaxPhoneNumberLength);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(MaxNameLength);

        builder.Property(x => x.Email)
            .HasMaxLength(MaxEmailLength);
        
        builder.Property(x => x.Password)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(MaxNameLength);

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId.Value)
            .HasPrincipalKey(x => x.Id.Value);
        
        builder.HasIndex(x => x.PhoneNumber);
    }
}