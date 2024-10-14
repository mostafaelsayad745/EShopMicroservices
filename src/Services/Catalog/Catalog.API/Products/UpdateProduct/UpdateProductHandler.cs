
namespace Catalog.API.Products.UpdateProduct
{
	public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, decimal Price, string ImageFile)
		: ICommand<UpdateProductResult>;

	public record UpdateProductResult(bool IsSuccess);


	public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
	{
		public UpdateProductCommandValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id Is required");
			RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name Is required")
				.Length(5,150).WithMessage("Product Name must be between 5 and 150 characters");

		}
	}

	public class UpdateProductQueryHandler(IDocumentSession session)
		: ICommandHandler<UpdateProductCommand, UpdateProductResult>
	{
		public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
		{
			
			var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
			if (product == null)
			{
				throw new ProductNotFoundException(command.Id);
			}

			product.Name = command.Name;
			product.Category = command.Category;
			product.Description = command.Description;
			product.Price = command.Price;
			product.ImageFile = command.ImageFile;

			session.Update(product);

			await session.SaveChangesAsync(cancellationToken);

			return new UpdateProductResult(true);
		}
	}
}
