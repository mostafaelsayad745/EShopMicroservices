

namespace Ordering.Domain.Abstractions
{
	// This class provides a base implementation for the IEntity<T> interface and inherits from IEntity.
	// By making it abstract, you prevent direct instantiation, requiring concrete entities to extend it.
	public abstract class Entity<T> : IEntity<T>
	{
		public T Id { get; set; } = default!;
		public DateTime? CreatedAt { get; set; }
		public string? CreatedBy { get; set; }
		public DateTime? LastModified { get; set; }
		public string? LastModifiedBy { get; set; }


	}
}
