using Domain.SeekWork;
using Domain.SeekWork.Exceptions;
using Identity.Domain.Users.Events;
using Identity.Domain.Users.ValueObjects;
using OnlineShop.Utility;

namespace Identity.Domain.Users.Entities;

public sealed class RefreshToken : Entity<RefreshTokenId>
{
    public UserId UserId { get; private set; }
    public string Token { get; private set; }
    public DateTime Expires { get; private set; }
    public bool IsRevoked { get; private set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsActive => !IsRevoked && !IsExpired;
    public DateTime CreatedAt { get; private set; }

    private RefreshToken() : base(RefreshTokenId.NewId()) { }

    private RefreshToken(RefreshTokenId id, UserId userId, string token, DateTime expires, DateTime createdAt) : base(id)
    {
        UserId = userId;
        Token = token;
        Expires = expires;
        CreatedAt = createdAt;
    }

    public static RefreshToken Create(UserId userId, string token, DateTime expires)
    {
        if (!userId.Value.IsValid())
            throw new DomainException("Invalid user ID.");

        if (string.IsNullOrEmpty(token))
            throw new DomainException("Token cannot be null or empty.");

        if (!expires.IsValid())
            throw new DomainException("Invalid expiration date.");

        return new RefreshToken
            (
                RefreshTokenId.NewId(),
                userId,
                token,
                expires,
                DateTime.UtcNow
            );
    }

    public void Revoke()
    {
        if (!IsActive)
            return;

        IsRevoked = true;

        AddDomainEvent(new RefreshTokenRevokedDomainEvent(Id, UserId));
    }

    public RefreshToken Rotate(string newToken,DateTime newExpires)
    {
        if (!IsActive)
            throw new DomainException("Cannot rotate an inactive token.");

        Revoke();

        var rotated = new RefreshToken(
           RefreshTokenId.NewId(),
           UserId,
           newToken,
           newExpires,
           DateTime.UtcNow
       );

        AddDomainEvent(new RefreshTokenRotatedDomainEvent(Id, rotated.Id, UserId));

        return rotated;
    }


}
