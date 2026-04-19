using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add rate limiter
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("one-per-minute-per-client", context =>
    {
        var apiKey = context.Request.Headers["Client-Name"].FirstOrDefault();

        return RateLimitPartition.GetFixedWindowLimiter(
           partitionKey: apiKey,
           factory: _ => new FixedWindowRateLimiterOptions
           {
               PermitLimit = 1,
               Window = TimeSpan.FromMinutes(1),
               QueueLimit = 0
           });
    });

    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        await context.HttpContext.Response.WriteAsync("Too many requests. Try again later.", token);
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapFallbackToFile("index.html");

app.UseRateLimiter();

app.UseAuthorization();

app.MapControllers();

app.MapStaticAssets();

app.Run();
