using OnlineShop.Identity.Entities;

namespace OnlineShop.Identity.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable(nameof(RefreshToken));

        builder.HasKey(x => x.Id);

        builder.HasOne(d => d.User)
         .WithMany(d => d.RefreshTokens)
         .HasForeignKey(d => d.UserId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}
