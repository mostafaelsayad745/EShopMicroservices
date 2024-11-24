using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
	.AddApplicationServices(builder.Configuration)
	.AddInfrastructureServices(builder.Configuration)
	.AddApiServices(builder.Configuration);

var app = builder.Build();

app.UserApiServices();
// Configure the HTTP request pipeline.

if(app.Environment.IsDevelopment())
{
	await app.InitializeDatabaseAsync();
}

app.Run();
