# ShopRUs
ShopsRUs is an existing retail outlet. They would like to provide discount to their customers on all their web/mobile platforms. 
They require a set of APIs to be built that provide capabilities to calculate discounts, generate the total costs and generate the 
invoices for customers


How to Start Up this Project

Select ShopRUs.Api project as start up project in visual studio
Go to appSettings.Json in your Api project
Update ShopRUsContext to your system path
Click on Tools => Nuget Manager => Package manager Console, to open up PM
select ShopRUs.Core as Default project
type in console :  add-migration {specify Migration name} eg add-migration InitialMigration after it runs successfully
type in console : update-database
Click on start to run project
