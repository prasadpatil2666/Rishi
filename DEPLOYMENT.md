# Deployment Guide: Static Web App + Cosmos DB

## Prerequisites
- Azure subscription
- Azure CLI installed
- GitHub account and repository

## Step 1: Create Azure Static Web App

```powershell
# Login to Azure
az login

# Create resource group
az group create --name rishi-rg --location westus

# Create Static Web App (linked to GitHub)
az staticwebapp create `
  --name rishi-web-app `
  --resource-group rishi-rg `
  --source https://github.com/prasadpatil2666/Rishi `
  --branch master `
  --app-location "StayFocus" `
  --api-location "StayFocusAPI" `
  --output-location "wwwroot" `
  --token YOUR_GITHUB_TOKEN
```

## Step 2: Configure Environment Variables

Add GitHub secrets to your repository:

1. Go to GitHub: Settings → Secrets and variables → Actions
2. Add these secrets:
   - `COSMOS_DB_ENDPOINT`: https://productivityflow.documents.azure.com:443/
   - `COSMOS_DB_KEY`: Your Cosmos DB primary key
   - `DATABASE_NAME`: stayfocus
   - `CONTAINER_NAME`: Reviews

## Step 3: Update Blazor App Configuration

Update your Blazor app to call the API:

```csharp
// In StayFocus/Program.cs
builder.Services.AddScoped(sp => 
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Call /api/* endpoints
```

## Step 4: Deploy

Push to master branch:
```powershell
git add .
git commit -m "Add Static Web App deployment configuration"
git push origin master
```

GitHub Actions will automatically build and deploy.

## Step 5: Verify Deployment

1. Navigate to Azure Portal
2. Find your Static Web App resource
3. Get the default domain URL
4. Test the API: `https://your-app-url/api/reviews`
5. Test the Blazor app: `https://your-app-url`

## Troubleshooting

- Check GitHub Actions workflow in your repo
- Review Azure Portal → Static Web App → Build History
- Check Azure Portal → Static Web App → Logs

## Cost Optimization

- Free tier includes:
  - 1 free static web app
  - 100 GB bandwidth/month
  - Managed SSL certificate
  - API support (up to 4 functions)

- Cosmos DB (shared throughput):
  - Starting at 400 RU/s ($25/month)
