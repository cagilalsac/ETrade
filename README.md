# Project Development Roadmap

# 1) CORE Project:
1.1) Install Microsoft.EntityFrameworkCore NuGet Package
1.2) Create (https://github.com/cagilalsac/ETrade/blob/main/CORE/APP/Domain/Entity.cs)[Entity.cs]
1.3) Create (https://github.com/cagilalsac/ETrade/blob/main/CORE/APP/Models/Request.cs)(Request.cs)
1.4) Create (https://github.com/cagilalsac/ETrade/blob/main/CORE/APP/Models/QueryResponse.cs)(QueryResponse.cs)
1.5) Create (https://github.com/cagilalsac/ETrade/blob/main/CORE/APP/Models/CommandResponse.cs)(CommandResponse.cs)
1.6) Create (https://github.com/cagilalsac/ETrade/blob/main/CORE/APP/Services/ServiceBase.cs)(ServiceBase.cs)
# 2) APP Project:
2.1) Add project reference of the CORE Project
2.2.1) If using SQL Server install Microsoft.EntityFrameworkCore.SqlServer NuGet Package
2.2.2) If using SQLite install Microsoft.EntityFrameworkCore.Sqlite with System.Data.SQLite.Core NuGet Packages
2.3) Create (https://github.com/cagilalsac/ETrade/blob/main/APP/Domain/Category.cs)(Category.cs)
2.4) Create (https://github.com/cagilalsac/ETrade/blob/main/APP/Domain/Product.cs)(Product.cs)
2.5) Create (https://github.com/cagilalsac/ETrade/blob/main/APP/Domain/Db.cs)(Db.cs)
# 3) MVC Project:
3.1) Install Microsoft.EntityFrameworkCore.Tools NuGet Package and set the MVC Project as the startup project
3.2) Add the ConnectionStrings section to (https://github.com/cagilalsac/ETrade/blob/main/MVC/appsettings.json)(appsettings.json) file
3.3) Add builder.Services.AddDbContext method in the IoC Container of the (https://github.com/cagilalsac/ETrade/blob/main/MVC/Program.cs)(Program.cs) file
3.4) Run "add-migration v1" in Package Manager Console after selecting APP Project as the default project
3.5) Run "update-database" to create the database in (LocalDB)\MSSQLLocalDB database server or SQLite database file
# 4) APP Project:
4.1) Create (https://github.com/cagilalsac/ETrade/blob/main/APP/Models/CategoryRequest.cs)(CategoryRequest.cs)
4.2) Create (https://github.com/cagilalsac/ETrade/blob/main/APP/Models/CategoryQueryResponse.cs)(CategoryQueryResponse.cs)
4.3) Create (https://github.com/cagilalsac/ETrade/blob/main/APP/Services/DbService.cs)(DbService.cs)
4.4) Create (https://github.com/cagilalsac/ETrade/blob/main/APP/Services/CategoryService.cs)(CategoryService.cs)
# 5) MVC Project:
5.1) Create (https://github.com/cagilalsac/ETrade/blob/main/MVC/Controllers/CategoriesController.cs)(CategoriesController.cs)
5.2) Create (https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Categories/CategoryList.cshtml)(CategoryList.cshtml)
5.3) Create (https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Categories/Details.cshtml)(Details.cshtml)
5.4) Create (https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Categories/Create.cshtml)(Create.cshtml)
5.5) Scaffold (https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Categories/Edit.cshtml)(Edit.cshtml)
5.6) Scaffold (https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Categories/Delete.cshtml)(Delete.cshtml)
5.7) Add Categories link to Bootstrap Navbar in (https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Shared/_Layout.cshtml)(_Layout.cshtml)
5.8) Modify (https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Home/Index.cshtml)(Index.cshtml) according to your application
5.9) Modify (https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Home/Privacy.cshtml)(Privacy.cshtml) according to your application