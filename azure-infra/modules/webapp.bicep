param location string
param appBaseName string
param environment string
param sku string

var appServicePlanName = 'asp-${appBaseName}-${environment}'
var webAppName = 'app-${appBaseName}-${environment}-${uniqueString(resourceGroup().id)}'

resource appServicePlan 'Microsoft.Web/serverfarms@2024-11-01' = {
    name: appServicePlanName
    location: location
    sku: {
        name: sku
        tier: 'Free'
    }
    properties: {
        reserved: false  
    }    
}

resource webApp 'Microsoft.Web/sites@2024-11-01' = {
    name: webAppName
    location: location
    properties: {
        serverFarmId: appServicePlan.id
        httpsOnly: true
        siteConfig: {
            alwaysOn: false
            ftpsState: 'Disabled'
            minTlsVersion: '1.2'    
        }
    }
}

output webAppName string = webApp.name
output webAppUrl string = 'https://${webApp.properties.defaultHostName}'