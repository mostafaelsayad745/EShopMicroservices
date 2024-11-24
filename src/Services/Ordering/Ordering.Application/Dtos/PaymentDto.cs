public record PaymentDto
(
	string CardNumber,
	string CardName,
	string Expiration,
	string Cvv,
	int PaymentMethod
);