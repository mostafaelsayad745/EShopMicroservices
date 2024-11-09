
namespace Ordering.Domain.Abstractions
{
	// This is a generic interface for entities, allowing flexibility with different types of IDs (such as int, Guid, or custom types).
	public interface IEntity<T> : IEntity
	{
        public T Id { get; set; }
    }

	// The base interface defines common properties for all entities
	public interface IEntity
	{
		// help in tracking who created or modified an entity and when.
		public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
		public DateTime? LastModified { get; set; }
		public string? LastModifiedBy { get; set; }
	}
}
