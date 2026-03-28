using Microsoft.Azure.Cosmos;
using StayFocusAPI.Configuration;
using StayFocusAPI.APIs;
using StayFocusAPI.Services;
using StayFocusAPI.DTOs;

var builder = WebApplication.CreateBuilder(args);

// ============================================================================
// CONFIGURATION
// ============================================================================

// Load Cosmos DB settings from appsettings.json
var cosmosDbSettings = new CosmosDbSettings();
builder.Configuration.GetSection("CosmosDb").Bind(cosmosDbSettings);

// ============================================================================
// SERVICES
// ============================================================================

// Add OpenAPI support
builder.Services.AddOpenApi();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

// Register Cosmos DB client
builder.Services.AddSingleton(new CosmosClient(
    cosmosDbSettings.Endpoint,
    cosmosDbSettings.AccountKey));

// Register Review Service
builder.Services.AddSingleton<IReviewService>(sp =>
{
    var cosmosClient = sp.GetRequiredService<CosmosClient>();
    return new ReviewService(cosmosClient, cosmosDbSettings.DatabaseName, cosmosDbSettings.ContainerName);
});

// ============================================================================
// BUILD APP
// ============================================================================

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");

// Map OpenAPI
app.MapOpenApi();

// ============================================================================
// SWAGGER UI
// ============================================================================

app.MapGet("/swagger", async (HttpContext context) =>
{
    var swaggerHtmlPath = Path.Combine(app.Environment.ContentRootPath, "Assets", "swagger-ui.html");
    var swaggerHtml = await System.IO.File.ReadAllTextAsync(swaggerHtmlPath);
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync(swaggerHtml);
});

// Also expose the same Swagger UI at /api/swagger for consumers that expect the UI under /api
app.MapGet("/api/swagger", async (HttpContext context) =>
{
    var swaggerHtmlPath = Path.Combine(app.Environment.ContentRootPath, "Assets", "swagger-ui.html");
    var swaggerHtml = await System.IO.File.ReadAllTextAsync(swaggerHtmlPath);
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync(swaggerHtml);
});

// ============================================================================
// API ENDPOINTS
// ============================================================================

// Map all review endpoints
app.MapReviewEndpoints();

// Weather forecast (sample endpoint)
app.MapGet("/api/weatherforecast", () => Results.Ok(new[] {
    new { date = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"), temperatureC = 20, summary = "Sunny" },
    new { date = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd"), temperatureC = 18, summary = "Cloudy" },
    new { date = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd"), temperatureC = 15, summary = "Rainy" }
}))
.Produces<IEnumerable<object>>(StatusCodes.Status200OK)
.WithName("GetWeatherForecast")
.WithOpenApi();

// ============================================================================
// IN-MEMORY DATA STORAGE
// ============================================================================

// Initialize in-memory reviews storage
var reviews = new List<ReviewDto>
{
    new ReviewDto { Id = 1, Company = "Tesla", Rating = 4.7f, Title = "Innovative", Content = "Great technology and design." },
    new ReviewDto { Id = 2, Company = "Apple", Rating = 4.5f, Title = "Premium", Content = "High-quality products and ecosystem." },
    new ReviewDto { Id = 3, Company = "Microsoft", Rating = 4.6f, Title = "Reliable", Content = "Enterprise-grade solutions." }
};

// Store reviews in HttpContext items for access in endpoints
app.Use(async (context, next) =>
{
    context.Items["localReviews"] = reviews;
    await next();
});

// ============================================================================
// START APP
// ============================================================================

app.Run();
