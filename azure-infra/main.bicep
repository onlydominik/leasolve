targetScope = 'subscription'

param location string
param appBaseName string

@allowed(['dev', 'prod'])
param environment string
param sku string

var resourceGroupName = 'rg-${appBaseName}-${environment}'

resource rg 'Microsoft.Resources/resourceGroups@2024-11-01' = {
    name: resourceGroupName
    location: location
    tags: {
        environment: environment
        managedBy: 'bicep'
        project: appBaseName
    }
}

module webApp 'modules/webapp.bicep' = {
    scope: rg
    name: 'webapp-deployment'
    params: {
        location: location
        appBaseName: appBaseName
        environment: environment
        sku: sku    
    }    
}

output webAppName string = webApp.outputs.webAppName
output webAppUrl string = webApp.outputs.webAppUrl