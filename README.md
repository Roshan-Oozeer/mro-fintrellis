# Fintrellis API for CRUD Demonstration
This application demonstrates a simple API for CRUD in .NET 8.0.

## Features
The solution has the following features:
- Layering - For best practices, a solution is layered in the following fashion (from bottom to top):
-- Data Layer (Repository / Data Access)
-- Domain Layer (Business logic)
-- Service Layer (Transform entity db data to response data objects)
-- Hosting Layer (WEB API)
-- Cross Cutting Concern (Common Utils classes and Domain models)
In our case, we have simplified the solution by collapsing the Data and Domain layers, and collapsing Service and Hosting layers into a single layer.
- Logging - The Microsoft Extension for logging has been used, which was complimented with SeriLog to write to a rolling flat file.
- Unit Tests - Mocking was implemented to make the solution portable to a CI/CD environment.
- Swagger - A Swagger UI page has been implemented for API documentation.
- MS-SQL server was used as data storage.
- The repository pattern was used for the Data Layer, and
- Inheritance / Polymorphism used to make coding more simple and intuitive - Please see how the repositories and controllers were implemented.


## Getting started
First and foremost, the database must be constructed. In this solution, MS-SQL server is utilized. An SQL script has been provided in the solution to build the database and tables. Once the latter has been actioned, please update the connection string in appsettings.Development.json file under Fintrellis.WebApi project.


## Running the Solution
The solution can be run within Visual Studio or VS Code.



