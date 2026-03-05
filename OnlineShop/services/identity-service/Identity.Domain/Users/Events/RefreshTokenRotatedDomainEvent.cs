using Domain.SeekWork.Events;
using Identity.Domain.Users.ValueObjects;

namespace Identity.Domain.Users.Events;

public sealed record RefreshTokenRotatedDomainEvent(RefreshTokenId RefreshTokenId, RefreshTokenId RotatedId, UserId UserId) : DomainEvent;
