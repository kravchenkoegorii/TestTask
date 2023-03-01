targetScope = 'resourceGroup'
param rgName string
param location string
param aspName string
param skuPlanName string
param appName string
param appiName string
param sqlName string
param sqldbName string
param adminUser string
@secure()
param adminPassword string

module app './appService.bicep' = {
  name: 'appServiceDeployment'
  scope: resourceGroup(rgName)
  params: {
    appName: appName
    location: location
    aspName: aspName
    skuPlanName: skuPlanName
    appiName: appiName
  }
}

module sql './sqlServer.bicep' = {
  name: 'sqlServerDeployment'
  scope: resourceGroup(rgName)
  params: {
    sqldbName: sqldbName
    sqlName: sqlName
    location: location
    adminUser: adminUser
    adminPassword: adminPassword
  }
}
