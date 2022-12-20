# Deploy with az
# Section 1
subscriptionName="Oscar"
resourceGroupName="MadWorldTools"

az account set --subscription $subscriptionName
az group create --name $resourceGroupName --location westeurope

az deployment group create \
  --mode Complete \
  --name deployment-madworld \
  --resource-group $resourceGroupName \
  --template-uri "https://raw.githubusercontent.com/oveldman/MadWorldTools/master/MadWorld/MadWorld.Deployment/main-deploy.json"
# End Section 1  

# Create credentials to deploy with ARM
# The response you can use as credentials
# Section 2
name="credentials_madworldtools"
defaultRole="contributor"
scope="/subscriptions/[id]/resourceGroups/MadWorldTools"

az ad sp create-for-rbac --name $name --role $defaultRole --scopes $scope --sdk-auth
# End Section 2

