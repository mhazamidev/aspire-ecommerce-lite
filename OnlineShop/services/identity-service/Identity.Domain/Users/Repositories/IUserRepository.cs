using Identity.Domain.Users.Entities;
using Identity.Domain.Users.Identity;
using System.Linq.Expressions;

namespace Identity.Domain.Users.Repositories;

public interface IUserRepository
{
    Task RegisterAsync(IdentityUserEntity user, string password);
    Task<IdentityUserEntity?> LoginAsync(string username, string password);
    Task UpdateAsync(IdentityUserEntity user, params Expression<Func<IdentityUserEntity, object>>[] modifiedProperties);
}
