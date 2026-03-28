param location string = resourceGroup().location
param staticWebAppName string = 'rishi-web-app'
param cosmosDbAccountName string = 'productivityflow'

resource staticWebApp 'Microsoft.Web/staticSites@2022-03-01' = {
  name: staticWebAppName
  location: location
  sku: {
    name: 'Free'
    tier: 'Free'
  }
  properties: {
    repositoryUrl: 'https://github.com/prasadpatil2666/Rishi'
    branch: 'master'
    buildProperties: {
      appLocation: 'StayFocus'
      apiLocation: 'StayFocusAPI'
      outputLocation: 'wwwroot'
      appBuildCommand: 'dotnet publish -c Release -o ./publish'
      apiBuildCommand: 'dotnet publish -c Release -o ./publish'
    }
  }
}

output staticWebAppUrl string = staticWebApp.properties.defaultHostname
