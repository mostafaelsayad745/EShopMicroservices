


namespace Catalog.API.Products.CreateProduct
{

	public record CreateProductCommand (string Name, List<string> Category, string Description, decimal Price, string ImageFile) 
		: ICommand<CreateProductResult>;
	public record CreateProductResult (Guid Id);

	public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
	{
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
			RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
			RuleFor(x => x.ImageFile).NotEmpty().WithMessage("image is required");
			RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
		}
    }

	internal class CreateProductCommandHandler(IDocumentSession session )
		: ICommandHandler<CreateProductCommand, CreateProductResult>

	{
		public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
		{
			// create product entity from command object 
			// save product entity to database
			// return the id of the product

			


			// create product entity from command object
			var product = new Product
			{
				Name = command.Name,
				Category = command.Category,
				Description = command.Description,
				Price = command.Price,
				ImageFile = command.ImageFile
			};

			// save product entity to database
			session.Store(product);
			await session.SaveChangesAsync(cancellationToken);

			// return the id of the product
			return new CreateProductResult(product.Id);

		}
	}
}
