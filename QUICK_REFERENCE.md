# 🚀 Quick Reference Guide

## File Locations

```
Project Structure:
├── Program.cs                          Main entry point
├── appsettings.json                   Default config
├── Configuration/CosmosDbSettings.cs   Settings model
├── DTOs/                               15 data models
├── Services/ReviewService.cs           Business logic
├── APIs/ReviewEndpoints.cs             Route mapping
└── Assets/swagger-ui.html              Test interface
```

---

## Common Tasks

### 1. Add Configuration Variable

**In `appsettings.json`:**
```json
{
  "MySettings": {
    "Value": "my-value"
  }
}
```

**Create Config Class:**
```csharp
public class MySettings
{
    public string Value { get; set; }
}
```

**In `Program.cs`:**
```csharp
var settings = new MySettings();
builder.Configuration.GetSection("MySettings").Bind(settings);
```

---

### 2. Add New DTO

**Create File:**
```csharp
// DTOs/YourDto.cs
namespace StayFocusAPI.DTOs;

public class YourDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
```

---

### 3. Add New Service

**Create Service:**
```csharp
// Services/YourService.cs
public interface IYourService
{
    Task<List<YourDto>> GetAllAsync();
}

public class YourService : IYourService
{
    public async Task<List<YourDto>> GetAllAsync()
    {
        // Implementation
        return new List<YourDto>();
    }
}
```

**Register in `Program.cs`:**
```csharp
builder.Services.AddSingleton<IYourService, YourService>();
```

---

### 4. Add New API Endpoints

**Create Endpoints Class:**
```csharp
// APIs/YourEndpoints.cs
public static class YourEndpoints
{
    public static void MapYourEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/your")
            .WithName("Your")
            .WithOpenApi();

        group.MapGet("/", GetAll);
        group.MapGet("/{id}", GetById);
    }

    private static async Task<IResult> GetAll(IYourService service)
    {
        return Results.Ok(await service.GetAllAsync());
    }

    private static IResult GetById(int id)
    {
        return Results.Ok(new { id });
    }
}
```

**Map in `Program.cs`:**
```csharp
app.MapYourEndpoints();
```

---

### 5. Access Dependency

**In Endpoint Handler:**
```csharp
private static async Task<IResult> GetReviews(IReviewService reviewService)
{
    var reviews = await reviewService.GetAllReviewsAsync();
    return Results.Ok(reviews);
}
```

**In Service Constructor:**
```csharp
public class YourService
{
    private readonly CosmosClient _cosmosClient;

    public YourService(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
    }
}
```

---

## Common Patterns

### Error Handling

```csharp
try
{
    // Code that might fail
    return await operation();
}
catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
{
    return Results.NotFound(new { error = "Not found" });
}
catch (Exception ex)
{
    return Results.BadRequest(new { error = ex.Message });
}
```

### Async/Await

```csharp
// ✅ Good
public async Task<List<Item>> GetItemsAsync()
{
    return await service.GetItemsAsync();
}

// ❌ Bad
public List<Item> GetItems()
{
    return service.GetItemsAsync().Result;  // Don't use Result
}
```

### Dependency Injection

```csharp
// ✅ Good - Constructor injection
public class MyService
{
    private readonly IRepository _repo;
    public MyService(IRepository repo) => _repo = repo;
}

// ❌ Bad - Creating new instance
public class MyService
{
    private readonly IRepository _repo = new Repository();
}
```

---

## API Endpoints Reference

### Local Reviews
```
GET    /api/reviews              → Get all reviews
GET    /api/reviews/{id}         → Get review by ID
POST   /api/reviews              → Create new review
```

### Cosmos DB Reviews
```
GET    /api/cosmos/reviews       → Get all from Cosmos DB
GET    /api/cosmos/reviews/{id}  → Get specific from Cosmos DB
```

### Sample
```
GET    /api/weatherforecast      → Sample weather data
```

### Documentation
```
GET    /swagger                  → Beautiful test UI
GET    /openapi/v1.json         → OpenAPI specification
```

---

## Response Format

### Success Response
```json
{
  "count": 1,
  "data": [
    {
      "id": "review_123",
      "rating": 8.7,
      "title": "Great product",
      "comment": "Very satisfied..."
    }
  ]
}
```

### Error Response
```json
{
  "error": "Review not found"
}
```

### Single Item Response
```json
{
  "id": "review_123",
  "rating": 8.7,
  "title": "Great product",
  "comment": "Very satisfied..."
}
```

---

## Command Reference

### Build Project
```bash
dotnet build
```

### Run Project
```bash
dotnet run
```

### Run with Release Build
```bash
dotnet run --configuration Release
```

### Create New Project
```bash
dotnet new webapi -n MyProject
```

### Add NuGet Package
```bash
dotnet add package PackageName
```

### Run Tests
```bash
dotnet test
```

---

## Folder Structure Quick Facts

| Folder | Purpose | Files |
|--------|---------|-------|
| `Configuration/` | Settings models | 1 file |
| `DTOs/` | Data models | 15 files |
| `Services/` | Business logic | 1+ files |
| `APIs/` | Route mappings | 1+ files |
| `Assets/` | Static files | HTML, CSS, JS |

---

## Key Concepts

### DTO (Data Transfer Object)
- Transfers data between layers
- Contains only properties
- No business logic
- Example: `ReviewDto.cs`

### Service
- Contains business logic
- Handles data operations
- Uses DI for dependencies
- Example: `ReviewService.cs`

### Endpoint
- Maps HTTP routes
- Uses dependency injection
- Calls services
- Returns formatted responses
- Example: `ReviewEndpoints.cs`

### Configuration
- External settings
- Environment-specific
- No hardcoded values
- Example: `appsettings.json`

---

## Debugging Tips

### Enable Detailed Logging
In `appsettings.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Debug"
    }
  }
}
```

### Check What Services Are Registered
In `Program.cs`:
```csharp
var services = builder.Services;
// Use Visual Studio debugger to inspect
```

### Verify Configuration Binding
```csharp
var settings = new CosmosDbSettings();
builder.Configuration.GetSection("CosmosDb").Bind(settings);
System.Diagnostics.Debug.WriteLine($"Endpoint: {settings.Endpoint}");
```

### Test API Directly
1. Open http://localhost:5000/swagger
2. Select endpoint
3. Click "Try it out"
4. Enter parameters
5. Click "Execute"
6. View response

---

## Performance Tips

### 1. Use Async/Await
```csharp
// ✅ Better performance
public async Task<List<Item>> GetAllAsync()
{
    return await _service.GetAllAsync();
}
```

### 2. Use DTOs
```csharp
// ✅ Transfer only needed fields
public class ReviewDto
{
    public string? Title { get; set; }
    public double Rating { get; set; }
}
```

### 3. Connection Pooling
```csharp
// ✅ Register CosmosClient as singleton
builder.Services.AddSingleton(new CosmosClient(...));
```

### 4. Pagination
```csharp
// ✅ Limit results
var iterator = container.GetItemQueryIterator<T>("SELECT * FROM c OFFSET 0 LIMIT 10");
```

---

## Security Checklist

- [ ] ✅ No hardcoded secrets in code
- [ ] ✅ Configuration from appsettings.json
- [ ] ✅ Secrets in Azure Key Vault (production)
- [ ] ✅ CORS configured properly
- [ ] ✅ Input validation on endpoints
- [ ] ✅ Error messages don't leak details
- [ ] ✅ Use HTTPS in production
- [ ] ✅ Authentication/Authorization (if needed)

---

## Deployment Steps

### 1. Prepare
```bash
dotnet build -c Release
dotnet publish -c Release -o ./publish
```

### 2. Configure Production
```json
{
  "CosmosDb": {
    "Endpoint": "production-endpoint",
    "AccountKey": "from-key-vault",
    "DatabaseName": "prod-db",
    "ContainerName": "prod-container"
  }
}
```

### 3. Deploy to Azure
```bash
az app up --name myappname --resource-group myresourcegroup
```

### 4. Verify
```
https://myappname.azurewebsites.net/swagger
```

---

## Code Examples

### Complete Service Example
```csharp
public interface IProductService
{
    Task<Product?> GetByIdAsync(string id);
    Task<List<Product>> GetAllAsync();
}

public class ProductService : IProductService
{
    private readonly CosmosClient _cosmosClient;

    public ProductService(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        try
        {
            var container = _cosmosClient.GetDatabase("db").GetContainer("products");
            var response = await container.ReadItemAsync<Product>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var container = _cosmosClient.GetDatabase("db").GetContainer("products");
        var query = "SELECT * FROM c";
        var iterator = container.GetItemQueryIterator<Product>(query);

        var products = new List<Product>();
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            products.AddRange(response);
        }
        return products;
    }
}
```

### Complete Endpoint Example
```csharp
public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/products")
            .WithName("Products")
            .WithOpenApi();

        group.MapGet("/", GetAll);
        group.MapGet("/{id}", GetById);
    }

    private static async Task<IResult> GetAll(IProductService service)
    {
        var products = await service.GetAllAsync();
        return Results.Ok(new { count = products.Count, data = products });
    }

    private static async Task<IResult> GetById(string id, IProductService service)
    {
        var product = await service.GetByIdAsync(id);
        if (product == null)
            return Results.NotFound(new { error = "Product not found" });
        return Results.Ok(product);
    }
}
```

---

## Troubleshooting

### Problem: Build Fails
**Solution:**
```bash
dotnet clean
dotnet restore
dotnet build
```

### Problem: Endpoints Not Working
**Solution:**
1. Verify endpoints are mapped in `Program.cs`
2. Check endpoint names match routes
3. Verify service is registered
4. Check appsettings.json is valid JSON

### Problem: Cosmos DB Connection Fails
**Solution:**
1. Verify `appsettings.json` has correct endpoint
2. Verify account key is correct
3. Check network connectivity
4. Verify database/container names exist

### Problem: Type Not Found
**Solution:**
1. Check namespace imports
2. Verify class is in correct folder
3. Check for typos
4. Rebuild solution

---

## Useful Links

- [Microsoft .NET Documentation](https://learn.microsoft.com/dotnet/)
- [Azure Cosmos DB Documentation](https://learn.microsoft.com/azure/cosmos-db/)
- [Minimal APIs](https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis)
- [Dependency Injection](https://learn.microsoft.com/aspnet/core/fundamentals/dependency-injection)

---

**Last Updated:** 2024  
**Status:** ✅ Ready for Use
