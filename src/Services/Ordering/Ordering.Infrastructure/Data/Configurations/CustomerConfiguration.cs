
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
	public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.HasKey(c => c.Id);

			// customerId => customerId.Value : this property is used to convert the CustomerId value object to a primitive type that can be stored in the database.
			// dbId => CustomerId.Of(dbId) : this property is used to convert the primitive type stored in the database back to the CustomerId value object.
			
			builder.Property(c => c.Id).HasConversion(
				customerId => customerId.Value,
				dbId => CustomerId.Of(dbId));

			builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
			builder.Property(c => c.Email).HasMaxLength(500);

			// To make the Email property unique, we need to use the HasIndex method and specify that it is unique.
			builder.HasIndex(c => c.Email).IsUnique();
		}
	}
}
