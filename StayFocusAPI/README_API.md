# StayFocus API Documentation

## Overview
The **StayFocus API** is a RESTful API with OpenAPI/Swagger documentation that provides review management endpoints. It can be consumed by:
- **Blazor WebAssembly frontend** (included in this project)
- **Mobile apps** (iOS, Android)
- **Web applications** (React, Vue, Angular)
- **Third-party integrations**

## Quick Start

### 1. Run the API
```bash
cd StayFocusAPI
dotnet run
```

The API will start on `http://localhost:5000` (dev) or `https://localhost:5001` (secure).

### 2. Access Swagger UI
Open your browser:
```
http://localhost:5000/swagger/index.html
```

### 3. Access OpenAPI JSON (for mobile/external apps)
```
http://localhost:5000/openapi/v1.json
```

## API Endpoints

### Get All Reviews
```
GET /api/reviews
```
**Response:**
```json
[
  {
    "id": 1,
    "company": "Amazon",
    "rating": 4.5,
    "title": "Great service",
    "content": "Fast delivery and good support."
  }
]
```

### Get Review by ID
```
GET /api/reviews/{id}
```

### Create Review
```
POST /api/reviews
Content-Type: application/json

{
  "company": "Apple",
  "rating": 4.8,
  "title": "Premium quality",
  "content": "Excellent build quality and support."
}
```

**Response:** Created review with assigned ID (201 Created)

## CORS Configuration
The API allows requests from:
- **Any origin** (`*`)
- **Any HTTP method** (GET, POST, PUT, DELETE, etc.)
- **Any header**

This is configured for development. For production, restrict origins:

**Example (production):**
```csharp
options.AddPolicy("AllowSpecific", policy =>
{
    policy.WithOrigins("https://yourdomain.com", "https://app.yourdomain.com")
          .AllowAnyMethod()
          .AllowAnyHeader();
});
```

## Using the API from Blazor

### 1. Inject the Service
```csharp
@inject ReviewApiClient ReviewApi
```

### 2. Call API Methods
```csharp
// Get all reviews
var reviews = await ReviewApi.GetReviewsAsync();

// Get specific review
var review = await ReviewApi.GetReviewAsync(1);

// Create review
var newReview = new ReviewDto 
{ 
    Company = "Microsoft", 
    Rating = 4.9, 
    Title = "Excellent", 
    Content = "Great product" 
};
var created = await ReviewApi.CreateReviewAsync(newReview);
```

### 3. See Example Page
Navigate to: `http://localhost:5173/reviews-api` (when running Blazor locally)

## Using the API from Mobile/External Apps

### Option 1: Swagger UI for Testing
1. Open `http://localhost:5000/swagger`
2. Click on an endpoint
3. Click "Try it out"
4. Enter parameters and click "Execute"

### Option 2: Generate SDK
Use OpenAPI Generator to generate a typed client:

**iOS (Swift):**
```bash
openapi-generator-cli generate -i http://localhost:5000/openapi/v1.json -g swift -o ./ReviewsSDK
```

**Android (Kotlin):**
```bash
openapi-generator-cli generate -i http://localhost:5000/openapi/v1.json -g kotlin -o ./ReviewsSDK
```

**JavaScript/TypeScript:**
```bash
openapi-generator-cli generate -i http://localhost:5000/openapi/v1.json -g typescript-axios -o ./ReviewsSDK
```

### Option 3: Manual cURL Requests

**Get all reviews:**
```bash
curl -X GET "http://localhost:5000/api/reviews" \
  -H "accept: application/json"
```

**Create review:**
```bash
curl -X POST "http://localhost:5000/api/reviews" \
  -H "accept: application/json" \
  -H "Content-Type: application/json" \
  -d '{
    "company": "Tesla",
    "rating": 4.7,
    "title": "Innovative",
    "content": "Great technology and design."
  }'
```

## Deployment

### Azure App Service
1. Publish to Azure:
   ```bash
   dotnet publish -c Release -o ./publish
   ```

2. Update CORS for production domain:
   ```csharp
   policy.WithOrigins("https://yourdomain.com")
   ```

3. Deploy via Azure CLI or Visual Studio

### Docker
1. Create `Dockerfile`:
   ```dockerfile
   FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
   WORKDIR /app
   COPY . .
   RUN dotnet publish -c Release -o out

   FROM mcr.microsoft.com/dotnet/aspnet:10.0
   WORKDIR /app
   COPY --from=build /app/out .
   EXPOSE 80
   CMD ["dotnet", "StayFocusAPI.dll"]
   ```

2. Build and run:
   ```bash
   docker build -t stayfocus-api .
   docker run -p 5000:80 stayfocus-api
   ```

## Security Notes

- **Enable HTTPS** in production
- **Restrict CORS** to known domains
- **Add authentication** (JWT, OAuth2) for sensitive endpoints
- **Validate input** on all endpoints
- **Use API keys** or **Bearer tokens** for client identification

## Troubleshooting

**API not responding?**
- Check if the API is running: `dotnet run`
- Verify the port (default 5000 dev, 5001 secure)
- Check firewall rules

**CORS errors in browser?**
- Ensure CORS is configured in `Program.cs`
- Check browser Network tab for `Access-Control-Allow-Origin` header

**Swagger not loading?**
- Verify `Swashbuckle.AspNetCore` is in the csproj
- Ensure `app.UseSwagger()` and `app.UseSwaggerUI()` are called

## Next Steps

1. ✅ Connect Blazor frontend to API
2. ✅ Test with Swagger UI
3. ✅ Generate SDKs for mobile apps
4. 📝 Add authentication (JWT)
5. 📝 Add database (Entity Framework)
6. 📝 Deploy to Azure

---

**Questions?** Check the Swagger UI at `http://localhost:5000/swagger` for full documentation.
