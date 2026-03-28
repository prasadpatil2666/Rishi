# 🎬 Visual Guide - Testing the Cosmos DB API

## Step-by-Step Testing Guide

### Phase 1: Launch the API

#### Step 1.1: Open Terminal
```
PowerShell / Command Prompt / Terminal
```

#### Step 1.2: Navigate to API Project
```bash
cd "C:\Users\Prasad Patil\source\repos\Rishi\StayFocusAPI"
```

#### Step 1.3: Run the API
```bash
dotnet run
```

**Expected Output:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to exit.
```

---

### Phase 2: Access Swagger UI

#### Step 2.1: Open Browser
- Press: `Ctrl + T` (new tab)
- or open: Firefox, Chrome, Edge, Safari

#### Step 2.2: Navigate to Swagger UI
```
URL: http://localhost:5000/swagger
```

**What You Should See:**
```
┌─────────────────────────────────────────────────────┐
│            StayFocus API                            │
│       Professional API Testing & Documentation      │
│                                                     │
│  [OpenAPI Spec] [GitHub]                           │
│                                                     │
│  ┌──────────────────┬──────────────────────────┐   │
│  │ Available        │ Test API                 │   │
│  │ Endpoints        │                          │   │
│  │                  │ Select Endpoint:         │   │
│  │ • GET /api/...   │ [Dropdown ▼]             │   │
│  │ • GET /api/...   │                          │   │
│  │ • POST /api/...  │ [Send Request] [Copy]    │   │
│  │ • GET /cosmos... │                          │   │
│  │ • GET /cosmos... │ Response:                │   │
│  │                  │ ┌──────────────────────┐ │   │
│  │                  │ │ (empty)              │ │   │
│  │                  │ └──────────────────────┘ │   │
│  └──────────────────┴──────────────────────────┘   │
│                                                     │
│  StayFocus API v1.0 | RESTful Review System       │
│  Base URL: http://localhost:5000                  │
│  [OpenAPI 3.0] [.NET 10] [CORS Enabled]          │
└─────────────────────────────────────────────────────┘
```

---

### Phase 3: Test Cosmos DB Endpoints

#### Step 3.1: Select "Get All Reviews from Cosmos DB"

**Click Dropdown:**
```
Select Endpoint: [Dropdown ▼]
                 • GET /api/reviews
                 • GET /api/reviews/{id}
                 • POST /api/reviews
                 • GET /api/weatherforecast
                 • GET /api/cosmos/reviews (Azure Cosmos DB) ◄─ Click Here
                 • GET /api/cosmos/reviews/{id} (Azure Cosmos DB)
```

**Expected Result:**
- Dropdown closes
- A button appears: "🚀 Send Request - All Reviews"

#### Step 3.2: Send Request

**Click Button:**
```
[🚀 Send Request - All Reviews]
```

**Expected Timeline:**
```
0ms:   Button clicked
100ms: Spinner appears: "Loading..." with rotating icon
...
500ms: Response received
600ms: Response displayed
```

#### Step 3.3: View Response

**Success Response (Green):**
```
┌─────────────────────────────────────────────────────┐
│ ✓ Status: 200 OK                [Copy]             │
├─────────────────────────────────────────────────────┤
│ {                                                   │
│   "count": 1,                                       │
│   "data": [                                         │
│     {                                               │
│       "id": "rev_2026_x998877",                    │
│       "partitionKey": "IN_MH_Mumbai_Automotive",   │
│       "countryCode": "IN",                         │
│       "state_city": "MH_Mumbai",                   │
│       "categoryId": "Automotive",                  │
│       "reviewData": {                              │
│         "rating": 8.7,                            │
│         "title": "Excellent vehicle...",          │
│         "pros": [                                 │
│           "Highway Stability",                    │
│           "Build Quality",                        │
│           "Spacious Cabin"                        │
│         ],                                         │
│         "cons": [                                 │
│           "Service Delay",                        │
│           "Touchscreen Lag"                       │
│         ],                                         │
│         "detailedRatings": {                      │
│           "valueForMoney": 9.0,                  │
│           "buildQuality": 9.5,                   │
│           "customerService": 4.0,                │
│           "deliveryExperience": 7.5,             │
│           "reliability": 9.0,                    │
│           "performance": 10.0                    │
│         }                                         │
│       },                                           │
│       "engagement": {                             │
│         "viewCount": 45200,                       │
│         "helpfulCount": 1240,                     │
│         "reportCount": 0                          │
│       },                                           │
│       "aiAnalytics": {                            │
│         "sentimentScore": 0.82,                   │
│         "sentimentLabel": "Positive",             │
│         "aiTrustScore": 0.98                      │
│       },                                           │
│       ... (more fields)                           │
│     }                                              │
│   ]                                                │
│ }                                                  │
└─────────────────────────────────────────────────────┘
```

---

#### Step 3.4: Copy Response

**Click Copy Button:**
```
[Copy] Button → [✓ Copied!] (animated for 2 seconds)
```

**Result:**
- Entire JSON response copied to clipboard
- Can paste in text editor, Postman, etc.

---

#### Step 3.5: Test Single Review Endpoint

**Select from Dropdown:**
```
Select Endpoint: [Dropdown ▼]
                 ...
                 • GET /api/cosmos/reviews/{id} (Azure Cosmos DB) ◄─ Click Here
```

**Expected UI Change:**
```
Before:
┌────────────────────────────────┐
│ [Send Request - All Reviews]   │
└────────────────────────────────┘

After:
┌────────────────────────────────┐
│ Review ID (Partition Key):     │
│ [rev_2026_x998877]             │
│                                │
│ [Send Request]                 │
└────────────────────────────────┘
```

**Enter Review ID:**
```
Pre-filled with: rev_2026_x998877
(You can change this if testing other reviews)
```

**Click Send Request:**
```
[Send Request]
```

**Expected Response (200 OK):**
```
┌─────────────────────────────────────────────────────┐
│ ✓ Status: 200 OK                [Copy]             │
├─────────────────────────────────────────────────────┤
│ {                                                   │
│   "id": "rev_2026_x998877",                        │
│   "partitionKey": "IN_MH_Mumbai_Automotive",       │
│   "schemaVersion": "2.1",                          │
│   "countryCode": "IN",                             │
│   ... (full review object)                         │
│ }                                                   │
└─────────────────────────────────────────────────────┘
```

---

#### Step 3.6: Test with Invalid Review ID

**Modify Review ID:**
```
Review ID: [invalid_id_12345]
```

**Click Send Request:**
```
[Send Request]
```

**Expected Response (404 Not Found):**
```
┌─────────────────────────────────────────────────────┐
│ ✗ Status: 404 Not Found         [Copy]             │
├─────────────────────────────────────────────────────┤
│ {                                                   │
│   "error": "Review not found"                       │
│ }                                                   │
└─────────────────────────────────────────────────────┘
```

---

### Phase 4: Test Other Endpoints

#### Step 4.1: Test Local Endpoints
```
Select: GET /api/reviews
Result: Local in-memory reviews

Select: GET /api/reviews/{id}
Result: Local review by ID

Select: POST /api/reviews
Result: Create local review (form appears)

Select: GET /api/weatherforecast
Result: Sample weather data
```

---

### Phase 5: Advanced Testing with cURL

#### Option A: Get All Cosmos Reviews
```bash
curl -X GET "http://localhost:5000/api/cosmos/reviews"
```

#### Option B: Get Specific Review
```bash
curl -X GET "http://localhost:5000/api/cosmos/reviews/rev_2026_x998877"
```

#### Option C: Test with Invalid ID
```bash
curl -X GET "http://localhost:5000/api/cosmos/reviews/invalid_id"
```

**Expected Outputs:**
- Success: JSON with review data (HTTP 200)
- Not Found: `{"error":"Review not found"}` (HTTP 404)
- Error: `{"error":"..."}` (HTTP 400/500)

---

### Phase 6: Postman Testing

#### Step 6.1: Create New Request
- URL: `http://localhost:5000/api/cosmos/reviews`
- Method: `GET`
- Headers: (auto)

#### Step 6.2: Send Request
```
[Send]
```

#### Step 6.3: View Response
```
Status: 200 OK
Body: (formatted JSON)
Time: ~500ms
```

#### Step 6.4: Save for Later
```
[Save]
Collection: StayFocus API
Request Name: Get All Cosmos Reviews
```

---

## Troubleshooting Guide

### Issue 1: Cannot Connect to API

**Symptom:**
```
Connection refused / ERR_CONNECTION_REFUSED
```

**Solutions:**
1. Verify API is running: Check terminal for "listening on http://localhost:5000"
2. Check port 5000: `netstat -ano | findstr :5000`
3. Restart API: Press Ctrl+C, then `dotnet run` again

---

### Issue 2: Swagger UI Shows "Cannot GET /swagger"

**Symptom:**
```
404 Not Found: Cannot GET /swagger
```

**Solutions:**
1. Verify endpoint exists in Program.cs
2. Check URL: Should be `/swagger` not `/swagger/`
3. Rebuild: `dotnet build`

---

### Issue 3: Cosmos DB Connection Error

**Symptom:**
```
Error: Connection string error: Unable to connect
```

**Solutions:**
1. Verify connection string in Program.cs
2. Check internet connection
3. Verify Cosmos DB account is active
4. Check account key is valid

---

### Issue 4: Review Not Found (404)

**Symptom:**
```
{"error": "Review not found"}
```

**Solutions:**
1. Verify review ID is correct: `rev_2026_x998877`
2. Check Cosmos DB has data
3. Verify partition key matches
4. Try getting all reviews first

---

### Issue 5: Slow Response

**Symptom:**
```
Request takes >5 seconds
```

**Solutions:**
1. Check network connection
2. Monitor CPU/Memory usage
3. Try specific review (should be faster)
4. Restart API and try again

---

## Performance Benchmarks

| Endpoint | First Call | Subsequent | Network |
|----------|-----------|-----------|---------|
| Get All Reviews | 800ms | 600ms | ~50KB |
| Get by ID | 400ms | 300ms | ~30KB |
| Connection | ~200ms | ~150ms | Latency |

---

## Success Criteria Checklist

- [ ] API starts without errors
- [ ] Swagger UI loads at localhost:5000/swagger
- [ ] "Get All Reviews" returns data (200 OK)
- [ ] Response includes count and data array
- [ ] Copy button works
- [ ] "Get by ID" returns single review
- [ ] Invalid ID returns 404
- [ ] Valid ID returns 200 with full review
- [ ] Review data includes company, rating, sentiment
- [ ] All endpoints accessible from dropdown

---

## Data Validation Checklist

In the response, verify you see:

**Company Information:**
- [x] companyDetails.name
- [x] companyDetails.brandLogoUrl
- [x] companyDetails.isVerifiedBrand

**Review Content:**
- [x] reviewData.rating (0-10)
- [x] reviewData.title
- [x] reviewData.pros (array)
- [x] reviewData.cons (array)

**Detailed Ratings:**
- [x] buildQuality
- [x] valueForMoney
- [x] customerService
- [x] performance

**Engagement:**
- [x] viewCount
- [x] helpfulCount
- [x] shareCount

**AI Insights:**
- [x] sentimentScore (0-1)
- [x] sentimentLabel ("Positive", etc.)
- [x] aiTrustScore (0-1)

**Other Fields:**
- [x] location.coordinates
- [x] address.area
- [x] media[] (videos, images)
- [x] socialFeeds (Instagram, YouTube, Twitter)
- [x] user.displayName

---

## Next Steps After Successful Testing

1. **Verify Locally** ✓ (just completed)
2. **Deploy to Azure** → App Service
3. **Test in Staging** → Environment
4. **Load Test** → Performance
5. **Security Audit** → Vulnerabilities
6. **Deploy to Production** → Live

---

## Notes

- API runs on `http://localhost:5000`
- Swagger UI at `http://localhost:5000/swagger`
- OpenAPI spec at `http://localhost:5000/openapi/v1.json`
- No authentication required (development)
- CORS enabled for all origins (development)

---

**Happy Testing! 🚀**
