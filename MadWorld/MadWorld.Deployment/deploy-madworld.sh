# Deploy with az
subscriptionName="Oscar"
resourceGroupName="MadWorldTools"

az account set --subscription $subscriptionName
az group create --name $resourceGroupName --location westeurope

az deployment group create \
  --mode Complete \
  --name deployment-madworld \
  --resource-group $resourceGroupName \
  --template-uri "https://raw.githubusercontent.com/oveldman/MadWorldTools/master/MadWorld/MadWorld.Deployment/deploy-madworld.json"
  
# Create credentials to deploy with ARM
# The response you can use as credentials
name="credentials_madworldtools"
role="contributor"
scope="/subscriptions/[id]/resourceGroups/MadWorldTools"

az ad sp create-for-rbac --name $name --role $role --scopes $scope --sdk-auth
