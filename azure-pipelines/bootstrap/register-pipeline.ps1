# one-time bootstrap - registers the Azure DevOps pipeline that deploys the IaC

az login
az account set -s "500e441f-2447-43a1-80ad-ebcc0404c170"

# from where you want check for pipeline
$env:GITHUB_BRANCH = 'feature/webapp-bicep-infra'

az extension add --name azure-devops --upgrade

az devops configure --defaults organization=https://dev.azure.com/leasolve project=leasolve

az pipelines create --name "leasolve-infra" --repository "onlydominik/leasolve" --repository-type github --branch $env:GITHUB_BRANCH --yaml-path /azure-pipelines/azure-pipelines.yaml --skip-first-run true