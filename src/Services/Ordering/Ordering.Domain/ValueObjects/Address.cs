

namespace Ordering.Domain.ValueObjects
{
	public record Address
	{
		

		public string FristName { get; } = default!;
		public string LastName { get; } = default!;
		public string EmailAddress { get; } = default!;
		public string AddressLine { get; } = default!;
		public string State { get; } = default!;
		public string Country { get; } = default!;
		public string ZipCode { get; } = default!;

        protected Address()
        {
            
        }
        private Address(string fristName, string lastName, string emailAddress, string addressLine, string state, string country, string zipCode)
		{
			FristName = fristName;
			LastName = lastName;
			EmailAddress = emailAddress;
			AddressLine = addressLine;
			State = state;
			Country = country;
			ZipCode = zipCode;
		}

		public static Address Of(string fristName, string lastName, string emailAddress, string addressLine, string state, string country, string zipCode)
		{
			
			ArgumentException.ThrowIfNullOrEmpty(emailAddress, nameof(emailAddress));
			ArgumentException.ThrowIfNullOrEmpty(addressLine, nameof(addressLine));
		

			return new Address(fristName, lastName, emailAddress, addressLine, state, country, zipCode);
		}
	}
}
