var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapFallbackToFile("index.html");

app.UseAuthorization();

app.MapControllers();

app.MapStaticAssets();

app.Run();
