using Domain.SeekWork.Events;
using Identity.Domain.Users.ValueObjects;

namespace Identity.Domain.Users.Events;

public record UserEmailChangedDomainEvent(UserId Id, Email Email) : DomainEvent;

