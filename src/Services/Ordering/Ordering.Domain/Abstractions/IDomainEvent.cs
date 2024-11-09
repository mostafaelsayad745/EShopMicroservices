

using MediatR;

namespace Ordering.Domain.Abstractions
{
	// This interface is used to represent a domain event in DDD.
	// Domain events capture things that happen within the domain that other parts of the system may need to react to.
	public interface IDomainEvent : INotification
	{
		// EventId gives each event a unique identifier.
		// OccurredOn tracks when the event occurred.
		// EventType identifies the event type using reflection.
		Guid EventId => Guid.NewGuid();
		public DateTime OccurredOn => DateTime.UtcNow;
		public string EventType => GetType().AssemblyQualifiedName!;
	}
}
