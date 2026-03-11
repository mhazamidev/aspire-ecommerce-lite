namespace Identity.Application.User.Commands.Login;

public record LoginResponse(string Token, string RefreshToken,Guid UserId, DateTime TokenExpire, DateTime RefreshTokenExpire);
