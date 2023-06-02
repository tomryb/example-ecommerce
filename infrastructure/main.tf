terraform {
  backend "azurerm" {
    resource_group_name = "MyResourceGroup2"
    storage_account_name = "storageaccountomryb"
    container_name = "tfstate"
    key = "state.tfstate"
    access_key = "7UGurk51hKDDDCHf1+WblW9Q0TjBsIHWvlLT6nMXFSzA0SnveN5aKwONGzyqDxFnSiB2u4Ow1n6M+ASt9TJkqQ=="
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
}

resource "azurerm_sql_server" "example" {
  name                         = "mysqlservertomrybazure"
  location                     = azurerm_resource_group.example.location
  resource_group_name          = azurerm_resource_group.example.name
  version                      = "12.0"
  administrator_login          = "myAdmin"
  administrator_login_password = "myPassword1234!"
}

resource "azurerm_sql_database" "example" {
  name                = "myDatabaseTomrybAzure"
  resource_group_name = azurerm_resource_group.example.name
  server_name         = azurerm_sql_server.example.name
  edition             = "Basic"
  location            = azurerm_resource_group.example.location
}
