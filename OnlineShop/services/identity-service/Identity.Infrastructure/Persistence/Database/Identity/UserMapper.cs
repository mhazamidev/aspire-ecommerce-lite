using Identity.Domain.Users.Entities;
using Identity.Domain.Users.Identity;
using Identity.Domain.Users.ValueObjects;

namespace Identity.Infrastructure.Persistence.Database.Identity;

public static class UserMapper
{
    public static IdentityUserEntity ToIdentity(User user)
    {
        return new IdentityUserEntity
        {
            Id = user.Id.Value,
            Email = user.Email.Value,
            UserName = user.UserName,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            IsActive = user.IsActive
        };
    }

    public static User ToDomain(IdentityUserEntity user)
    {
        return User.Map(new UserId(user.Id), user.UserName!, user.FirstName, user.LastName, user.Email!, user.IsActive);
    }
}
