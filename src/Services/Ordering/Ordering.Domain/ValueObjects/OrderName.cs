
namespace Ordering.Domain.ValueObjects
{
	// Primitive Obsession
	public record OrderName
	{
		private const int DefaultLength = 5;
		public string Value { get; } = default!;

		private OrderName(string value) => Value = value;

		public static OrderName Of(string value)
		{
			ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));
			ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

			return new OrderName(value);
		}
	}
}
