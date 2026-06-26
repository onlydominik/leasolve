# leasolve - Bicep + Azure DevOps

---

## azure-infra
- split into a module for the webapp
- parameters in bicepparam

## azure-pipelines
- azure-pipelines.yaml built as stages
- jobs called as templates
- variables also as a template
- trigger limited to changes in the azure-infra and azure-pipelines folders
- validation stage before deploy - bicep build
- for multi-env / prod, azure-pipelines variables would be better in AZ DevOps variable groups 


Successful Azure DevOps pipeline run
![success-pipeline.png](assets/success-pipeline.png)
