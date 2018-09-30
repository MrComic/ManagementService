This is a Starting Point For Your Spa Project.an Angular Based Application with .net Core integration
here's the list of Tecnologies I'm using in this project:
Angular 6.1
Datatablesjs-For Ui Table
NgSelect-For Select DropDown
NgBootstrap-Bootstrap Components
.Net Core 2.1
EntityFramework-Database Orm
Asp.net Mvc WebApi - My BackEnd
Asp.net Identity - Authorization System 
Autofac - For Dependency injection in .net
AutoMapper - For Mapping Beetwin Two Model
Urf unit of work - this is an Implementation of Repository Pattern for EntityFramework
nlog-for loggin purpose (will be added in the next build)
Microsoft Sql Server - Database  

You Will Need Microsoft Visual Studio 2017 and .net Core 2.1 sdk to run this project 
and for Angular Development you can use what ever Editor you like (I'm Using WebStorm) 
After Cloning This project you will need to to run Npm install in ui folder of ManagementService.Web folder 
After that You will need to Change the ConnectionString in DatabaseContext class in ManagementService.Data Project 
In the end open Package Manager Console in visual studio and Run the 'update-database' Command to Create The database in your Server.
be sure to select ManagementService.Data in Package Manager Console unless update-database command won't work.


this repo is under active development and we will try our best to improve this repo every day.
feel free to submit an issue and we will answer as soon as possible.
