using OnlineShop.Identity.Entities;

namespace OnlineShop.Identity.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(d => d.FirstName)
               .HasMaxLength(50);

        builder.Property(d => d.LastName)
               .HasMaxLength(50);
    }
}
