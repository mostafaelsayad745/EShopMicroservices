

namespace Ordering.Domain.Abstractions
{
    public interface IAggregate<T> : IAggregate,IEntity<T>
    {
        
    }

    public interface IAggregate : IDomainEvent
	{
		IReadOnlyList<IDomainEvent> DomainEvents { get; }

		IDomainEvent[] ClearDomainEvents();
	}
}
