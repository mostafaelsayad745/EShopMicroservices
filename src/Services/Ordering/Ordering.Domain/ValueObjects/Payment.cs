

namespace Ordering.Domain.ValueObjects
{
	public record Payment
	{
		

		public string? CardName { get;  } = default!;
		public string CardNumber { get; } = default!;
		public string Expiration { get; } = default!;
		public string CVV { get; } = default!;
		public int PaymentMethod { get; } = default!;

		protected Payment()
		{

		}

		private Payment(string? cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
		{
			CardName = cardName;
			CardNumber = cardNumber;
			Expiration = expiration;
			CVV = cvv;
			PaymentMethod = paymentMethod;
		}

		public static Payment Of(string? cardName, string cardNumber, string expiration, string cVV, int paymentMethod)
		{

			ArgumentException.ThrowIfNullOrEmpty(cardNumber, nameof(cardNumber));
			ArgumentException.ThrowIfNullOrEmpty(expiration, nameof(expiration));
			ArgumentException.ThrowIfNullOrEmpty(cVV, nameof(cVV));
			ArgumentOutOfRangeException.ThrowIfGreaterThan(cVV.Length, 3);


			return new Payment(cardName, cardNumber, expiration, cVV, paymentMethod);
		}
	}
}
