# 🎯 Quick Reference - Cosmos DB API Endpoints

## API Endpoints Overview

### Cosmos DB Endpoints (NEW ⭐)

#### 1️⃣ Get All Reviews
```
GET /api/cosmos/reviews
```
- **Returns:** All reviews from Azure Cosmos DB
- **Parameters:** None
- **Response:** `{ "count": number, "data": [...] }`
- **Status:** 200 OK on success

**cURL Example:**
```bash
curl -X GET "http://localhost:5000/api/cosmos/reviews"
```

---

#### 2️⃣ Get Review by ID
```
GET /api/cosmos/reviews/{id}
```
- **Returns:** Single review by ID
- **Parameters:** `id` - review identifier (e.g., `rev_2026_x998877`)
- **Response:** Complete review object
- **Status:** 200 OK on success, 404 if not found

**cURL Example:**
```bash
curl -X GET "http://localhost:5000/api/cosmos/reviews/rev_2026_x998877"
```

---

## Local Endpoints (Existing)

```
GET  /api/reviews              - Get all local reviews
GET  /api/reviews/{id}         - Get local review by ID
POST /api/reviews              - Create new review
GET  /api/weatherforecast      - Sample weather data
```

---

## Testing with Swagger UI

**URL:** `http://localhost:5000/swagger`

**Steps:**
1. Open browser to Swagger UI
2. Select endpoint from dropdown
3. Enter parameters if needed
4. Click "Send Request"
5. View formatted response
6. Click "Copy" to copy to clipboard

---

## Response Status Codes

| Code | Meaning |
|------|---------|
| 200 | ✅ Success |
| 400 | ❌ Bad Request / Connection Error |
| 404 | ❌ Not Found |
| 500 | ❌ Server Error |

---

## Sample Responses

### GET /api/cosmos/reviews (All Reviews)
```json
{
  "count": 1,
  "data": [
    {
      "id": "rev_2026_x998877",
      "partitionKey": "IN_MH_Mumbai_Automotive",
      "categoryId": "Automotive",
      "reviewData": {
        "rating": 8.7,
        "title": "Excellent vehicle...",
        "pros": ["Highway Stability", "Build Quality"],
        "cons": ["Service Delay"]
      },
      "engagement": {
        "viewCount": 45200,
        "helpfulCount": 1240
      },
      "aiAnalytics": {
        "sentimentScore": 0.82,
        "sentimentLabel": "Positive"
      }
      // ... more fields
    }
  ]
}
```

### GET /api/cosmos/reviews/{id} (Single Review)
```json
{
  "id": "rev_2026_x998877",
  "partitionKey": "IN_MH_Mumbai_Automotive",
  // ... full review object
}
```

---

## Key Fields Explained

| Field | Type | Purpose |
|-------|------|---------|
| `id` | string | Unique review ID |
| `rating` | 0-10 | Overall rating |
| `sentimentScore` | 0-1 | AI sentiment (0=negative, 1=positive) |
| `viewCount` | number | Total views |
| `pros` | string[] | What customer liked |
| `cons` | string[] | What customer disliked |
| `detailedRatings` | object | Multi-criteria breakdown |
| `isVerifiedPurchase` | boolean | Genuine customer? |

---

## Cosmos DB Configuration

- **Endpoint:** `https://productivityflow.documents.azure.com:443/`
- **Database:** `productivityflow`
- **Container:** `Review`
- **Account Key:** Configured in Program.cs

---

## Common Use Cases

### Get high-rated reviews
```bash
# Get all reviews, then filter client-side for rating >= 8.5
curl -X GET "http://localhost:5000/api/cosmos/reviews"
```

### Get specific customer review
```bash
# Use review ID from search/admin panel
curl -X GET "http://localhost:5000/api/cosmos/reviews/rev_2026_x998877"
```

### Monitor sentiment
```bash
# Get all reviews and check aiAnalytics.sentimentScore
curl -X GET "http://localhost:5000/api/cosmos/reviews"
```

### Track engagement
```bash
# Get all reviews and sum engagement.viewCount
curl -X GET "http://localhost:5000/api/cosmos/reviews"
```

---

## Error Handling

### Not Found (404)
```json
{
  "error": "Review not found"
}
```

### Connection Error (400)
```json
{
  "error": "Connection string error: ..."
}
```

---

## Data Model Structure

```
CosmosReviewDto
├── Basic Info (id, partition key, schema version)
├── Location (coordinates, address)
├── CompanyDetails (name, brand, verification)
├── ReviewData (rating, title, pros/cons)
├── Media (videos, images, audio)
├── SocialFeeds (Instagram, YouTube, Twitter)
├── Engagement (views, helpful, shares)
├── AiAnalytics (sentiment, issues, summary)
├── EnquiryDetails (support ticket info)
├── User (reviewer info)
└── Tags & Metadata
```

---

## Quick Start

### 1. Run API
```bash
cd StayFocusAPI
dotnet run
```

### 2. Open Swagger
```
http://localhost:5000/swagger
```

### 3. Test Cosmos DB Endpoints
- Select "GET /api/cosmos/reviews" from dropdown
- Click "Send Request"
- See results in formatted JSON
- Click "Copy" to copy response

### 4. Test with cURL
```bash
curl -X GET "http://localhost:5000/api/cosmos/reviews"
```

---

## Troubleshooting

| Issue | Solution |
|-------|----------|
| Connection failed | Check Cosmos DB account key and endpoint |
| 404 Not Found | Verify review ID exists in database |
| Slow response | Consider pagination for large datasets |
| CORS error | Verify CORS is enabled (it is by default) |

---

## Next Steps

✅ Current: Basic GET endpoints  
📋 Planned: Filtering by category, sentiment, country  
📊 Planned: Aggregation and analytics endpoints  
🔍 Planned: Search functionality  
📄 Planned: Pagination support  

---

## Resources

- Full docs: `StayFocusAPI/COSMOS_DB_API.md`
- Cosmos DB docs: https://docs.microsoft.com/azure/cosmos-db/
- SDK reference: https://www.nuget.org/packages/Microsoft.Azure.Cosmos/

---

**Last Updated:** 2026-03-28  
**Status:** ✅ Production Ready
