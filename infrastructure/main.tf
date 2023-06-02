terraform {
  backend "azurerm" {
    resource_group_name  = "MyResourceGroup2"
    storage_account_name = "storageaccountomryb"
    container_name       = "tfstate"
    key                  = "state.tfstate"
    access_key           = "7UGurk51hKDDDCHf1+WblW9Q0TjBsIHWvlLT6nMXFSzA0SnveN5aKwONGzyqDxFnSiB2u4Ow1n6M+ASt9TJkqQ=="
  }
}
provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "example" {
  name     = "MyResourceGroup235"
  location = "eastus"
}

resource "azurerm_app_service_plan" "example" {
  name                = "tomrybAzureCourseAppServicePlan"
  location            = azurerm_resource_group.example.location
  resource_group_name = azurerm_resource_group.example.name
  sku {
    tier = "Free"
    size = "F1"
  }
}

resource "azurerm_app_service" "example" {
  name                = "tomrybAzureCourseWebApp"
  location            = azurerm_resource_group.example.location
  resource_group_name = azurerm_resource_group.example.name
  app_service_plan_id = azurerm_app_service_plan.example.id
  app_settings = {
    "APPINSIGHTS_INSTRUMENTATIONKEY"      = azurerm_application_insights.example.instrumentation_key
    "ConnectionStrings:DefaultConnection" = "Server=tcp:${azurerm_sql_server.example.name},1433;Initial Catalog=${azurerm_sql_database.example.name};Persist Security Info=False;User ID=${azurerm_sql_server.example.administrator_login};Password=${azurerm_sql_server.example.administrator_login_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }

  connection_string {
    name  = "DBConnectionString"
    type  = "SQLAzure"
    value = "Server=tcp:${azurerm_sql_server.example.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_sql_database.example.name};Persist Security Info=False;User ID=${azurerm_sql_server.example.administrator_login};Password=${azurerm_sql_server.example.administrator_login_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}

locals {
  location = "northeurope"
}

resource "azurerm_application_insights" "example" {
  name                = "tomrybAzureCourseAppInsights"
  location            = azurerm_resource_group.example.location
  resource_group_name = azurerm_resource_group.example.name
  application_type    = "web"
}

resource "azurerm_sql_server" "example" {
  name                         = "example-sqlserver-tomryb-azure"
  resource_group_name          = azurerm_resource_group.example.name
  location                     = local.location
  version                      = "12.0"
  administrator_login          = "adminTomRyb"
  administrator_login_password = "passwordTomRyb1234!"
}

resource "azurerm_sql_database" "example" {
  name                = "example-database-tomryb-azure"
  resource_group_name = azurerm_resource_group.example.name
  server_name         = azurerm_sql_server.example.name
  location            = local.location
  edition             = "Basic"
}


resource "azurerm_storage_account" "example" {
  name                     = "tomrybjagodno1"
  resource_group_name      = azurerm_resource_group.example.name
  location                 = local.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
  account_kind             = "StorageV2"
}

resource "azurerm_storage_container" "example" {
  name                  = "tomrybjagodno1container"
  storage_account_name  = azurerm_storage_account.example.name
  container_access_type = "private"
}
