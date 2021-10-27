# ShopRUs

ShopsRUs is an existing retail outlet. They would like to provide discount to their customers on all their web/mobile platforms. 
They require a set of APIs to be built that provide capabilities to calculate discounts, generate the total costs and generate the 
invoices for customers


How to Start Up this Project

1. Select ShopRUs.Api project as start up project in visual studio
2. Go to appSettings.Json in your Api project
3. Update ShopRUsContext to your system path
4. Click on Tools => Nuget Manager => Package manager Console, to open up PM
5. select ShopRUs.Core as Default project
6. type in console :  add-migration {specify Migration name} eg add-migration InitialMigration after it runs successfully
7. type in console : update-database
8. Click on start to run project

