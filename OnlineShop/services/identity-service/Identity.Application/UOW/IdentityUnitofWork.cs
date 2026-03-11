using Identity.Domain.Users.Identity;
using Identity.Domain.Users.Repositories;
using Identity.Infrastructure.Persistence.Database.Context;
using Identity.Infrastructure.Persistence.Repositories;
using Identity.Infrastructure.Persistence.UOW;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Application.UOW;

public class IdentityUnitofWork : UnitOfWork, IIdentityUnitofWork
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<IdentityUserEntity> _userManager;
    private readonly SignInManager<IdentityUserEntity> _signInManager;
    public IdentityUnitofWork(
        ApplicationDbContext dbContext,
        ILogger<ApplicationDbContext> logger,
        SignInManager<IdentityUserEntity> signInManager,
        UserManager<IdentityUserEntity> userManager) : base(dbContext, logger)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IUserRepository Users
        => new UserRepository(_userManager, _signInManager, _dbContext);

    public IRefreshTokenRepository RefreshTokens
        => new RefreshTokenRepository(_dbContext);

}
