# InfoTrack-Rank
This application is designed to retrieve search engine results and determine the rank of a specific URL within those search results. If the rank is identified, it will be saved in the MSSQL server for historical records. 
Note: The records will be stored only if Ranks are found.
## API : 
The API is built using .Net 7.0, EF Core, LINQ operations, and Repositories for robust functionality. 
#### Setup:
1. Fetch the User Agent and store it in the 'Agent' variable in the 'SearchRepoImplement.cs'file under Repositories.
2. Set your local server name as the value for 'Server=' in the 'ConnectionStrings' section of the 'appsettings.json' file.
3. Install the below:
   1. Microsoft.EntityFrameworkCore
   2. Microsoft.EntityFrameworkCore.SqlServer
   3. Microsoft.EntityFrameworkCore.Tools
4. This utilizes Microsoft SQL Server. Run "update-database" to create the database, which also creates 2 items in the "SearchEngine" table.
5. Run the application and test on Swagger.

## SPA : 
The front end of the same is built using Angular, TypeScript, RxJS, and Bootstrap. Validations not handled in the front-end as of now as it is handled in the API.
#### Setup:
1. Run 'npm install' to install the project dependencies.
2. Run 'ng serve' to run the application.
3. URLs:
   1. /search: To search for an item.
   2. /history: To view the search history of previous queries.

## API-SPA Configuration:
1. Make sure the API is running at 'localhost:7056'. If not, update the URL in the 'rank.service.ts' file under the 'app' folder under the 'src' of the Angular application.
2. If the Angular application is not running at 'localhost:4200', make the required changes in the 'Program.cs' file of the API.

### ****** SCREENSHOTS********

1. Search Page:
   ![BasicWithURL](https://github.com/jaison10/InfoTrack-Rank/assets/36099608/b27c6258-de11-4fae-8887-8b780df5f001)

2. Bing Search:
   ![Bing](https://github.com/jaison10/InfoTrack-Rank/assets/36099608/54110ea3-dcb6-496a-beb6-5eee57622637)

3. Google Search:
   ![Google](https://github.com/jaison10/InfoTrack-Rank/assets/36099608/d7502ba6-829e-4465-a7de-3d5a8378e591)

4. Successful search with Ranks found:
    ![Success](https://github.com/jaison10/InfoTrack-Rank/assets/36099608/2042afe4-d742-4ad1-91a9-4550aec90ffc)

5. Successful search with no Ranks found:
   ![NoRank](https://github.com/jaison10/InfoTrack-Rank/assets/36099608/af5c127b-83f1-484f-a695-ba0ffd2fff69)

6. Invalid Input given:
   ![Invalid](https://github.com/jaison10/InfoTrack-Rank/assets/36099608/be4e6f69-a2a0-476f-bbc4-890766e5512b)

7. History of searches:
   ![History](https://github.com/jaison10/InfoTrack-Rank/assets/36099608/4b43dace-842a-4240-8b12-19c4ee912715)

8. Filter by Search in History: 
   ![SearchHistory](https://github.com/jaison10/InfoTrack-Rank/assets/36099608/4ab403d4-9469-4b7b-acf4-7812332402cb)


### Thank you! Feel free to reach out to me in case of any issues!
