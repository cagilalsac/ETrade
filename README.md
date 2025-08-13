# Project Development Roadmap

# 1) CORE Project:
1.1) Install Microsoft.EntityFrameworkCore NuGet Package\
1.2) Create [CORE/APP/Domain/Entity.cs](https://github.com/cagilalsac/ETrade/blob/main/CORE/APP/Domain/Entity.cs)\
1.3) Create [CORE/APP/Models/Request.cs](https://github.com/cagilalsac/ETrade/blob/main/CORE/APP/Models/Request.cs)\
1.4) Create [CORE/APP/Models/Response.cs](https://github.com/cagilalsac/ETrade/blob/main/CORE/APP/Models/Response.cs)\
1.5) Create [CORE/APP/Models/CommandResponse.cs](https://github.com/cagilalsac/ETrade/blob/main/CORE/APP/Models/CommandResponse.cs)\
1.6) Create [CORE/APP/Services/ServiceBase.cs](https://github.com/cagilalsac/ETrade/blob/main/CORE/APP/Services/ServiceBase.cs)
# 2) APP Project:
2.1) Add project reference of the CORE Project\
2.2.1) If using SQL Server install Microsoft.EntityFrameworkCore.SqlServer NuGet Package\
2.2.2) If using SQLite install Microsoft.EntityFrameworkCore.Sqlite with System.Data.SQLite.Core NuGet Packages\
2.3) Create [APP/Domain/Category.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Domain/Category.cs)\
2.4) Create [APP/Domain/Product.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Domain/Product.cs)\
2.5) Create [APP/Domain/Db.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Domain/Db.cs)
# 3) MVC Project:
3.1) Install Microsoft.EntityFrameworkCore.Tools NuGet Package and set the MVC Project as the startup project\
3.2) Add the ConnectionStrings section to [MVC/appsettings.json](https://github.com/cagilalsac/ETrade/blob/main/MVC/appsettings.json) file\
3.3) Add "builder.Services.AddDbContext<Db>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Db")));"\
in the IoC Container of the [MVC/Program.cs](https://github.com/cagilalsac/ETrade/blob/main/MVC/Program.cs) file\
3.4) Run "add-migration v1" in Package Manager Console after selecting APP Project as the default project\
3.5) Run "update-database" to create the database in (LocalDB)\MSSQLLocalDB database server or SQLite database file
# 4) APP Project (Category):
4.1) Create [APP/Models/CategoryRequest.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Models/CategoryRequest.cs)\
4.2) Create [APP/Models/CategoryResponse.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Models/CategoryResponse.cs)\
4.3) Create [APP/Services/DbService.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Services/DbService.cs)\
4.4) Create [APP/Services/CategoryService.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Services/CategoryService.cs)
# 5) MVC Project (Category):
5.1) Add "builder.Services.AddScoped<IService<CategoryRequest, CategoryResponse>, CategoryService>();"\
in the IoC Container of the [MVC/Program.cs](https://github.com/cagilalsac/ETrade/blob/main/MVC/Program.cs) file\
5.2) Create [MVC/Controllers/CategoriesController.cs](https://github.com/cagilalsac/ETrade/blob/main/MVC/Controllers/CategoriesController.cs)\
5.3) Create [MVC/Views/Categories/CategoryList.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Categories/CategoryList.cshtml)\
5.4) Create [MVC/Views/Categories/Details.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Categories/Details.cshtml)\
5.5) Create [MVC/Views/Categories/Create.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Categories/Create.cshtml)\
5.6) Download and extract the Templates folder to the MVC Project from:\ 
https://need4code.com/DotNet/Home/Index?path=.NET%5C00_Files%5CScaffolding%20Templates%5CTemplates.7z\
5.7) Scaffold [MVC/Views/Categories/Edit.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Categories/Edit.cshtml) using Entity Framework\
5.8) Scaffold [MVC/Views/Categories/Delete.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Categories/Delete.cshtml) using Entity Framework\
5.9) Add Categories link to Bootstrap Navbar in [MVC/Views/Shared/Layout.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Shared/_Layout.cshtml)\
5.10) Modify [MVC/Views/Home/Index.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Home/Index.cshtml) according to your application\
5.11) Modify [MVC/Views/Home/Privacy.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Home/Privacy.cshtml) according to your application
# 6) APP Project (Product):
6.1) Create [APP/Models/ProductRequest.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Models/ProductRequest.cs)\
6.2) Create [APP/Models/ProductResponse.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Models/ProductResponse.cs)\
6.3) Create [APP/Services/ProductService.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Services/ProductService.cs)
# 7) MVC Project (Product):
7.1) Add "builder.Services.AddScoped<IService<ProductRequest, ProductResponse>, ProductService>();"\
in the IoC Container of the [MVC/Program.cs](https://github.com/cagilalsac/ETrade/blob/main/MVC/Program.cs) file\
7.2) Scaffold Products controller with views using Entity Framework and make changes in the controller and views if needed\
[MVC/Controllers/ProductsController.cs](https://github.com/cagilalsac/ETrade/blob/main/MVC/Controllers/ProductsController.cs)\
[MVC/Views/Products/Index.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Products/Index.cshtml)\
[MVC/Views/Products/Details.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Products/Details.cshtml)\
[MVC/Views/Products/Create.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Products/Create.cshtml)\
[MVC/Views/Products/Edit.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Products/Edit.cshtml)\
[MVC/Views/Products/Delete.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Products/Delete.cshtml)\
7.3) Add "jquery-datetimepicker" client-side library under wwwroot/lib folder of the MVC Project\
7.4) Add Products link to Bootstrap Navbar in [MVC/Views/Shared/Layout.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Shared/_Layout.cshtml)
# 8) APP Project (Store):
8.1) Create [APP/Domain/Store.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Domain/Store.cs)\
8.2) Create [APP/Domain/ProductStore.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Domain/ProductStore.cs)\
8.3) Add ProductStores and StoreIds properties to [APP/Domain/Product.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Domain/Product.cs)\
8.4) Add Stores and ProductStores DbSets to [APP/Domain/Db.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Domain/Db.cs)\
8.5) Run "add-migration v2" in Package Manager Console after selecting APP Project as the default project\
8.6) Run "update-database" to update the database for Stores and ProductStores tables in (LocalDB)\MSSQLLocalDB database server or SQLite database file\
8.7) Create [APP/Models/StoreRequest.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Models/StoreRequest.cs)\
8.8) Create [APP/Models/StoreResponse.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Models/StoreResponse.cs)\
8.9) Create [APP/Services/StoreService.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Services/StoreService.cs)\
8.10) Add StoreIds property to [APP/Models/ProductRequest.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Models/ProductRequest.cs)\
8.11) Add StoreIds and Stores properties to [APP/Models/ProductResponse.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Models/ProductResponse.cs)\
8.12) Add the lines commented as STORE UPDATE to the methods of [APP/Services/ProductService.cs](https://github.com/cagilalsac/ETrade/blob/main/APP/Services/ProductService.cs)
# 9) MVC Project (Store):
9.1) Add "builder.Services.AddScoped<IService<StoreRequest, StoreResponse>, StoreService>();"\
in the IoC Container of the [MVC/Program.cs](https://github.com/cagilalsac/ETrade/blob/main/MVC/Program.cs) file\
9.2) Add the lines commented as STORE UPDATE to the view [MVC/Views/Products/Index.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Products/Index.cshtml)\
9.3) Add the lines commented as STORE UPDATE to the view [MVC/Views/Products/Details.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Products/Details.cshtml)\
9.4) Add the lines commented as STORE UPDATE to the controller [MVC/Controllers/ProductsController.cs](https://github.com/cagilalsac/ETrade/blob/main/MVC/Controllers/ProductsController.cs)\
9.5) Add the lines commented as STORE UPDATE to the view [MVC/Views/Products/Create.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Products/Create.cshtml)\
9.6) Add the lines commented as STORE UPDATE to the view [MVC/Views/Products/Edit.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Products/Edit.cshtml)\
9.7) Add the lines commented as STORE UPDATE to the view [MVC/Views/Products/Delete.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Products/Delete.cshtml)\
9.8) Scaffold Stores controller with views using Entity Framework and make changes in the controller and views if needed\
[MVC/Controllers/StoresController.cs](https://github.com/cagilalsac/ETrade/blob/main/MVC/Controllers/StoresController.cs)\
[MVC/Views/Stores/Index.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Stores/Index.cshtml)\
[MVC/Views/Stores/Details.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Stores/Details.cshtml)\
[MVC/Views/Stores/Create.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Stores/Create.cshtml)\
[MVC/Views/Stores/Edit.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Stores/Edit.cshtml)\
[MVC/Views/Stores/Delete.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Stores/Delete.cshtml)\
9.9) Add Stores link to Bootstrap Navbar in [MVC/Views/Shared/Layout.cshtml](https://github.com/cagilalsac/ETrade/blob/main/MVC/Views/Shared/_Layout.cshtml)