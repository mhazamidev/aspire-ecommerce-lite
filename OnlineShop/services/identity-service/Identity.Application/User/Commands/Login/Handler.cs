using Application.Shared.Exceptions;
using Identity.Domain.Users.Entities;
using Identity.Domain.Users.Services;
using Identity.Domain.Users.ValueObjects;
using Identity.Infrastructure.Persistence.UOW;
using Identity.Shared;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Identity.Application.User.Commands.Login;

public class LoginCommandHandler(
    IOptions<JwtOption> options,
    IIdentityUnitofWork _unitofWork,
    IUserService _userService) : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var jwtOption = options.Value;

        var user = await _unitofWork.Users.LoginAsync(request.UserName, request.Password);
        if (user == null)
            throw new AuthenticationException();

        var userId = new UserId(user.Id);
        var now = DateTime.UtcNow;
        var refreshTokenExpireTime = now.AddSeconds(jwtOption.RefreshTokenExpireInSecond);
        var tokenExpireTime = now.AddSeconds(jwtOption.TokenExpireInSecond);
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var userRefreshToken = await _unitofWork.RefreshTokens.GetByUserIdAsync(userId);


        if (userRefreshToken is null)
            await _unitofWork.RefreshTokens.AddAsync(RefreshToken.Create(userId, refreshToken, refreshTokenExpireTime));
        else
        {
            var newRefreshToken = userRefreshToken.Rotate(refreshToken, refreshTokenExpireTime);
            await _unitofWork.RefreshTokens.AddAsync(newRefreshToken);

            userRefreshToken.Revoke();
            await _unitofWork.RefreshTokens.UpdateAsync(userRefreshToken, x => x.IsRevoked);
        }


        var token = await _userService.GenerateTokenAsync(user, tokenExpireTime);


        return new LoginResponse(token, refreshToken, user.Id, tokenExpireTime, refreshTokenExpireTime);
    }
}