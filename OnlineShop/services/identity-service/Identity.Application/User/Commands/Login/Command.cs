namespace Identity.Application.User.Commands.Login;

public record LoginCommand(string UserName, string Password) : IRequest<LoginResponse>
{
}







