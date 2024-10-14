




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(assembly);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));

});


// add fluent validation to the services
builder.Services.AddValidatorsFromAssembly(assembly);

// add carter configuration to the services
builder.Services.AddCarter();

// add marten configuration to the services

builder.Services.AddMarten(opts =>
{
	opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

// Seed the database
if (builder.Environment.IsDevelopment())
{
	builder.Services.InitializeMartenWith<CatalogInitialData>();
}
// add cutome exception handler to the pipeline

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// add health checks
builder.Services.AddHealthChecks()
	.AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapCarter();

// handle exceptions
app.UseExceptionHandler(options => { });

// handle health checks
app.UseHealthChecks("/health", new HealthCheckOptions
	{
		ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
	});

app.Run();
