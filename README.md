# Rank.Casino
//TODO complete
Deployment Requirements
Windows Web Application Server supoprting .Net Core 6.0 (https://dotnet.microsoft.com/permalink/dotnetcore-current-windows-runtime-bundle-installer)
Webdeploy. If deploying Via WebDeploy

Included is a ZIP file (Rank.Casino.Zip in the Solutions Root File) with 3 Folders.
WebDeploy (Contains the files required to deploy the Web API using webdploy)
- Readme /Webdeploy/Rank.Casino.Web.deploy-readme.txt for details deploying

IIS (Contains the files to manually Copy the Files to the IIS Application Server)
Create IIS .net COre WEb application on Web server
Copy Files from Deploy Folder

TestHarness (Included Console App to TestHarness API functions. This App calls the Controller Class functions Directly)
-There are 2 default users created:
--John Doe: Id 1
--Jane Doe: Id 2

Solution Overview: Compiled VS 2022
Rank.Casino.Data
-Data Layer. Functions for calling "SQL" Emulated in memory. Returns the DataSets to the Logical LAyer
Rank.Casino.Web
-The Rest API Web Application. Does the call to the Logical Layer for API functions
Rank.Casino.TestHarness
-A Test Console to Test API Calls and the Rest Controller
Rank.Casino.API
-The Logical Layer for the Function. Does the call to the DataLayer and returns to the UI\ExtSystem Layer