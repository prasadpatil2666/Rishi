# Azure Cosmos DB API Integration

## Overview

The StayFocus API now includes two new endpoints that connect to **Azure Cosmos DB** to retrieve comprehensive review data from the `productivityflow` database's `Review` container.

## Configuration

### Connection Details
- **Endpoint**: `https://productivityflow.documents.azure.com:443/`
- **Database**: `productivityflow`
- **Container**: `Review`
- **Account Key**: Configured in `Program.cs`

### NuGet Package
```xml
<PackageReference Include="Microsoft.Azure.Cosmos" Version="3.36.0" />
```

## API Endpoints

### 1. Get All Reviews from Cosmos DB

**Endpoint**: `GET /api/cosmos/reviews`

**Description**: Retrieves all reviews stored in Azure Cosmos DB with comprehensive metadata.

**Request**:
```bash
curl -X GET "http://localhost:5000/api/cosmos/reviews"
```

**Response** (200 OK):
```json
{
  "count": 1,
  "data": [
    {
      "id": "rev_2026_x998877",
      "partitionKey": "IN_MH_Mumbai_Automotive",
      "schemaVersion": "2.1",
      "countryCode": "IN",
      "state_city": "MH_Mumbai",
      "categoryId": "Automotive",
      "location": {
        "type": "Point",
        "coordinates": [72.8777, 19.0760]
      },
      "address": {
        "area": "Andheri West",
        "zipCode": "400053",
        "landmark": "Near Infinity Mall"
      },
      "companyDetails": {
        "id": "comp_tata_001",
        "name": "Tata Motors Service Center",
        "globalBrandId": "tata_motors_global",
        "isVerifiedBrand": true,
        "brandLogoUrl": "https://cdn.viewdly.com/brands/tata_logo.png"
      },
      "reviewData": {
        "rating": 8.7,
        "title": "Excellent vehicle performance, but service turnaround needs work.",
        "comment": "The Safari is a beast on the highway! 10/10 for comfort...",
        "pros": ["Highway Stability", "Spacious Cabin", "Ventilated Seats", "Build Quality"],
        "cons": ["Service Delay", "Touchscreen Lag"],
        "detailedRatings": {
          "valueForMoney": 9.0,
          "buildQuality": 9.5,
          "customerService": 4.0,
          "deliveryExperience": 7.5,
          "reliability": 9.0,
          "performance": 10.0
        },
        "verification": {
          "isVerifiedPurchase": true,
          "purchaseDate": "2025-12-15T10:30:00Z",
          "usagePeriod": "3 Months",
          "verificationMethod": "Manual_Invoice_Check"
        }
      },
      "media": [
        {
          "type": "video",
          "url": "https://cdn.viewdly.com/reviews/vid_12345.mp4",
          "thumbnail": "https://cdn.viewdly.com/reviews/thumb_12345.jpg",
          "durationSeconds": 45,
          "aspectRatio": "9:16"
        },
        {
          "type": "image",
          "url": "https://cdn.viewdly.com/reviews/img_6789.jpg",
          "altText": "Dashboard of the new Safari"
        }
      ],
      "socialFeeds": {
        "instagram": {
          "postId": "C1xYz92abc",
          "embedUrl": "https://www.instagram.com/p/C1xYz92abc/",
          "syncStatus": "Synced"
        },
        "youtube": {
          "videoId": "dQw4w9WgXcQ",
          "videoUrl": "https://www.youtube.com/watch?v=dQw4w9WgXcQ"
        },
        "twitter": {
          "tweetId": "17654321098",
          "url": "https://twitter.com/user/status/17654321098"
        }
      },
      "engagement": {
        "viewCount": 45200,
        "helpfulCount": 1240,
        "reportCount": 0,
        "shareCount": {
          "whatsapp": 150,
          "facebook": 45,
          "twitter": 32,
          "directLink": 88
        }
      },
      "aiAnalytics": {
        "sentimentScore": 0.82,
        "sentimentLabel": "Positive",
        "detectedIssues": ["Wait Time", "Service Management"],
        "autoSummary": "User highly satisfied with vehicle build and highway performance...",
        "isSpam": false,
        "aiTrustScore": 0.98
      },
      "isEnquiry": true,
      "enquiryDetails": {
        "enquiryId": "ENQ_998877",
        "status": "In-Mediation",
        "priority": "High",
        "assignedAgent": "agent_mumbai_rahul",
        "lastUpdate": "2026-03-28T15:00:00Z",
        "slaDeadline": "2026-03-31T10:00:00Z"
      },
      "user": {
        "userId": "user_prasad_77",
        "displayName": "Prasad Patil",
        "isExpert": true,
        "profilePicture": "https://cdn.viewdly.com/users/prasad.jpg"
      },
      "tags": ["SUV", "RoadTrip", "ServiceExperience", "Automotive_India"],
      "timestamp": "2026-03-28T10:00:00Z",
      "_ts": 1711620000
    }
  ]
}
```

---

### 2. Get Review by ID from Cosmos DB

**Endpoint**: `GET /api/cosmos/reviews/{id}`

**Description**: Retrieves a specific review from Azure Cosmos DB using its unique ID.

**Parameters**:
- `id` (string, required): The unique review ID (e.g., `rev_2026_x998877`)

**Request**:
```bash
curl -X GET "http://localhost:5000/api/cosmos/reviews/rev_2026_x998877"
```

**Response** (200 OK):
```json
{
  "id": "rev_2026_x998877",
  "partitionKey": "IN_MH_Mumbai_Automotive",
  "schemaVersion": "2.1",
  "countryCode": "IN",
  "state_city": "MH_Mumbai",
  "categoryId": "Automotive",
  ...
  // Full review object as shown above
}
```

**Error Response** (404 Not Found):
```json
{
  "error": "Review not found"
}
```

---

## Data Model

### Top-Level Properties
| Property | Type | Description |
|----------|------|-------------|
| `id` | string | Unique review identifier |
| `partitionKey` | string | Cosmos DB partition key (Country_Region_Category) |
| `schemaVersion` | string | Data schema version |
| `countryCode` | string | ISO country code |
| `state_city` | string | State and city identifier |
| `categoryId` | string | Product/service category |
| `location` | Location | Geographic location with coordinates |
| `address` | Address | Physical address details |
| `companyDetails` | CompanyDetails | Company information |
| `reviewData` | ReviewData | Comprehensive review content |
| `media` | Media[] | Videos, images, audio files |
| `socialFeeds` | SocialFeeds | Social media integration |
| `engagement` | Engagement | View counts and interaction metrics |
| `aiAnalytics` | AiAnalytics | AI-generated insights |
| `isEnquiry` | boolean | Whether this is an enquiry/complaint |
| `enquiryDetails` | EnquiryDetails | Enquiry/complaint tracking info |
| `user` | User | Review author information |
| `tags` | string[] | Categorization tags |
| `timestamp` | string | Review creation timestamp |
| `_ts` | long | Cosmos DB timestamp |

### Key Sub-Objects

#### ReviewData
- `rating`: numeric (0-10)
- `title`: string
- `comment`: string
- `pros`: string[]
- `cons`: string[]
- `detailedRatings`: breakdown by criteria
- `verification`: purchase verification info

#### AiAnalytics
- `sentimentScore`: 0-1 (positive sentiment)
- `sentimentLabel`: "Positive", "Negative", "Neutral"
- `detectedIssues`: string[]
- `autoSummary`: AI-generated summary
- `isSpam`: boolean
- `aiTrustScore`: 0-1

#### Engagement
- `viewCount`: total views
- `helpfulCount`: helpful votes
- `reportCount`: abuse reports
- `shareCount`: breakdown by platform (WhatsApp, Facebook, Twitter, direct)

---

## Testing with Swagger UI

The API includes a beautiful dark-themed Swagger UI for testing:

1. Navigate to: `http://localhost:5000/swagger`
2. Select from dropdown:
   - **GET /api/cosmos/reviews** - Fetch all reviews
   - **GET /api/cosmos/reviews/{id}** - Fetch specific review
3. Enter the review ID (e.g., `rev_2026_x998877`)
4. Click "Send Request"
5. Use the **Copy** button to copy the JSON response

---

## Error Handling

### Common Error Responses

**400 Bad Request** - Invalid query or database connection issue:
```json
{
  "error": "Connection string error: ..."
}
```

**404 Not Found** - Review ID doesn't exist:
```json
{
  "error": "Review not found"
}
```

**500 Internal Server Error** - Server-side issue:
```json
{
  "error": "An unexpected error occurred"
}
```

---

## Code Structure

### Program.cs Configuration
```csharp
// Cosmos DB client registration
builder.Services.AddSingleton(new Microsoft.Azure.Cosmos.CosmosClient(
    "https://productivityflow.documents.azure.com:443/",
    "AGqhYPPQdf5CATeZZeHbT1zUsYywhjBs4yNkWWsrOE4hGg2xTBiistfhS6dt4FnDHFVOMFinQBieACDbPlC8WA=="
));

// Endpoints
app.MapGet("/api/cosmos/reviews", async (Microsoft.Azure.Cosmos.CosmosClient cosmosClient) => { ... });
app.MapGet("/api/cosmos/reviews/{id}", async (string id, Microsoft.Azure.Cosmos.CosmosClient cosmosClient) => { ... });
```

### Data Models (Located in Program.cs)
- `CosmosReviewDto` - Main review object
- `Location`, `Address` - Geographic data
- `CompanyDetails` - Company information
- `ReviewData`, `DetailedRatings`, `Verification` - Review content
- `Media` - Images, videos, audio
- `SocialFeeds` - Social media connections
- `Engagement` - Interaction metrics
- `AiAnalytics` - AI insights
- `EnquiryDetails` - Complaint tracking
- `User` - Author information

---

## Performance Considerations

- **Query All Reviews**: Returns all reviews from container (may be large)
- **Query by ID**: Direct item lookup (fastest)
- **Pagination**: Currently not implemented; consider adding for large datasets
- **Filtering**: Consider using Cosmos DB SQL queries for complex filters

### Suggested Future Enhancements
```csharp
// Add filtering endpoint
app.MapGet("/api/cosmos/reviews/category/{category}", async (string category, CosmosClient cosmosClient) => {
    // SQL query filtered by category
});

// Add pagination
app.MapGet("/api/cosmos/reviews/page/{page}", async (int page, CosmosClient cosmosClient) => {
    // OFFSET/LIMIT pagination
});

// Add sentiment filtering
app.MapGet("/api/cosmos/reviews/sentiment/{label}", async (string label, CosmosClient cosmosClient) => {
    // Filter by AI sentiment label
});
```

---

## Security Notes

⚠️ **Important**: The Cosmos DB connection string includes the account key and should be:
1. Moved to Azure Key Vault in production
2. Never hardcoded in source code
3. Rotated regularly
4. Not committed to version control

### Production Recommendation
```csharp
var cosmosConnectionString = builder.Configuration["CosmosDb:ConnectionString"];
builder.Services.AddSingleton(new Microsoft.Azure.Cosmos.CosmosClient(cosmosConnectionString));
```

Then store the connection string in `appsettings.json` (local) or Azure Key Vault (production).

---

## Related Endpoints

- `GET /api/reviews` - Local in-memory reviews
- `GET /api/reviews/{id}` - Local review by ID
- `POST /api/reviews` - Create local review
- `GET /api/weatherforecast` - Sample weather data
- `GET /swagger` - API testing UI
- `GET /openapi/v1.json` - OpenAPI specification

---

## Support & Documentation

For more information:
- [Azure Cosmos DB Documentation](https://docs.microsoft.com/azure/cosmos-db/)
- [Microsoft.Azure.Cosmos NuGet Package](https://www.nuget.org/packages/Microsoft.Azure.Cosmos/)
- [Cosmos DB SQL Query Reference](https://docs.microsoft.com/azure/cosmos-db/sql-query-getting-started)
