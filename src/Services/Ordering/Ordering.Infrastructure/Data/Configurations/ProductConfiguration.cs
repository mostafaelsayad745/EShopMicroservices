
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(p => p.Id);

			// productId => productId.Value : this property is used to convert the ProductId value object to a primitive type that can be stored in the database.
			// dbId => ProductId.Of(dbId); :  this property is used to convert the primitive type stored in the database back to the CustomerId value object.
			builder.Property(p => p.Id).HasConversion(
				productId => productId.Value,
				dbId => ProductId.Of(dbId));

			builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
		}
	}
}
