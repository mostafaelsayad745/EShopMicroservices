using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
	public class DiscountService(DiscountContext dbcontext, ILogger<DiscountService> logger)
		: DiscountProtoService.DiscountProtoServiceBase
	{
		public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
		{
			var coupon = await dbcontext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
			if (coupon == null)
			{
				coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
			}

			logger.LogInformation("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);

			var couponModel = coupon.Adapt<CouponModel>();
			return couponModel;
		}

		public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
		{
			var coupon = request.Coupon.Adapt<Coupon>();
			if (coupon == null)
			{
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Discount Request"));
			}
			await dbcontext.Coupons.AddAsync(coupon);
			await dbcontext.SaveChangesAsync();
			logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);
			var couponModel = coupon.Adapt<CouponModel>();
			return couponModel;
		}

		public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
		{
			var coupon = request.Coupon.Adapt<Coupon>();
			if (coupon == null)
			{
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Discount Request"));
			}
			dbcontext.Coupons.Update(coupon);
			await dbcontext.SaveChangesAsync();
			logger.LogInformation("Discount is successfully Updated. ProductName : {ProductName}", coupon.ProductName);
			var couponModel = coupon.Adapt<CouponModel>();
			return couponModel;
		}

		public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
		{
			var coupon = await dbcontext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

			if (coupon == null)
			{
				throw new RpcException(new Status(StatusCode.NotFound, "Discount with ProductName not found"));
			}

			dbcontext.Coupons.Remove(coupon);
			await dbcontext.SaveChangesAsync();

			logger.LogInformation("Discount is successfully deleted. ProductName : {ProductName}", request.ProductName);

			return new DeleteDiscountResponse { Success = true };
		}
	}
}