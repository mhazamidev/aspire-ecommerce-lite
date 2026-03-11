using Identity.Domain.Users.Entities;
using Identity.Domain.Users.ValueObjects;
using System.Linq.Expressions;

namespace Identity.Domain.Users.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByUserIdAsync(UserId userId);
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task AddAsync(RefreshToken refreshToken);
    Task UpdateAsync(RefreshToken token, params Expression<Func<RefreshToken, object>>[] modifiedProperties);
}
