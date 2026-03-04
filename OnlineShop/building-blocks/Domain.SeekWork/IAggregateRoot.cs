using Domain.SeekWork.Events;

namespace Domain.SeekWork;
public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
