namespace OnlineShop.Identity.Entities;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<RefreshToken> RefreshTokens { get; set; }
}
