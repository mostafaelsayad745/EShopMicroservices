
namespace Catalog.API.Products.GetProductsByCategory
{
	public record GetProductsByCategoryRequest(string Category) : IQuery<GetProductsByCategoryResult>;
	public record GetProductsByCategoryResult(IEnumerable<Product> Products);
	internal class GetProductsByCategoryQueryHandler(IDocumentSession session)
		: IQueryHandler<GetProductsByCategoryRequest, GetProductsByCategoryResult>
	{
		public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryRequest query, CancellationToken cancellationToken)
		{
			
			var products = await session.Query<Product>()
				.Where(p => p.Category.Contains(query.Category))
				.ToListAsync(cancellationToken);

			return new GetProductsByCategoryResult(products);
		}
	}

}
