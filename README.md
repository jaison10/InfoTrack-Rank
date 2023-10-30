# InfoTrack-Rank
This application is designed to retrieve search engine results and determine the rank of a specific URL within those search results.
## API : 
The API was built using .Net 7.0, EF Core, LINQ operations, and Repositories for robust functionality. 
#### Setup:
1. Fetch the User Agent and store it in the 'Agent' variable in the 'SearchRepoImplement.cs'file under Repositories.
2. Set your local server name as the value for 'Server=' in the 'ConnectionStrings' section of the 'appsettings.json' file.
3. Install the below:
   1. Microsoft.EntityFrameworkCore
   2. Microsoft.EntityFrameworkCore.SqlServer
   3. Microsoft.EntityFrameworkCore.Tools
4. This utilizes Microsoft SQL Server. Run "add-migration 'name'" and "update-database" to create the database, which also creates 2 items in the "SearchEngine" table.
5. Run the application and test on Swagger.

## SPA : 
The front end of the same is built using Angular, TypeScript, RxJS, and Bootstrap.
#### Setup:
1. Run 'npm install' to install the npm packages.
2. Run 'ng serve' to run the application.

## API-SPA Configuration:
Make sure the API is running at 'localhost:7056'. If not, update the URL in the 'rank.service.ts' file under the 'app' folder under the 'src' of the Angular application.
If the Angular application is not running at 'localhost:4200', make the required changes in the 'Program.cs' file of the API.

