using Domain.SeekWork;
using Identity.Domain.Users.Entities;

namespace Identity.Domain.Users.ValueObjects;

public sealed class RefreshTokenId : StronglyTypedId<RefreshTokenId>
{
    public RefreshTokenId(Guid value) : base(value)
    {
    }

    public static RefreshTokenId NewId() => new(Guid.NewGuid());
}
