using Domain.SeekWork;
using Domain.SeekWork.Exceptions;
using Identity.Domain.Users.Events;
using Identity.Domain.Users.ValueObjects;

namespace Identity.Domain.Users.Entities;

public class User : AggregateRoot<UserId>
{
    public FullName Name { get; private set; }
    public Email Email { get; private set; }
    public bool IsActive { get; private set; }

    private readonly List<RefreshToken> _refreshTokens = new();
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();
    private User() : base(UserId.NewId()) { }

    private User(UserId id, FullName fullName, Email email, bool isActive) : base(id)
    {
        Name = fullName;
        Email = email;
        IsActive = isActive;
    }

    public static User Create(string firstName, string lastName, string email, bool isActive)
    {
        return new User
        (
            UserId.NewId(),
            FullName.Create(firstName, lastName),
            Email.Create(email),
            isActive
        );
    }

    public void ChangeName(string firstName, string lastName)
    {
        var name = FullName.Create(firstName, lastName);

        if (Name.Equals(name))
            return;

        Name = name;
    }

    public void ChangeEmail(string newEmail)
    {
        var email = Email.Create(newEmail);

        if (Email.Equals(email))
            return;

        Email = email;

        AddDomainEvent(new UserEmailChangedDomainEvent(Id, Email));
    }

    public void Enable()
    {
        if (IsActive)
            return;

        IsActive = true;
    }

    public void Disable()
    {
        if (!IsActive)
            return;

        IsActive = false;
    }

    public void AddRefreshToken(string token, DateTime expires)
    {
        var refreshToken = RefreshToken.Create(Id, token, expires);
        
        _refreshTokens.Add(refreshToken);

        AddDomainEvent(new UserRefreshTokenAddedDomainEvent(Id, refreshToken.Id));
    }

    public void ReplaceRefreshToken(RefreshToken oldToken, RefreshToken newToken)
    {
        if (!_refreshTokens.Contains(oldToken))
            throw new DomainException("The token to replace does not exist.");

        _refreshTokens.Remove(oldToken);

        _refreshTokens.Add(newToken);
    }
}
