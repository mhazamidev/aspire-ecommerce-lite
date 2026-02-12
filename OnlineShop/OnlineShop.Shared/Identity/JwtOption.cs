namespace OnlineShop.Shared.Identity;

public class JwtOption
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int TokenExpireInSecond { get; set; }
    public int RefreshTokenExpireInSecond { get; set; }
}
