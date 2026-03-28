# 🚀 API Integration - Quick Start Guide

## What Was Set Up

✅ **ASP.NET Core REST API** with:
- 📋 Swagger/OpenAPI documentation
- 🔄 CORS enabled for mobile & external apps
- 📦 Sample Review endpoints (`GET`, `POST`)
- 🛡️ Production-ready structure

✅ **Blazor Frontend Integration**:
- 🔌 `ReviewApiClient` service to call the API
- 📄 `/reviews-api` page to demonstrate API usage
- 🎨 Styled with your theme colors

## How to Run

### Step 1: Start the API
```bash
cd StayFocusAPI
dotnet run
```
✅ API runs on `http://localhost:5000`

### Step 2: Start Blazor Frontend (in another terminal)
```bash
cd StayFocus
dotnet run
```
✅ Frontend runs on `http://localhost:5173` (or similar)

### Step 3: Test the Integration

**Option A: Via Blazor Page**
- Navigate to `/reviews-api` page in your Blazor app
- Click "Load Reviews from API"
- See the API response rendered on the page

**Option B: Via Swagger UI**
- Open `http://localhost:5000/swagger`
- Try out the endpoints directly
- See request/response examples

## API Endpoints

| Method | Endpoint | Purpose |
|--------|----------|---------|
| `GET` | `/api/reviews` | Get all reviews |
| `GET` | `/api/reviews/{id}` | Get review by ID |
| `POST` | `/api/reviews` | Create new review |

## Using the API from Mobile/External Apps

### 1. **iOS/Android via OpenAPI**
Copy your OpenAPI spec URL and generate a typed SDK:
```
http://localhost:5000/openapi/v1.json
```

### 2. **JavaScript/React**
```javascript
const response = await fetch('http://localhost:5000/api/reviews');
const reviews = await response.json();
```

### 3. **cURL (Testing)**
```bash
# Get all reviews
curl http://localhost:5000/api/reviews

# Create review
curl -X POST http://localhost:5000/api/reviews \
  -H "Content-Type: application/json" \
  -d '{"company":"Apple","rating":4.8,"title":"Great!","content":"Love it"}'
```

## Project Structure

```
.
├── StayFocus/                          (Blazor WASM Frontend)
│   ├── Pages/ReviewsApi.razor          ← NEW: Demo page calling API
│   ├── Services/ReviewApiClient.cs     ← NEW: API client service
│   ├── Program.cs                      ← UPDATED: Register API client
│   └── wwwroot/
│
├── StayFocusAPI/                       (ASP.NET Core API)
│   ├── Program.cs                      ← UPDATED: Swagger + CORS + endpoints
│   ├── StayFocusAPI.csproj             ← UPDATED: Added Swashbuckle.AspNetCore
│   └── README_API.md                   ← NEW: Comprehensive API docs
│
├── Rishi.sln                           ← NEW: Solution file
└── azure-static-web-apps-*.yml         ← Existing: Deployment config
```

## Next Steps

### 🔐 Add Authentication
```csharp
// In StayFocusAPI/Program.cs
builder.Services.AddAuthentication()
    .AddJwtBearer(options => { /* config */ });
```

### 💾 Add Database
```bash
cd StayFocusAPI
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

### 🌐 Deploy to Azure

**API (API App):**
```bash
az webapp up --sku B1 -n <app-name>-api
```

**Frontend (Static Web App):** Already configured in your workflow

### 📱 Generate Mobile SDK

**For React:**
```bash
npm install openapi-generator-cli -g
openapi-generator-cli generate -i http://localhost:5000/openapi/v1.json \
  -g typescript-axios -o ./src/generated
```

## Troubleshooting

| Issue | Solution |
|-------|----------|
| API returns 404 | Check API is running on port 5000 |
| CORS error | API CORS is configured to allow all origins |
| Swagger not loading | Ensure Swashbuckle is in csproj: `dotnet add package Swashbuckle.AspNetCore` |
| "Connection refused" | Check both API and Blazor are running |

## Files Created/Modified

**Created:**
- ✅ `StayFocus/Services/ReviewApiClient.cs` - API client
- ✅ `StayFocus/Pages/ReviewsApi.razor` - Demo page
- ✅ `StayFocusAPI/README_API.md` - API docs
- ✅ `Rishi.sln` - Solution file

**Modified:**
- ✅ `StayFocus/Program.cs` - Register API client
- ✅ `StayFocusAPI/Program.cs` - Add Swagger + CORS + endpoints
- ✅ `StayFocusAPI/StayFocusAPI.csproj` - Add Swashbuckle NuGet

## Questions?

1. Check `StayFocusAPI/README_API.md` for detailed API docs
2. Visit Swagger UI at `http://localhost:5000/swagger` for interactive testing
3. Check the `/reviews-api` page source in `StayFocus/Pages/ReviewsApi.razor` for integration examples

---

**Ready to go live?** See the API deployment section in `StayFocusAPI/README_API.md` for production setup.
