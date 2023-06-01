terraform {
  backend "azurerm" {
    resource_group_name = "MyResourceGroup"
    storage_account_name = "tomrybstorage"
    container_name = "tomrybcontainer"
    key = "state.tfstate"
  }
}
provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "rg" {
  name     = "MyResourceGroup2"
  location = "eastus"
}