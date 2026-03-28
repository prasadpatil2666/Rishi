# 🎨 Custom Swagger UI - API Testing Hub

## Overview

I've created a **beautiful, custom UI for testing your API** - similar to Swagger but tailored specifically for StayFocus. It's interactive, responsive, and ready to use.

---

## Access the UI

**When API is running:**
```
http://localhost:5000/swagger
```

---

## Features

✅ **Interactive Endpoint Testing**
- Click on any endpoint to test it
- Fill in parameters (IDs, request body, etc.)
- Send requests and see live responses

✅ **Beautiful Design**
- Modern gradient interface
- Responsive layout (works on mobile too)
- Color-coded HTTP methods:
  - 🟢 GET (Green)
  - 🔵 POST (Blue)
  - 🟠 PUT (Orange)
  - 🔴 DELETE (Red)

✅ **Live Response Display**
- View API responses in real-time
- See status codes (200, 404, 500, etc.)
- Pretty-printed JSON output
- Color-coded success/error responses

✅ **Quick Links**
- 📋 Link to OpenAPI spec (for SDK generation)
- 🔗 Link to GitHub repository
- ✅ "Test All" button to run all endpoints

---

## Endpoints Available

### 1. **Get All Reviews**
```
GET /api/reviews
```
- **Description:** Retrieve all reviews
- **Parameters:** None
- **Response:** Array of review objects

### 2. **Get Review by ID**
```
GET /api/reviews/{id}
```
- **Description:** Retrieve a specific review
- **Parameters:**
  - `id` (number) - Review ID
- **Response:** Single review object

### 3. **Create Review**
```
POST /api/reviews
```
- **Description:** Create a new review
- **Parameters (JSON Body):**
  - `company` (string) - Company name
  - `rating` (number) - Rating 0-5
  - `title` (string) - Review title
  - `content` (string) - Review content
- **Response:** Created review with assigned ID

### 4. **Get Weather Forecast**
```
GET /api/weatherforecast
```
- **Description:** Sample endpoint for testing
- **Parameters:** None
- **Response:** Array of weather forecast objects

---

## How to Use

### Step 1: Start the API
```powershell
cd StayFocusAPI
dotnet run
```

### Step 2: Open the UI
Navigate to:
```
http://localhost:5000/swagger
```

### Step 3: Test an Endpoint

**Option A: Click an endpoint**
1. Click on any endpoint card (e.g., "GET /api/reviews")
2. The form auto-populates in the right panel
3. Click "Send Request"
4. View the response below

**Option B: Use the dropdown**
1. Click the "Select Endpoint" dropdown
2. Choose an endpoint
3. Fill in any required parameters
4. Click "Send Request"

### Step 4: View Results
- Status code appears at the top (green for success, red for error)
- Full JSON response is displayed below
- Easy to copy/paste for debugging

---

## Testing Examples

### Test 1: Get All Reviews
1. Click "GET /api/reviews" card
2. Click "Send Request"
3. See list of all reviews in response

### Test 2: Get Specific Review
1. Click "GET /api/reviews/{id}" card
2. Enter "1" in the Review ID field
3. Click "Send Request"
4. See the review with ID 1

### Test 3: Create New Review
1. Click "POST /api/reviews" card
2. Fill in the form:
   - Company: "Microsoft"
   - Rating: 4.9
   - Title: "Excellent"
   - Content: "Amazing products"
3. Click "Send Request"
4. See the newly created review with an assigned ID

---

## Advanced Testing

### Generate Mobile SDKs

The OpenAPI spec is available at:
```
http://localhost:5000/openapi/v1.json
```

**Generate TypeScript SDK:**
```bash
openapi-generator-cli generate \
  -i http://localhost:5000/openapi/v1.json \
  -g typescript-axios \
  -o ./src/generated
```

**Generate Swift SDK (iOS):**
```bash
openapi-generator-cli generate \
  -i http://localhost:5000/openapi/v1.json \
  -g swift \
  -o ./ReviewsSDK
```

**Generate Kotlin SDK (Android):**
```bash
openapi-generator-cli generate \
  -i http://localhost:5000/openapi/v1.json \
  -g kotlin \
  -o ./ReviewsSDK
```

---

## File Structure

```
StayFocusAPI/
├── Program.cs                  (Updated to serve swagger-ui.html)
├── swagger-ui.html             (NEW: Beautiful testing UI)
├── StayFocusAPI.csproj         (No external dependencies needed!)
└── ... other files
```

---

## Browser Support

✅ Chrome/Chromium
✅ Firefox
✅ Safari
✅ Edge
✅ Mobile browsers

---

## Troubleshooting

| Issue | Solution |
|-------|----------|
| UI doesn't load | Ensure API is running on port 5000 |
| Requests fail | Check browser DevTools Network tab for CORS issues |
| Can't create review | Verify JSON in request body is valid |
| Response shows 404 | Check endpoint path spelling |

---

## Key Advantages

✅ **No External Dependencies**
- Works with .NET 10's native OpenAPI
- No Swagger/Swashbuckle issues
- Lightweight and fast

✅ **Fully Customizable**
- Source code is in `swagger-ui.html`
- Easy to add more endpoints
- Easy to change colors/theme

✅ **Mobile-Friendly**
- Responsive design
- Works on tablets and phones
- Perfect for on-the-go testing

✅ **Production Ready**
- Professional appearance
- Error handling
- Live JSON formatting

---

## Next Steps

1. ✅ Run API: `dotnet run`
2. ✅ Visit: `http://localhost:5000/swagger`
3. ✅ Test endpoints
4. ✅ Generate mobile SDKs if needed
5. ✅ Deploy to Azure

---

## Customization

Want to add more endpoints or customize the UI?

Edit `StayFocusAPI/swagger-ui.html` and:
1. Add new endpoint card in the "Available Endpoints" section
2. Add new option in the dropdown
3. Add new test panel for parameters
4. Add new test function for the endpoint

Example:
```html
<!-- Add new endpoint card -->
<div class="endpoint" onclick="selectEndpoint('my-endpoint')">
    <span class="method post">POST</span>
    <div class="endpoint-path">/api/my-endpoint</div>
    <div class="endpoint-desc">Description here</div>
</div>
```

---

**Status: ✅ Ready to Use**

Your API now has a professional testing interface!

Open `http://localhost:5000/swagger` and start testing. 🚀
