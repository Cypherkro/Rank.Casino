# Rank.Casino
//todo complete

install iis

https://dotnet.microsoft.com/permalink/dotnetcore-current-windows-runtime-bundle-installer

Compiled VS 2022
.NET Core 6.0

TestHarness

John Doe
Jane Doe
PlayerID 1

Deploy Via WebDeploy
-link to Path

Deploy To IIS 
Create IIS .net COre WEb application on Web server
Copy Files from Deploy Folder

SOlution OVerview
Rank.Casino.Data
-Data Layer. Functions for calling "SQL" Emulated in memory. Returns the DataSets to the Logical LAyer
Rank.Casino.Web
Rank.Casino.TestHarness
Rank.Casino.API
-The Logical Layer for the Function. Does the call to the DataLayer and returns to the UI\ExtSystem Layer