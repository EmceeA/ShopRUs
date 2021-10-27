# ShopRUs

Select ShopRUs.Api project as start up project in visual studio
Go to appSettings.Json in your Api project
Update ShopRUsContext to your system path
Click on Tools => Nuget Manager => Package manager Console, to open up PM
select ShopRUs.Core as Default project
type in console :  add-migration {specify Migration name} eg add-migration InitialMigration after it runs successfully
type in console : update-database
Click on start to run project
