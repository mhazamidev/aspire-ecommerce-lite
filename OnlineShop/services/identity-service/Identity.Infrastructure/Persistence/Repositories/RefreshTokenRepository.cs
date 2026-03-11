using Identity.Domain.Users.Entities;
using Identity.Domain.Users.Repositories;
using Identity.Domain.Users.ValueObjects;
using Identity.Infrastructure.Persistence.Database.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Identity.Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository(ApplicationDbContext _context) : IRefreshTokenRepository
{
    public async Task AddAsync(RefreshToken refreshToken)
    {
        await _context.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens
             .FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task<RefreshToken?> GetByUserIdAsync(UserId userId)
    {
        return await _context.RefreshTokens
             .FirstOrDefaultAsync(rt => rt.UserId == userId);
    }

    public async Task UpdateAsync(RefreshToken token, params Expression<Func<RefreshToken, object>>[] modifiedProperties)
    {
        _context.RefreshTokens.Attach(token);

        if (modifiedProperties.Length == 0)
        {
            _context.Entry(token).State = EntityState.Modified;
        }
        else
        {
            foreach (var prop in modifiedProperties)
            {
                _context.Entry(token).Property(prop).IsModified = true;
            }
        }

        await _context.SaveChangesAsync();
    }
}
