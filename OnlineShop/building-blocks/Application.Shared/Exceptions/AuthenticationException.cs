namespace Application.Shared.Exceptions;

public class AuthenticationException : Exception
{
    public AuthenticationException()
        : base("The username or password is incorrect.")
    {
    }

    public AuthenticationException(string message)
        : base(message)
    {
    }

    public AuthenticationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
