using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Users.Identity;

public class IdentityUserEntity : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
}
