using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Register DiscountContext
builder.Services.AddDbContext<DiscountContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();
// auto migration database
app.UseMigration();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();

app.Run();