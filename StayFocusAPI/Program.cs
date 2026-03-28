var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// .NET 10 has native OpenAPI support built-in
builder.Services.AddOpenApi();

// Add CORS to allow Blazor frontend, mobile apps, and external clients
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

// Add Azure Cosmos DB client
builder.Services.AddSingleton(new Microsoft.Azure.Cosmos.CosmosClient(
    "https://productivityflow.documents.azure.com:443/",
    "AGqhYPPQdf5CATeZZeHbT1zUsYywhjBs4yNkWWsrOE4hGg2xTBiistfhS6dt4FnDHFVOMFinQBieACDbPlC8WA=="
));

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");

// Map OpenAPI for Swagger integration
app.MapOpenApi();

// Custom Swagger UI with beautiful dark theme
const string swaggerHtml = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>StayFocus API</title>
    <link href=""https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"" rel=""stylesheet"">
    <style>
        :root {
            --primary: #667eea;
            --primary-dark: #5568d3;
            --secondary: #764ba2;
            --success: #10b981;
            --warning: #f59e0b;
            --error: #ef4444;
            --accent: #c8973a;
            --bg-primary: #0f172a;
            --bg-secondary: #1e293b;
            --bg-tertiary: #334155;
            --text-primary: #f1f5f9;
            --text-secondary: #cbd5e1;
            --border: #475569;
        }

        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        html {
            scroll-behavior: smooth;
        }

        body {
            font-family: 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
            background: linear-gradient(135deg, var(--bg-primary) 0%, var(--bg-secondary) 100%);
            color: var(--text-primary);
            min-height: 100vh;
            line-height: 1.6;
        }

        .container {
            max-width: 1400px;
            margin: 0 auto;
            padding: 2rem;
        }

        /* Header */
        header {
            background: linear-gradient(135deg, rgba(102, 126, 234, 0.1) 0%, rgba(118, 75, 162, 0.1) 100%);
            backdrop-filter: blur(10px);
            border: 1px solid rgba(102, 126, 234, 0.2);
            border-radius: 16px;
            padding: 3rem;
            margin-bottom: 3rem;
            animation: slideDown 0.6s ease-out;
        }

        @keyframes slideDown {
            from {
                opacity: 0;
                transform: translateY(-30px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        header h1 {
            font-size: 2.5rem;
            background: linear-gradient(135deg, var(--primary) 0%, var(--secondary) 100%);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            background-clip: text;
            margin-bottom: 0.5rem;
            font-weight: 700;
        }

        header p {
            color: var(--text-secondary);
            font-size: 1.1rem;
            margin-bottom: 1.5rem;
        }

        .links {
            display: flex;
            gap: 1rem;
            flex-wrap: wrap;
        }

        .links a {
            display: inline-flex;
            align-items: center;
            gap: 0.5rem;
            padding: 0.75rem 1.5rem;
            background: var(--primary);
            color: white;
            text-decoration: none;
            border-radius: 8px;
            transition: all 0.3s ease;
            font-weight: 600;
            border: 2px solid transparent;
        }

        .links a:hover {
            background: transparent;
            border-color: var(--primary);
            color: var(--primary);
            transform: translateY(-2px);
        }

        /* Main Content */
        .main-content {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 2rem;
            margin-bottom: 3rem;
        }

        @media (max-width: 1024px) {
            .main-content {
                grid-template-columns: 1fr;
            }
        }

        .card {
            background: rgba(30, 41, 59, 0.6);
            backdrop-filter: blur(10px);
            border: 1px solid var(--border);
            border-radius: 12px;
            padding: 2.5rem;
            transition: all 0.3s ease;
            animation: fadeIn 0.6s ease-out;
            animation-fill-mode: both;
        }

        .card:nth-child(1) { animation-delay: 0.1s; }
        .card:nth-child(2) { animation-delay: 0.2s; }

        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(20px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        .card:hover {
            border-color: var(--primary);
            box-shadow: 0 0 30px rgba(102, 126, 234, 0.2);
        }

        .card h2 {
            font-size: 1.5rem;
            margin-bottom: 1.5rem;
            display: flex;
            align-items: center;
            gap: 0.75rem;
            color: var(--text-primary);
        }

        .card h2 i {
            color: var(--primary);
            font-size: 1.75rem;
        }

        /* Endpoints */
        .endpoint {
            background: rgba(51, 65, 85, 0.5);
            border: 1px solid rgba(102, 126, 234, 0.3);
            padding: 1.25rem;
            margin-bottom: 1rem;
            border-radius: 8px;
            cursor: pointer;
            transition: all 0.3s ease;
            position: relative;
            overflow: hidden;
        }

        .endpoint::before {
            content: '';
            position: absolute;
            top: 0;
            left: -100%;
            width: 100%;
            height: 100%;
            background: linear-gradient(90deg, transparent, rgba(102, 126, 234, 0.2), transparent);
            transition: left 0.5s ease;
        }

        .endpoint:hover::before {
            left: 100%;
        }

        .endpoint:hover {
            background: rgba(102, 126, 234, 0.1);
            border-color: var(--primary);
            transform: translateX(8px);
        }

        .method {
            display: inline-block;
            padding: 0.4rem 1rem;
            border-radius: 6px;
            font-weight: 700;
            font-size: 0.875rem;
            margin-right: 1rem;
            text-transform: uppercase;
            letter-spacing: 0.05em;
        }

        .method.get {
            background: rgba(16, 185, 129, 0.2);
            color: var(--success);
            border: 1px solid var(--success);
        }

        .method.post {
            background: rgba(59, 130, 246, 0.2);
            color: #3b82f6;
            border: 1px solid #3b82f6;
        }

        .endpoint-path {
            font-family: 'Courier New', monospace;
            font-weight: 700;
            color: var(--text-primary);
            margin: 0.75rem 0;
            font-size: 1rem;
            letter-spacing: 0.02em;
        }

        .endpoint-desc {
            color: var(--text-secondary);
            font-size: 0.95rem;
        }

        /* Form Styling */
        .test-section {
            margin-top: 1rem;
        }

        .test-section label {
            display: block;
            margin-bottom: 0.75rem;
            font-weight: 600;
            color: var(--text-primary);
            font-size: 0.95rem;
        }

        .test-section input,
        .test-section textarea,
        .test-section select {
            width: 100%;
            padding: 0.875rem;
            background: rgba(51, 65, 85, 0.5);
            border: 1px solid var(--border);
            border-radius: 8px;
            color: var(--text-primary);
            font-family: 'Courier New', monospace;
            font-size: 0.9rem;
            margin-bottom: 1rem;
            transition: all 0.3s ease;
        }

        .test-section input:focus,
        .test-section textarea:focus,
        .test-section select:focus {
            outline: none;
            border-color: var(--primary);
            background: rgba(102, 126, 234, 0.1);
            box-shadow: 0 0 10px rgba(102, 126, 234, 0.3);
        }

        .test-section button {
            background: linear-gradient(135deg, var(--primary) 0%, var(--secondary) 100%);
            color: white;
            border: none;
            padding: 0.875rem 2rem;
            border-radius: 8px;
            cursor: pointer;
            font-weight: 600;
            transition: all 0.3s ease;
            width: 100%;
            font-size: 1rem;
            text-transform: uppercase;
            letter-spacing: 0.05em;
            position: relative;
            overflow: hidden;
        }

        .test-section button::before {
            content: '';
            position: absolute;
            top: 0;
            left: -100%;
            width: 100%;
            height: 100%;
            background: rgba(255, 255, 255, 0.2);
            transition: left 0.5s ease;
        }

        .test-section button:hover {
            transform: translateY(-2px);
            box-shadow: 0 10px 20px rgba(102, 126, 234, 0.3);
        }

        .test-section button:hover::before {
            left: 100%;
        }

        /* Response Box */
        .response-container {
            margin-top: 1.5rem;
            display: none;
        }

        .response-container.active {
            display: block;
            animation: slideUp 0.3s ease-out;
        }

        .response-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            background: rgba(15, 23, 42, 0.9);
            border: 2px solid var(--border);
            border-bottom: none;
            border-radius: 8px 8px 0 0;
            padding: 1rem 1.5rem;
            font-weight: 700;
            font-size: 1rem;
        }

        .status-code {
            display: flex;
            align-items: center;
            gap: 0.5rem;
        }

        .response-header.success {
            border-color: var(--success);
            background: linear-gradient(135deg, rgba(16, 185, 129, 0.15) 0%, rgba(16, 185, 129, 0.05) 100%);
        }

        .response-header.success .status-code {
            color: var(--success);
        }

        .response-header.error {
            border-color: var(--error);
            background: linear-gradient(135deg, rgba(239, 68, 68, 0.15) 0%, rgba(239, 68, 68, 0.05) 100%);
        }

        .response-header.error .status-code {
            color: var(--error);
        }

        .copy-btn {
            display: flex;
            align-items: center;
            gap: 0.5rem;
            background: var(--primary);
            color: white;
            border: none;
            padding: 0.5rem 1rem;
            border-radius: 6px;
            cursor: pointer;
            font-size: 0.85rem;
            font-weight: 600;
            transition: all 0.3s ease;
        }

        .copy-btn:hover {
            background: var(--primary-dark);
            transform: scale(1.05);
        }

        .copy-btn.copied {
            background: var(--success);
        }

        .response-box {
            background: rgba(15, 23, 42, 0.95);
            border: 2px solid var(--border);
            border-top: none;
            border-radius: 0 0 8px 8px;
            padding: 1.5rem;
            font-family: 'Courier New', monospace;
            font-size: 0.875rem;
            color: var(--text-secondary);
            max-height: 450px;
            overflow-y: auto;
            line-height: 1.8;
            word-break: break-word;
            white-space: pre-wrap;
        }

        .response-box.success {
            border-color: var(--success);
            background: linear-gradient(135deg, rgba(15, 23, 42, 0.98) 0%, rgba(16, 185, 129, 0.05) 100%);
        }

        .response-box.error {
            border-color: var(--error);
            background: linear-gradient(135deg, rgba(15, 23, 42, 0.98) 0%, rgba(239, 68, 68, 0.05) 100%);
        }

        .response-box pre {
            margin: 0;
            font-family: 'Courier New', monospace;
        }

        @keyframes slideUp {
            from {
                opacity: 0;
                transform: translateY(20px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        /* Syntax highlighting for JSON */
        .json-key { color: #667eea; }
        .json-string { color: #10b981; }
        .json-number { color: #f59e0b; }
        .json-boolean { color: #c8973a; }
        .json-null { color: #cbd5e1; }

        /* Footer */
        .footer {
            background: rgba(30, 41, 59, 0.6);
            backdrop-filter: blur(10px);
            border: 1px solid var(--border);
            border-radius: 12px;
            padding: 2.5rem;
            text-align: center;
            animation: fadeIn 0.6s ease-out 0.3s both;
        }

        .footer p {
            margin-bottom: 1rem;
            color: var(--text-secondary);
        }

        .footer code {
            background: rgba(102, 126, 234, 0.1);
            border: 1px solid rgba(102, 126, 234, 0.3);
            padding: 0.5rem 1rem;
            border-radius: 6px;
            font-family: 'Courier New', monospace;
            color: var(--primary);
        }

        .badge {
            display: inline-block;
            padding: 0.5rem 1rem;
            background: linear-gradient(135deg, rgba(102, 126, 234, 0.2), rgba(118, 75, 162, 0.2));
            border: 1px solid var(--primary);
            color: var(--primary);
            border-radius: 20px;
            font-size: 0.875rem;
            font-weight: 600;
            margin-right: 0.75rem;
            margin-bottom: 0.5rem;
            display: inline-block;
        }

        /* Scrollbar */
        ::-webkit-scrollbar {
            width: 8px;
        }

        ::-webkit-scrollbar-track {
            background: rgba(30, 41, 59, 0.5);
        }

        ::-webkit-scrollbar-thumb {
            background: var(--primary);
            border-radius: 4px;
        }

        ::-webkit-scrollbar-thumb:hover {
            background: var(--secondary);
        }
    </style>
</head>
<body>
    <div class=""container"">
        <header>
            <h1><i class=""fas fa-rocket""></i> StayFocus API</h1>
            <p>Professional API Testing & Documentation Hub</p>
            <div class=""links"">
                <a href=""/openapi/v1.json"" target=""_blank""><i class=""fas fa-file-alt""></i> OpenAPI Spec</a>
                <a href=""https://github.com/prasadpatil2666/Rishi"" target=""_blank""><i class=""fab fa-github""></i> GitHub</a>
            </div>
        </header>

        <div class=""main-content"">
            <div class=""card"">
                <h2><i class=""fas fa-list""></i> Available Endpoints</h2>
                <div class=""endpoint"" onclick=""selectEndpoint('get-reviews')"">
                    <span class=""method get""><i class=""fas fa-arrow-down""></i> GET</span>
                    <div class=""endpoint-path"">/api/reviews</div>
                    <div class=""endpoint-desc"">Retrieve all reviews from the database</div>
                </div>
                <div class=""endpoint"" onclick=""selectEndpoint('get-review')"">
                    <span class=""method get""><i class=""fas fa-arrow-down""></i> GET</span>
                    <div class=""endpoint-path"">/api/reviews/{id}</div>
                    <div class=""endpoint-desc"">Retrieve a specific review by its ID</div>
                </div>
                <div class=""endpoint"" onclick=""selectEndpoint('post-review')"">
                    <span class=""method post""><i class=""fas fa-arrow-up""></i> POST</span>
                    <div class=""endpoint-path"">/api/reviews</div>
                    <div class=""endpoint-desc"">Create a new review submission</div>
                </div>
                <div class=""endpoint"" onclick=""selectEndpoint('get-weather')"">
                    <span class=""method get""><i class=""fas fa-arrow-down""></i> GET</span>
                    <div class=""endpoint-path"">/api/weatherforecast</div>
                    <div class=""endpoint-desc"">Sample weather forecast data endpoint</div>
                </div>
                <div class=""endpoint"" onclick=""selectEndpoint('cosmos-reviews')"">
                    <span class=""method get""><i class=""fas fa-database""></i> GET</span>
                    <div class=""endpoint-path"">/api/cosmos/reviews</div>
                    <div class=""endpoint-desc"">Fetch all reviews from Azure Cosmos DB</div>
                </div>
                <div class=""endpoint"" onclick=""selectEndpoint('cosmos-review')"">
                    <span class=""method get""><i class=""fas fa-database""></i> GET</span>
                    <div class=""endpoint-path"">/api/cosmos/reviews/{id}</div>
                    <div class=""endpoint-desc"">Fetch specific review from Cosmos DB by ID</div>
                </div>

            <div class=""card"">
                <h2><i class=""fas fa-flask""></i> Test API</h2>
                <div id=""test-panel"">
                    <div class=""test-section"">
                        <label><i class=""fas fa-tasks""></i> Select Endpoint:</label>
                        <select id=""endpoint-select"" onchange=""handleEndpointSelect()"">
                            <option value="""">-- Choose an endpoint --</option>
                            <option value=""get-reviews"">GET /api/reviews</option>
                            <option value=""get-review"">GET /api/reviews/{id}</option>
                            <option value=""post-review"">POST /api/reviews</option>
                            <option value=""get-weather"">GET /api/weatherforecast</option>
                            <option value=""cosmos-reviews"">GET /api/cosmos/reviews (Azure Cosmos DB)</option>
                            <option value=""cosmos-review"">GET /api/cosmos/reviews/{id} (Azure Cosmos DB)</option>
                        </select>
                    </div>
                    <div id=""get-reviews-params"" style=""display: none;"">
                        <button onclick=""testEndpoint('get-reviews')""><i class=""fas fa-paper-plane""></i> Send Request</button>
                    </div>
                    <div id=""get-review-params"" style=""display: none;"">
                        <label><i class=""fas fa-id-card""></i> Review ID:</label>
                        <input type=""number"" id=""review-id"" placeholder=""Enter review ID"" value=""1"">
                        <button onclick=""testEndpoint('get-review')""><i class=""fas fa-paper-plane""></i> Send Request</button>
                    </div>
                    <div id=""post-review-params"" style=""display: none;"">
                        <label><i class=""fas fa-building""></i> Company Name:</label>
                        <input type=""text"" id=""company"" placeholder=""e.g., Apple, Microsoft"" value=""Tesla"">
                        <label><i class=""fas fa-star""></i> Rating (0-5):</label>
                        <input type=""number"" id=""rating"" placeholder=""e.g., 4.5"" min=""0"" max=""5"" step=""0.1"" value=""4.7"">
                        <label><i class=""fas fa-heading""></i> Title:</label>
                        <input type=""text"" id=""title"" placeholder=""e.g., Excellent Service"" value=""Innovative"">
                        <label><i class=""fas fa-comment""></i> Content:</label>
                        <textarea id=""content"" placeholder=""Share your detailed review..."" rows=""3"">Great technology and design.</textarea>
                        <button onclick=""testEndpoint('post-review')""><i class=""fas fa-paper-plane""></i> Send Request</button>
                    </div>
                    <div id=""get-weather-params"" style=""display: none;"">
                        <button onclick=""testEndpoint('get-weather')""><i class=""fas fa-paper-plane""></i> Send Request</button>
                    </div>
                    <div id=""cosmos-reviews-params"" style=""display: none;"">
                        <button onclick=""testEndpoint('cosmos-reviews')""><i class=""fas fa-paper-plane""></i> Send Request - All Reviews</button>
                    </div>
                    <div id=""cosmos-review-params"" style=""display: none;"">
                        <label><i class=""fas fa-database""></i> Review ID (Partition Key):</label>
                        <input type=""text"" id=""cosmos-review-id"" placeholder=""e.g., rev_2026_x998877"" value=""rev_2026_x998877"">
                        <button onclick=""testEndpoint('cosmos-review')""><i class=""fas fa-paper-plane""></i> Send Request</button>
                    </div>
                    <div id=""response-container"" class=""response-container"">
                        <div id=""response-header"" class=""response-header"">
                            <div class=""status-code"" id=""status-display""></div>
                            <button class=""copy-btn"" onclick=""copyResponse()"" id=""copy-btn""><i class=""fas fa-copy""></i> Copy</button>
                        </div>
                        <div id=""response"" class=""response-box""></div>
                    </div>
                </div>
            </div>
        </div>

        <div class=""footer"">
            <p><strong>StayFocus API v1.0</strong> | RESTful Review Management System</p>
            <p>Base URL: <code>http://localhost:5000</code></p>
            <div>
                <span class=""badge""><i class=""fas fa-cube""></i> OpenAPI 3.0</span>
                <span class=""badge""><i class=""fab fa-microsoft""></i> .NET 10</span>
                <span class=""badge""><i class=""fas fa-shield-alt""></i> CORS Enabled</span>
            </div>
        </div>
    </div>

    <script>
        const API_BASE = window.location.origin;
        let lastResponse = null;

        function selectEndpoint(e) { 
            document.getElementById('endpoint-select').value = e; 
            handleEndpointSelect(); 
        }

        function handleEndpointSelect() {
            const v = document.getElementById('endpoint-select').value;
            document.querySelectorAll('[id$=""-params""]').forEach(el => el.style.display = 'none');
            if (v === 'get-reviews') document.getElementById('get-reviews-params').style.display = 'block';
            else if (v === 'get-review') document.getElementById('get-review-params').style.display = 'block';
            else if (v === 'post-review') document.getElementById('post-review-params').style.display = 'block';
            else if (v === 'get-weather') document.getElementById('get-weather-params').style.display = 'block';
            else if (v === 'cosmos-reviews') document.getElementById('cosmos-reviews-params').style.display = 'block';
            else if (v === 'cosmos-review') document.getElementById('cosmos-review-params').style.display = 'block';
        }

        function copyResponse() {
            if (!lastResponse) return;
            navigator.clipboard.writeText(lastResponse).then(() => {
                const btn = document.getElementById('copy-btn');
                btn.classList.add('copied');
                btn.innerHTML = '<i class=""fas fa-check""></i> Copied!';
                setTimeout(() => {
                    btn.classList.remove('copied');
                    btn.innerHTML = '<i class=""fas fa-copy""></i> Copy';
                }, 2000);
            }).catch(err => console.error('Copy failed:', err));
        }

        async function testEndpoint(type) {
            const container = document.getElementById('response-container');
            const header = document.getElementById('response-header');
            const statusDisplay = document.getElementById('status-display');
            const responseBox = document.getElementById('response');

            container.classList.remove('active');
            header.classList.remove('success', 'error');
            responseBox.classList.remove('success', 'error');
            statusDisplay.innerHTML = '<i class=""fas fa-spinner fa-spin""></i> Sending request...';
            responseBox.innerHTML = '';
            container.classList.add('active');

            try {
                let url, options;
                switch (type) {
                    case 'get-reviews':
                        url = `${API_BASE}/api/reviews`;
                        options = { method: 'GET' };
                        break;
                    case 'get-review':
                        url = `${API_BASE}/api/reviews/${document.getElementById('review-id').value}`;
                        options = { method: 'GET' };
                        break;
                    case 'post-review':
                        url = `${API_BASE}/api/reviews`;
                        options = {
                            method: 'POST',
                            headers: { 'Content-Type': 'application/json' },
                            body: JSON.stringify({
                                company: document.getElementById('company').value,
                                rating: parseFloat(document.getElementById('rating').value),
                                title: document.getElementById('title').value,
                                content: document.getElementById('content').value
                            })
                        };
                        break;
                    case 'get-weather':
                        url = `${API_BASE}/api/weatherforecast`;
                        options = { method: 'GET' };
                        break;
                    case 'cosmos-reviews':
                        url = `${API_BASE}/api/cosmos/reviews`;
                        options = { method: 'GET' };
                        break;
                    case 'cosmos-review':
                        url = `${API_BASE}/api/cosmos/reviews/${document.getElementById('cosmos-review-id').value}`;
                        options = { method: 'GET' };
                        break;
                }
                const response = await fetch(url, options);
                const data = await response.json();
                const statusClass = response.ok ? 'success' : 'error';
                const icon = response.ok ? '<i class=""fas fa-check-circle""></i>' : '<i class=""fas fa-times-circle""></i>';
                const statusText = response.ok ? '200 OK' : `${response.status} ${response.statusText}`;

                lastResponse = JSON.stringify(data, null, 2);
                statusDisplay.innerHTML = `${icon} ${statusText}`;
                responseBox.innerHTML = `<pre>${lastResponse}</pre>`;

                header.classList.add(statusClass);
                responseBox.classList.add(statusClass);
            } catch (error) {
                lastResponse = error.message;
                statusDisplay.innerHTML = `<i class=""fas fa-exclamation-circle""></i> Error`;
                responseBox.innerHTML = `<pre>Error: ${error.message}</pre>`;
                header.classList.add('error');
                responseBox.classList.add('error');
            }
        }
    </script>
</body>
</html>";


app.MapGet("/swagger", (HttpContext context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    return Results.Content(swaggerHtml, "text/html");
});

// Sample data
var reviews = new List<ReviewDto>
{
    new ReviewDto { Id = 1, Company = "Tesla", Rating = 4.7f, Title = "Innovative", Content = "Great technology and design." },
    new ReviewDto { Id = 2, Company = "Apple", Rating = 4.5f, Title = "Premium", Content = "High-quality products and ecosystem." },
    new ReviewDto { Id = 3, Company = "Microsoft", Rating = 4.6f, Title = "Reliable", Content = "Enterprise-grade solutions." }
};

// API Endpoints
app.MapGet("/api/reviews", () => Results.Ok(reviews));
app.MapGet("/api/reviews/{id}", (int id) => reviews.FirstOrDefault(r => r.Id == id) is ReviewDto review ? Results.Ok(review) : Results.NotFound());
app.MapPost("/api/reviews", (ReviewDto review) =>
{
    review.Id = reviews.Count > 0 ? reviews.Max(r => r.Id) + 1 : 1;
    reviews.Add(review);
    return Results.Created($"/api/reviews/{review.Id}", review);
});

app.MapGet("/api/weatherforecast", () => Results.Ok(new[] {
    new { date = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"), temperatureC = 20, summary = "Sunny" },
    new { date = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd"), temperatureC = 18, summary = "Cloudy" },
    new { date = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd"), temperatureC = 15, summary = "Rainy" }
}));

// Cosmos DB endpoint - Get all reviews from Azure Cosmos DB
app.MapGet("/api/cosmos/reviews", async (Microsoft.Azure.Cosmos.CosmosClient cosmosClient) =>
{
    try
    {
        var database = cosmosClient.GetDatabase("productivityflow");
        var container = database.GetContainer("Review");

        var query = "SELECT * FROM c";
        var iterator = container.GetItemQueryIterator<CosmosReviewDto>(query);

        var reviews = new List<CosmosReviewDto>();
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            reviews.AddRange(response);
        }

        return Results.Ok(new { count = reviews.Count, data = reviews });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

// Cosmos DB endpoint - Get review by ID
app.MapGet("/api/cosmos/reviews/{id}", async (string id, Microsoft.Azure.Cosmos.CosmosClient cosmosClient) =>
{
    try
    {
        var database = cosmosClient.GetDatabase("productivityflow");
        var container = database.GetContainer("Review");

        var response = await container.ReadItemAsync<CosmosReviewDto>(id, new Microsoft.Azure.Cosmos.PartitionKey(id));
        return Results.Ok(response.Resource);
    }
    catch (Microsoft.Azure.Cosmos.CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
    {
        return Results.NotFound(new { error = "Review not found" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.Run();

// Data models
record ReviewDto
{
    public int Id { get; set; }
    public string Company { get; set; }
    public float Rating { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}

record WeatherForecast(DateTime Date, int TemperatureC, string Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

// Azure Cosmos DB Review Model
public class CosmosReviewDto
{
    public string? id { get; set; }
    public string? partitionKey { get; set; }
    public string? schemaVersion { get; set; }
    public string? countryCode { get; set; }
    public string? state_city { get; set; }
    public string? categoryId { get; set; }
    public Location? location { get; set; }
    public Address? address { get; set; }
    public CompanyDetails? companyDetails { get; set; }
    public ReviewData? reviewData { get; set; }
    public List<Media>? media { get; set; }
    public SocialFeeds? socialFeeds { get; set; }
    public Engagement? engagement { get; set; }
    public AiAnalytics? aiAnalytics { get; set; }
    public bool isEnquiry { get; set; }
    public EnquiryDetails? enquiryDetails { get; set; }
    public User? user { get; set; }
    public List<string>? tags { get; set; }
    public string? timestamp { get; set; }
    public long _ts { get; set; }
}

public class Location
{
    public string? type { get; set; }
    public List<double>? coordinates { get; set; }
}

public class Address
{
    public string? area { get; set; }
    public string? zipCode { get; set; }
    public string? landmark { get; set; }
}

public class CompanyDetails
{
    public string? id { get; set; }
    public string? name { get; set; }
    public string? globalBrandId { get; set; }
    public bool isVerifiedBrand { get; set; }
    public string? brandLogoUrl { get; set; }
}

public class ReviewData
{
    public double rating { get; set; }
    public string? title { get; set; }
    public string? comment { get; set; }
    public List<string>? pros { get; set; }
    public List<string>? cons { get; set; }
    public DetailedRatings? detailedRatings { get; set; }
    public Verification? verification { get; set; }
}

public class DetailedRatings
{
    public double valueForMoney { get; set; }
    public double buildQuality { get; set; }
    public double customerService { get; set; }
    public double deliveryExperience { get; set; }
    public double reliability { get; set; }
    public double performance { get; set; }
}

public class Verification
{
    public bool isVerifiedPurchase { get; set; }
    public string? purchaseDate { get; set; }
    public string? usagePeriod { get; set; }
    public string? verificationMethod { get; set; }
}

public class Media
{
    public string? type { get; set; }
    public string? url { get; set; }
    public string? thumbnail { get; set; }
    public int? durationSeconds { get; set; }
    public string? aspectRatio { get; set; }
    public string? altText { get; set; }
    public string? label { get; set; }
}

public class SocialFeeds
{
    public Instagram? instagram { get; set; }
    public YouTube? youtube { get; set; }
    public Twitter? twitter { get; set; }
}

public class Instagram
{
    public string? postId { get; set; }
    public string? embedUrl { get; set; }
    public string? syncStatus { get; set; }
}

public class YouTube
{
    public string? videoId { get; set; }
    public string? videoUrl { get; set; }
}

public class Twitter
{
    public string? tweetId { get; set; }
    public string? url { get; set; }
}

public class Engagement
{
    public int viewCount { get; set; }
    public int helpfulCount { get; set; }
    public int reportCount { get; set; }
    public ShareCount? shareCount { get; set; }
}

public class ShareCount
{
    public int whatsapp { get; set; }
    public int facebook { get; set; }
    public int twitter { get; set; }
    public int directLink { get; set; }
}

public class AiAnalytics
{
    public double sentimentScore { get; set; }
    public string? sentimentLabel { get; set; }
    public List<string>? detectedIssues { get; set; }
    public string? autoSummary { get; set; }
    public bool isSpam { get; set; }
    public double aiTrustScore { get; set; }
}

public class EnquiryDetails
{
    public string? enquiryId { get; set; }
    public string? status { get; set; }
    public string? priority { get; set; }
    public string? assignedAgent { get; set; }
    public string? lastUpdate { get; set; }
    public string? slaDeadline { get; set; }
}

public class User
{
    public string? userId { get; set; }
    public string? displayName { get; set; }
    public bool isExpert { get; set; }
    public string? profilePicture { get; set; }
}
