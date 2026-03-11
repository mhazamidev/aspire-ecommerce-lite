using Identity.Domain.Users.Entities;
using Identity.Domain.Users.Identity;
using Identity.Domain.Users.Services;
using Identity.Infrastructure.Persistence.Database.Context;
using Identity.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Infrastructure.Persistence.Implementation;

public class UserService(
    ApplicationDbContext _context,
    IOptions<JwtOption> options, 
    UserManager<IdentityUserEntity> userManager) : IUserService
{
    public async Task<string> GenerateTokenAsync(IdentityUserEntity user, DateTime expireTime)
    {
        var jwtOption = options.Value;


        var userClaims = await userManager.GetClaimsAsync(user);

        var roleIds = await _context.UserRoles             
            .AsNoTracking()
            .Where(x => x.UserId == user.Id)
            .Select(x => x.RoleId)
            .ToListAsync();

        var userRoleClaims = await _context.RoleClaims
            .AsNoTracking()
            .Where(x => roleIds.Contains(x.RoleId))
            .Select(x => x.ToClaim())
            .ToListAsync();

        var roles = await userManager.GetRolesAsync(user);

        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user?.Id.ToString() ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user?.Id.ToString() ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user?.UserName ?? ""),
                new Claim(ClaimTypes.Name, user?.UserName ?? ""),
                new Claim("FName", user?.FirstName ?? ""),
                new Claim("LName", user?.LastName ?? ""),
            }
        .Union(userRoleClaims)
        .Union(userClaims)
        .ToList();


        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }


        var seurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Key));

        var signingCredential = new SigningCredentials(seurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: jwtOption.Issuer,
            audience: jwtOption.Audience,
            claims: claims,
            expires: expireTime,
            signingCredentials: signingCredential
            );

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
