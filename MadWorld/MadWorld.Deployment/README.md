## MadWorld.Deployment
### How to deploy the ARM template
Run the scripts with az cli from Azure.
1. Run the command from section 1 in [deploy-madworld.sh](https://github.com/oveldman/MadWorldTools/tree/main/MadWorld/MadWorld.Deployment/deploy-madworld.sh)
2. And you already done!

### How to use Github Action deploy the ARM template
Run the scripts with az cli from Azure. 
1. Run the command from section 2 in [deploy-madworld.sh](https://github.com/oveldman/MadWorldTools/tree/main/MadWorld/MadWorld.Deployment/deploy-madworld.sh)
2. Copy the output of the command and save it as a secret with the name "AZURE_CREDENTIALS"
3. Run the action and have fun!

### Exceptions
1. Deploy B2C in portal or az cli. It doesn't supported directly by ARM script. (Not easily anyway. )
2. Deploy Custom domain in portal or az cli. It doesn't supported directly by ARM script on creating. It only supports update. Because of the validation. 
