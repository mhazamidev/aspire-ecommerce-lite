using Domain.SeekWork;
using Identity.Domain.Users.Repositories;

namespace Identity.Infrastructure.Persistence.UOW;

public interface IIdentityUnitofWork : IUnitOfWork
{
    public IUserRepository Users { get; }
    public IRefreshTokenRepository RefreshTokens { get; }
}
