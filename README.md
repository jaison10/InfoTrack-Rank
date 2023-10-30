# InfoTrack-Rank
This application is designed to retrieve search engine results and determine the rank of a specific URL within those search results.
## API : 
The API built using .Net 7.0, EF Core, LINQ operations, and Repositories for robust functionality. 
#### Setup:
1. Fetch the User Agent and store it in 'Agent' variable in 'SearchRepoImplement.cs'file under Repositories.
2. Set your local server name as the value for 'Server=' in the 'ConnectionStrings' section of the 'appsettings.json' file.
3. Install the below:
   1. Microsoft.EntityFrameworkCore
   2. Microsoft.EntityFrameworkCore.SqlServer
   3. Microsoft.EntityFrameworkCore.Tools
4. This utilizes Microsoft SQL Server. Run "add-migration 'name'" and "update-database" to create the database, which also creates 2 items in "SearchEngine" table.
5. Run the application and test on Swagger.
