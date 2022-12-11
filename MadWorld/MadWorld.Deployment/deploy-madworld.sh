subscriptionName="Oscar"
resourceGroupName="MadWorldTools"

az account set --subscription $subscriptionName
az group create --name $resourceGroupName --location westeurope

az deployment group create \
  --mode Complete \
  --name deployment-madworld \
  --resource-group $resourceGroupName \
  --template-uri "https://raw.githubusercontent.com/oveldman/MadWorldTools/master/MadWorld/MadWorld.Deployment/deploy-madworld.json"