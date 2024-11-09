
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
	public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

	public record StoreBasketResult(string UserName);

	public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
	{
		public StoreBasketCommandValidator()
		{
			RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
			RuleFor(x => x.Cart.UserName).NotNull().WithMessage("UserName is required");
		}
	}
	public class StoreBasketCommandHandler (IBasketRepository repository , DiscountProtoService.DiscountProtoServiceClient discountProto)
		: ICommandHandler<StoreBasketCommand, StoreBasketResult>
	{
		public async Task<StoreBasketResult> Handle(StoreBasketCommand command
			, CancellationToken cancellationToken)
		{
			await DeductDiscount( command.Cart, cancellationToken);

			// save to db
			// update cache
			await repository.StoreBasket(command.Cart, cancellationToken);

			return new StoreBasketResult(command.Cart.UserName);
		}

		private async Task DeductDiscount( ShoppingCart cart, CancellationToken cancellationToken)
		{
			// communicate with discount.grpc service to get discount for each product if available
			foreach (var item in cart.Items)
			{
				var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
				item.Price -= coupon.Amount;
			}
		}
	}
}
