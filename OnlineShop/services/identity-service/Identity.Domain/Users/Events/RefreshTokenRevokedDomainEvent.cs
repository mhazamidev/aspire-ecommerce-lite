using Domain.SeekWork.Events;
using Identity.Domain.Users.ValueObjects;

namespace Identity.Domain.Users.Events;

public sealed record RefreshTokenRevokedDomainEvent(RefreshTokenId RefreshTokenId, UserId UserId) : DomainEvent;
