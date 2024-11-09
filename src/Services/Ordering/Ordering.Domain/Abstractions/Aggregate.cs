


namespace Ordering.Domain.Abstractions
{
	public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
	{
		public readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
		public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

		public void AddDomainEvent(IDomainEvent domainEvent)
		{
			_domainEvents.Add(domainEvent);
		}

		public IDomainEvent[] ClearDomainEvents()
		{
			IDomainEvent[] dequeuedEvent = _domainEvents.ToArray();
			_domainEvents.Clear();
			return dequeuedEvent;

		}
	}

}
