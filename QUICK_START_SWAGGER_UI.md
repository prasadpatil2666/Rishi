# 🎨 Swagger UI - Quick Start

## Launch API + UI in 30 Seconds

```powershell
cd C:\Users\Prasad Patil\source\repos\Rishi\StayFocusAPI
dotnet run
```

Then open your browser:
```
http://localhost:5000/swagger
```

**Done!** 🚀 Your API testing UI is live.

---

## What You Get

🎨 **Beautiful UI** - Modern, responsive design
🧪 **Interactive Testing** - Click & test endpoints
📊 **Live Responses** - See JSON output instantly
🎯 **All Endpoints** - GET, POST, GET by ID, Weather
📱 **Mobile Ready** - Works on all devices
⚡ **Zero Dependencies** - Native .NET 10 only

---

## Test an Endpoint (30 seconds)

1. Click "GET /api/reviews" card
2. Click "Send Request"
3. See response ✅

---

## Create a Review (1 minute)

1. Click "POST /api/reviews" card
2. Fill form:
   - Company: "Tesla"
   - Rating: 4.7
   - Title: "Innovative"
   - Content: "Great tech!"
3. Click "Send Request" ✅

---

## Files

- `StayFocusAPI/swagger-ui.html` - UI (custom built)
- `StayFocusAPI/Program.cs` - Serves UI at /swagger
- No external NuGet packages needed!

---

## OpenAPI Spec

For mobile SDKs:
```
http://localhost:5000/openapi/v1.json
```

---

## Commands

| Command | Purpose |
|---------|---------|
| `dotnet run` | Start API + UI |
| `dotnet build` | Build API |
| `dotnet clean` | Clean build artifacts |
| `dotnet publish -c Release -o ./publish` | Publish for production |

---

## Links

- UI: `http://localhost:5000/swagger`
- API: `http://localhost:5000/api/reviews`
- OpenAPI: `http://localhost:5000/openapi/v1.json`

---

**That's it!** Your API is ready for testing. 🎉
