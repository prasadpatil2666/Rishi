# 🏗️ StayFocus API Architecture & Data Flow

## System Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                    Client Applications                      │
│  (Browser, Mobile App, Third-party Services, etc.)         │
└────────┬──────────────────────────────────────────────────┘
         │
         │ HTTP Requests
         │
         ▼
┌─────────────────────────────────────────────────────────────┐
│              StayFocus API (.NET 10)                        │
│                 localhost:5000                              │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│  Local Endpoints          │  Cosmos DB Endpoints            │
│  ─────────────────        │  ────────────────────           │
│  GET  /api/reviews        │  GET /api/cosmos/reviews        │
│  GET  /api/reviews/{id}   │  GET /api/cosmos/reviews/{id}   │
│  POST /api/reviews        │                                 │
│  GET  /api/weatherforecast│  Utility Endpoints              │
│                           │  ────────────────────           │
│  In-Memory Storage        │  GET /swagger                   │
│  (List<ReviewDto>)        │  GET /openapi/v1.json           │
│                           │                                 │
└─────────────────────────────────────────────────────────────┘
         │
         │ (Cosmos DB Endpoints only)
         │
         ▼
┌─────────────────────────────────────────────────────────────┐
│          Azure Cosmos DB                                    │
│     productivityflow database                               │
│         Review container                                    │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│  Documents:                                                 │
│  - rev_2026_x998877 (Tata Motors Review)                   │
│  - rev_2026_x998878 (Apple Review)                         │
│  - ... (more reviews)                                       │
│                                                             │
│  Fields per document:                                       │
│  - reviewData (rating, title, pros, cons)                  │
│  - companyDetails (name, brand, verification)             │
│  - aiAnalytics (sentiment, trust score)                    │
│  - engagement (views, helpful count, shares)              │
│  - media (videos, images, audio)                          │
│  - ... (20+ more fields)                                   │
│                                                             │
└─────────────────────────────────────────────────────────────┘
```

---

## Request Flow for Cosmos DB Endpoints

### GET /api/cosmos/reviews (All Reviews)

```
┌─────────────┐
│   Browser   │
└──────┬──────┘
       │ Click "Send Request"
       │ in Swagger UI
       ▼
┌──────────────────────────────────────┐
│  HTTP GET /api/cosmos/reviews        │
└──────────┬───────────────────────────┘
           │
           ▼
┌──────────────────────────────────────┐
│  ASP.NET Core Route Handler          │
│  (Program.cs line ~XXX)              │
└──────────┬───────────────────────────┘
           │
           ▼
┌──────────────────────────────────────┐
│  Dependency Injection                │
│  Gets CosmosClient from DI container │
└──────────┬───────────────────────────┘
           │
           ▼
┌──────────────────────────────────────┐
│  Get Database & Container            │
│  - Database: productivityflow        │
│  - Container: Review                 │
└──────────┬───────────────────────────┘
           │
           ▼
┌──────────────────────────────────────┐
│  Execute SQL Query                   │
│  "SELECT * FROM c"                   │
└──────────┬───────────────────────────┘
           │
           ▼
┌──────────────────────────────────────┐
│  Azure Cosmos DB Service             │
│  Query execution & data retrieval    │
└──────────┬───────────────────────────┘
           │
           ▼
┌──────────────────────────────────────┐
│  Parse Results                       │
│  Deserialize to List<CosmosReviewDto>│
└──────────┬───────────────────────────┘
           │
           ▼
┌──────────────────────────────────────┐
│  Build Response JSON                 │
│  {                                   │
│    "count": 1,                       │
│    "data": [ ... ]                   │
│  }                                   │
└──────────┬───────────────────────────┘
           │
           ▼
┌──────────────────────────────────────┐
│  HTTP 200 Response                   │
│  Content-Type: application/json      │
└──────────┬───────────────────────────┘
           │
           ▼
┌─────────────┐
│   Browser   │ ✅ Display in Swagger UI
│             │ ✅ User can copy response
└─────────────┘
```

---

### GET /api/cosmos/reviews/{id} (Single Review)

```
┌─────────────────────────────────────────────────────────────┐
│  User enters ID: "rev_2026_x998877" and clicks Send         │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│  HTTP GET /api/cosmos/reviews/rev_2026_x998877              │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│  Route Handler receives:                                    │
│  - Endpoint type: "cosmos-review"                           │
│  - ID parameter: "rev_2026_x998877"                         │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│  Cosmos DB Point Read (Direct Lookup)                       │
│  container.ReadItemAsync<CosmosReviewDto>(                  │
│    id: "rev_2026_x998877",                                  │
│    partitionKey: "rev_2026_x998877"                         │
│  )                                                           │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│  Database Returns:                                          │
│  ✅ If found: Complete document                            │
│  ❌ If not found: CosmosException (404)                    │
└──────────────────────┬──────────────────────────────────────┘
                       │
        ┌──────────────┴──────────────┐
        │                             │
    Found                        Not Found
        │                             │
        ▼                             ▼
  ┌──────────┐              ┌──────────────────┐
  │200 OK    │              │404 Not Found     │
  │Data      │              │{ "error": "..."}│
  └──────────┘              └──────────────────┘
```

---

## Data Structure Hierarchy

```
CosmosReviewDto
│
├─ Identification
│  ├─ id: "rev_2026_x998877"
│  ├─ partitionKey: "IN_MH_Mumbai_Automotive"
│  └─ schemaVersion: "2.1"
│
├─ Geographic Data
│  ├─ countryCode: "IN"
│  ├─ state_city: "MH_Mumbai"
│  ├─ location
│  │  ├─ type: "Point"
│  │  └─ coordinates: [72.8777, 19.0760]
│  └─ address
│     ├─ area: "Andheri West"
│     ├─ zipCode: "400053"
│     └─ landmark: "Near Infinity Mall"
│
├─ Company Information
│  └─ companyDetails
│     ├─ id: "comp_tata_001"
│     ├─ name: "Tata Motors Service Center"
│     ├─ globalBrandId: "tata_motors_global"
│     ├─ isVerifiedBrand: true
│     └─ brandLogoUrl: "https://..."
│
├─ Review Content
│  └─ reviewData
│     ├─ rating: 8.7
│     ├─ title: "Excellent vehicle performance..."
│     ├─ comment: "The Safari is a beast on..."
│     ├─ pros: ["Highway Stability", "Build Quality"]
│     ├─ cons: ["Service Delay", "Touchscreen Lag"]
│     ├─ detailedRatings
│     │  ├─ valueForMoney: 9.0
│     │  ├─ buildQuality: 9.5
│     │  ├─ customerService: 4.0
│     │  ├─ deliveryExperience: 7.5
│     │  ├─ reliability: 9.0
│     │  └─ performance: 10.0
│     └─ verification
│        ├─ isVerifiedPurchase: true
│        ├─ purchaseDate: "2025-12-15T10:30:00Z"
│        ├─ usagePeriod: "3 Months"
│        └─ verificationMethod: "Manual_Invoice_Check"
│
├─ Media
│  └─ media[]
│     ├─ type: "video" | "image" | "audio"
│     ├─ url: "https://cdn.viewdly.com/..."
│     ├─ thumbnail: "https://..." (for video)
│     ├─ altText: "..." (for image)
│     └─ durationSeconds: 45 (for audio/video)
│
├─ Social Integration
│  └─ socialFeeds
│     ├─ instagram
│     │  ├─ postId: "C1xYz92abc"
│     │  ├─ embedUrl: "https://www.instagram.com/..."
│     │  └─ syncStatus: "Synced"
│     ├─ youtube
│     │  ├─ videoId: "dQw4w9WgXcQ"
│     │  └─ videoUrl: "https://www.youtube.com/..."
│     └─ twitter
│        ├─ tweetId: "17654321098"
│        └─ url: "https://twitter.com/..."
│
├─ Engagement Metrics
│  └─ engagement
│     ├─ viewCount: 45200
│     ├─ helpfulCount: 1240
│     ├─ reportCount: 0
│     └─ shareCount
│        ├─ whatsapp: 150
│        ├─ facebook: 45
│        ├─ twitter: 32
│        └─ directLink: 88
│
├─ AI Analytics
│  └─ aiAnalytics
│     ├─ sentimentScore: 0.82 (0-1 scale)
│     ├─ sentimentLabel: "Positive" | "Negative" | "Neutral"
│     ├─ detectedIssues: ["Wait Time", "Service Management"]
│     ├─ autoSummary: "User highly satisfied with..."
│     ├─ isSpam: false
│     └─ aiTrustScore: 0.98
│
├─ Support Ticket
│  ├─ isEnquiry: true
│  └─ enquiryDetails
│     ├─ enquiryId: "ENQ_998877"
│     ├─ status: "In-Mediation"
│     ├─ priority: "High"
│     ├─ assignedAgent: "agent_mumbai_rahul"
│     ├─ lastUpdate: "2026-03-28T15:00:00Z"
│     └─ slaDeadline: "2026-03-31T10:00:00Z"
│
├─ User Information
│  └─ user
│     ├─ userId: "user_prasad_77"
│     ├─ displayName: "Prasad Patil"
│     ├─ isExpert: true
│     └─ profilePicture: "https://..."
│
└─ Metadata
   ├─ tags: ["SUV", "RoadTrip", "ServiceExperience"]
   ├─ timestamp: "2026-03-28T10:00:00Z"
   └─ _ts: 1711620000 (Cosmos DB timestamp)
```

---

## API Endpoints Map

```
┌─────────────────────────────────────────────────────────────┐
│                    StayFocus API                            │
│                   /api/** routes                            │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│  Local Storage Endpoints                                    │
│  ┌─────────────────────────────────────────────────────┐   │
│  │ GET    /reviews              - Get all local        │   │
│  │ GET    /reviews/{id}         - Get single local     │   │
│  │ POST   /reviews              - Create local         │   │
│  └─────────────────────────────────────────────────────┘   │
│                                                             │
│  Azure Cosmos DB Endpoints ⭐                              │
│  ┌─────────────────────────────────────────────────────┐   │
│  │ GET    /cosmos/reviews       - Get all from CosmosDB│   │
│  │ GET    /cosmos/reviews/{id}  - Get single from DB   │   │
│  └─────────────────────────────────────────────────────┘   │
│                                                             │
│  Sample/Demo Endpoints                                      │
│  ┌─────────────────────────────────────────────────────┐   │
│  │ GET    /weatherforecast      - Weather data         │   │
│  └─────────────────────────────────────────────────────┘   │
│                                                             │
│  UI & Documentation Endpoints                              │
│  ┌─────────────────────────────────────────────────────┐   │
│  │ GET    /swagger              - Beautiful UI         │   │
│  │ GET    /openapi/v1.json      - OpenAPI spec        │   │
│  └─────────────────────────────────────────────────────┘   │
│                                                             │
└─────────────────────────────────────────────────────────────┘
```

---

## Technology Stack

```
┌────────────────────────────────────────────┐
│         Client Layer                       │
│  - Browser (Swagger UI)                    │
│  - Mobile Apps                             │
│  - Third-party Services                    │
└─────────────────────────────────────────────┘
                   ▲
                   │ HTTP/JSON
                   ▼
┌────────────────────────────────────────────┐
│    ASP.NET Core API Layer (.NET 10)        │
│  - Minimal APIs                            │
│  - Dependency Injection                    │
│  - CORS Middleware                         │
│  - Native OpenAPI Support                  │
└─────────────────────────────────────────────┘
                   ▲
                   │ CosmosClient SDK
                   ▼
┌────────────────────────────────────────────┐
│    Azure Services                          │
│  - Cosmos DB (Document Storage)            │
│  - Connection via Account Key              │
└─────────────────────────────────────────────┘
```

---

## Deployment Architecture

```
┌──────────────────────────────────────────────────┐
│           Development Environment                │
│  localhost:5000 (local machine)                  │
│  - StayFocus API running                        │
│  - Blazor Frontend running                      │
│  - Direct Cosmos DB connection                  │
└──────────────────────────────────────────────────┘
                       ▼
┌──────────────────────────────────────────────────┐
│      Production Deployment (Future)              │
│  Azure App Service / AKS                        │
│  - API Container                               │
│  - Auto-scaling                                │
│  - Load Balancing                              │
└──────────────────────────────────────────────────┘
                       │
                       ▼
┌──────────────────────────────────────────────────┐
│           Azure Cosmos DB                        │
│  - productivityflow database                    │
│  - Review container                            │
│  - Global distribution                         │
│  - 99.99% SLA                                  │
└──────────────────────────────────────────────────┘
```

---

## Error Handling Flow

```
Request to /api/cosmos/reviews/{id}
         │
         ▼
    Try Block
         │
    ┌────┴────────────────────┐
    │                         │
Success              Exception Thrown
    │                         │
    ▼                         ▼
Return 200 OK        Catch Block
    +                    │
  Data              Is CosmosException?
                         │
                  ┌──────┴───────┐
                Yes             No
                  │              │
                  ▼              ▼
             StatusCode       Generic
             == 404?          Exception
                  │              │
             ┌────┴─────┐        ▼
           Yes         No     Return 400
             │          │      Bad Request
             ▼          ▼
          404          Other
        Not Found      Error
```

---

## Performance Characteristics

```
Operation           Time    Cost  Use Case
─────────────────────────────────────────────
GET all reviews     Slow    High  Dashboard, reporting
GET by ID (direct)  Fast    Low   Specific review lookup
POST create         Medium  Med   New review submission
Query with filter   Medium  Med   Search results
```

---

## Security Architecture

```
┌─────────────────────────────────────────────────┐
│           Public API Endpoint                   │
│  - CORS enabled (development)                  │
│  - No authentication required (development)    │
└──────────────────┬──────────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────────┐
│     Connection String (Currently Hardcoded)     │
│  ⚠️ TODO: Move to Azure Key Vault              │
└──────────────────┬──────────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────────┐
│        Azure Cosmos DB (Protected)              │
│  - Account key authentication                  │
│  - Network isolation                           │
│  - Encryption at rest & in transit             │
└─────────────────────────────────────────────────┘
```

---

## Summary

This architecture provides:
- ✅ Clean separation of concerns
- ✅ Scalable API design
- ✅ Direct Azure Cosmos DB integration
- ✅ Real-time data access
- ✅ Beautiful testing interface
- ⚠️ Security improvements needed for production
