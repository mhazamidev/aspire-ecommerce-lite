using Identity.Domain.Users.Entities;
using Identity.Domain.Users.Identity;
using Identity.Domain.Users.Repositories;
using Identity.Infrastructure.Persistence.Database.Context;
using Identity.Infrastructure.Persistence.Database.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Identity.Infrastructure.Persistence.Repositories;

public class UserRepository(
    UserManager<IdentityUserEntity> _userManager,
    SignInManager<IdentityUserEntity> _signInManager,
    ApplicationDbContext _context) : IUserRepository
{
    public async Task<IdentityUserEntity?> LoginAsync(string username, string password)
    {
        var identityUser = await _userManager.FindByNameAsync(username);

        if (identityUser == null)
            return null;

        var result = await _signInManager.CheckPasswordSignInAsync(identityUser, password, false);

        if (!result.Succeeded)
            return null;

        return identityUser;
    }

    public async Task RegisterAsync(IdentityUserEntity user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));
    }

    public async Task UpdateAsync(IdentityUserEntity user, params Expression<Func<IdentityUserEntity, object>>[] modifiedProperties)
    {
        _context.Users.Attach(user);

        if (modifiedProperties.Length == 0)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
        else
        {
            foreach (var prop in modifiedProperties)
            {
                _context.Entry(user).Property(prop).IsModified = true;
            }
        }

        await _context.SaveChangesAsync();
    }
}
