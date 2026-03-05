using Domain.SeekWork;

namespace Identity.Domain.Users.ValueObjects;

public sealed class UserId : StronglyTypedId<UserId>
{
    public UserId(Guid value) : base(value)
    {
    }

    public static UserId NewId() => new(Guid.NewGuid());
}
