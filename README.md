# Project Development Roadmap

# 1) CORE Project:
1.1) Install Microsoft.EntityFrameworkCore NuGet Package\
1.2) Create [Entity.cs](https://github.com/cagilalsac/ETrade/blob/main/CORE/APP/Domain/Entity.cs)\
1.3) Create [Request.cs](https://github.com/cagilalsac/ETrade/blob/main/CORE/APP/Models/Request.cs)\
1.4) Create [QueryResponse.cs](https://github.com/cagilalsac/ETrade/blob/main/CORE/APP/Models/QueryResponse.cs)\
1.5) Create [CommandResponse.cs](https://github.com/cagilalsac/ETrade/blob/main/CORE/APP/Models/CommandResponse.cs)\
1.6) Create [ServiceBase.cs](https://github.com/cagilalsac/ETrade/blob/main/CORE/APP/Services/ServiceBase.cs)
# 2) APP Project:
2.1) Add project reference of the CORE Project\
2.2.1) If using SQL Server install Microsoft.EntityFrameworkCore.SqlServer NuGet Package\
2.2.2) If using SQLite install Microsoft.EntityFrameworkCore.Sqlite with System.Data.SQLite.Core NuGet Packages\
2.3) Create [Category.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Domain/Category.cs)\
2.4) Create [Product.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Domain/Product.cs)\
2.5) Create [Db.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Domain/Db.cs)
# 3) MVC Project:
3.1) Install Microsoft.EntityFrameworkCore.Tools NuGet Package and set the MVC Project as the startup project\
3.2) Add the ConnectionStrings section to [appsettings.json](https://github.com/cagilalsac/ETrade/blob/main/MVC/appsettings.json) file\
3.3) Add builder.Services.AddDbContext method in the IoC Container of the [Program.cs](https://github.com/cagilalsac/ETrade/blob/main/MVC/Program.cs) file\
3.4) Run "add-migration v1" in Package Manager Console after selecting APP Project as the default project\
3.5) Run "update-database" to create the database in (LocalDB)\MSSQLLocalDB database server or SQLite database file
# 4) APP Project:
4.1) Create [CategoryRequest.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Models/CategoryRequest.cs)\
4.2) Create [CategoryQueryResponse.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Models/CategoryQueryResponse.cs)\
4.3) Create [DbService.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Services/DbService.cs)\
4.4) Create [CategoryService.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Services/CategoryService.cs)
# 5) MVC Project:
5.1) Create [CategoriesController.cs](https://github.com/cagilalsac/ETrade/blob/main/MVC/Controllers/CategoriesController.cs)\
5.2) Create [CategoryList.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Categories/CategoryList.cshtml)\
5.3) Create [Details.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Categories/Details.cshtml)\
5.4) Create [Create.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Categories/Create.cshtml)\
5.5) Scaffold [Edit.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Categories/Edit.cshtml)\
5.6) Scaffold [Delete.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Categories/Delete.cshtml)\
5.7) Add Categories link to Bootstrap Navbar in [Layout.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Shared/_Layout.cshtml)\
5.8) Modify [Index.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Home/Index.cshtml) according to your application\
5.9) Modify [Privacy.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Home/Privacy.cshtml) according to your application