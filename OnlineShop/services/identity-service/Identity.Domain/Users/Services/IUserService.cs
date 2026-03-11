using Identity.Domain.Users.Entities;
using Identity.Domain.Users.Identity;

namespace Identity.Domain.Users.Services;

public interface IUserService
{
    Task<string> GenerateTokenAsync(IdentityUserEntity user, DateTime expireTime);
}
