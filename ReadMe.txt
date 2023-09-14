MvcWebUI (ASP.NET Core Web App Model View Controller), Business (Class Library), DataAccess (Class Library) ve Core (Class Library) projeleri oluşturulduktan sonra 
solution build edilir ve DataAccess projesine Core, Business projesine Core ve DataAccess, MvcWebUI projesine ise Business, DataAccess ve Core
projeleri referans olarak eklenir:

1) İster solution altında Core adında yeni bir proje oluşturulur, istenirse de Core projesi dışarıdan solution'a eklenebilir.

2) DataAccess katmanında Core katmanındaki Record'dan miras alan Entity'ler oluşturulur.

3) Core katmanına Microsoft.EntityFrameworkCore ile DataAccess katmanına Microsoft.EntityFrameworkCore.SqlServer ve Microsoft.EntityFrameworkCore.Tools paketleri 
NuGet'ten indirilir. .NET versiyonu hangisi ise NuGet'ten o versiyon numarası ile başlayan paketlerin son versiyonları indirilmelidir.

4) DataAccess katmanında DbContext'ten türeyen Context ve içerisindeki DbSet'ler ile connection string bilgisini de içeren DbContextOptions tipindeki objenin 
Context'e constructor üzerinden enjekte edilmesini sağlayacak parametreli constructor oluşturulur, daha sonra MvcWebUI katmanında Program.cs'teki
IoC Container'da DbContext için bağımlılık yönetimi gerçekleştirilir.

5) MvcWebUI katmanındaki appsettings.json ile eğer istenirse appsettings.Development.json içerisine connection string 
server=.\\SQLEXPRESS;database=ETrade;user id=sa;password=sa;multipleactiveresultsets=true;trustservercertificate=true; 
formatta yazılır. appsettings.json dosyası Properties klasöründeki launchSettings.json dosyasında konfigüre edilen production (canlı) profilinde, 
appsettings.Development.json dosyası ise development (geliştirme) profilinde proje çalıştırıldığında kullanılacaktır.
Ayrıca launchSettings.json dosyasına view'larda değişiklik yapıldığında projenin tekrar başlatılma gerekliliğini ortadan kaldırmak için
"ASPNETCORE_HOSTINGSTARTUPASSEMBLIES": "Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation" satırının eklenmesi
ve NuGet üzerinden Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation kütüphanesinin MvcWebUI projesine indirilmesi faydalı olacaktır.

6) MvcWebUI katmanına Microsoft.EntityFrameworkCore.Design paketi NuGet'ten indirilerek MvcWebUI projesi Startup Project yapılır. 
Tools -> NuGet Package Manager -> Package Manager Console açılır, Default project DataAccess seçilir 
ve önce örneğin add-migration v1 daha sonra update-database komutları çalıştırılır.
Entity'ler veya DbSet'ler üzerinde yapılan her değişiklik için örneğin add-migration v2 daha sonra da update-database çalıştırılmalıdır.

7) DataAccess katmanında entity'ler üzerinden Core'daki RepoBase'den miras alan concrete (somut) repository (Repo) oluşturulur 
ve MvcWebUI katmanında Program.cs'teki IoC Container'da bağımlılığı yönetilir.

8) Business katmanında entity'ler üzerinden model class'ları Core katmanındaki Record'dan miras alacak şekilde oluşturulur,
eğer istenirse MvcWebUI katmanında view'larda kullanılmak üzere formatlama, ilişkili referans özellikleri kullanma, vb. için yeni özellikler eklenebilir.

9) Business katmanında model'ler üzerinden entity <-> model dönüşümlerini gerçekleştirip DataAccess katmanındaki Repo üzerinden
veritabanı işlemleri gerçekleştirmek için Core'daki IService'i implemente eden interface'ler ile bu interface'leri implemente
eden concrete (somut) service'ler oluşturulur ve MvcWebUI katmanında Program.cs'teki IoC Container'da bağımlılıkları yönetilir.

10) MvcWebUI katmanında yönetilecek model için controller ile ilgili action ve view'ları oluşturularak ilgili service constructor üzerinden enjekte edilir
ve controller action'larında methodları kullanılarak model objeleri üzerinden işlemler (örneğin CRUD) gerçekleştirilir.

View <-> Controller (Action) <-> Service (Model) <-> Repository (Entity) <-> DbContext (Entity) <-> Database

11) MvcWebUI katmanındaki Views -> Shared klasörü altındaki projede tüm oluşturulan view'ların bir şablon içerisinde gösterilmesini
sağlayan _Layout.cshtml view'ı içerisinde controller ve action'lar üzerinden, örneğin menüye link'ler eklenir.

12) Eğer istenirse veritabanındaki tüm verilerin sıfırdan oluşturulmasını sağlayan, örneğin MvcWebUI katmanında Areas klasöründeki Database area'sı içerisinde,
bir controller ve aksiyonu yazılabilir.

Konu Anlatımlı Proje Geliştirme Aşamaları:
1) DataAccess -> Entities -> Product ve Category entity'leri
2) DataAccess -> Contexts -> ETradeContext -> Products ve Categories DbSet'leri ve OnModelCreating methodundaki ilişkileri
(MvcWebUI -> Program.cs -> IoC Container region ile MvcWebUI -> appsettings.json veya istenirse appsetting.Development.json -> ConnectionStrings)
3) Business -> Models -> ProductModel
4) Business -> Services -> ProductService -> Query (MvcWebUI -> Program.cs -> IoC Container region)
5) MvcWebUI -> Controllers -> ProductsController -> Index
6) MvcWebUI -> Views -> Products -> Index.cshtml
7) MvcWebUI -> Controllers -> ProductsController -> Details
8) MvcWebUI -> Views -> Products -> Details.cshtml
9) Business -> Services -> ProductService -> Add
10) MvcWebUI -> Controllers -> ProductsController -> Create
11) MvcWebUI -> Views -> Products -> Create.cshtml
12) Business -> Models -> CategoryModel
13) Business -> Services -> CategoryService -> Query (MvcWebUI -> Program.cs -> IoC Container region)
14) Business -> Services -> ProductService -> Update
15) MvcWebUI -> Controllers -> ProductsController -> Edit
16) MvcWebUI -> Views -> Products -> Edit.cshtml
17) Business -> Services -> ProductService -> Delete
18) MvcWebUI -> Controllers -> ProductsController -> Delete
19) MvcWebUI -> Views -> Products -> Delete.cshtml

20) MvcWebUI -> Controllers -> Categories -> MVC Controller Entity Framework Scaffolding
21) Business -> Services -> CategoryService -> Add
22) Business -> Services -> CategoryService -> Update
23) Business -> Services -> CategoryService -> Delete

24) DataAccess -> Entities -> Store entity
25) DataAccess -> Contexts -> ETradeContext -> Stores DbSet'i ve OnModelCreating methodundaki ilişkisi
26) Business -> Models -> StoreModel
27) Business -> Services -> StoreService -> Query (MvcWebUI -> Program.cs -> IoC Container region)
28) Business -> Services -> StoreService -> Add
29) Business -> Services -> StoreService -> Update
30) Business -> Services -> StoreService -> Delete
31) MvcWebUI -> Controllers -> Stores -> Index, Details, Create, Edit ve Delete action'ları
32) MvcWebUI -> Views -> Stores -> Index, _Details ve _CreateEdit view'ları

Kullanıcı Yönetimi: İstenirse kullanıcı yönetimi için Microsoft'un Identity Framework kütüphanesi kullanılabilir.
33) DataAccess -> Entities -> User, UserDetail, Country, City ve Role entity'leri 
34) DataAccess -> Contexts -> ETradeContext -> Users, UserDetails, Countries, Cities ve Roles DbSet'leri ve OnModelCreating methodundaki ilişkileri
35) DataAccess -> Enums -> Roles
36) DataAccess -> Enums -> Sex
37) Business -> Models -> UserModel
38) Business -> Models -> UserDetailModel
39) Business -> Models -> CountryModel
40) Business -> Models -> CityModel
41) Business -> Models -> Account -> AccountLoginModel
42) Business -> Services -> UserService -> Query (MvcWebUI -> Program.cs -> IoC Container region)
43) Business -> Services -> AccountService -> Login (MvcWebUI -> Program.cs -> IoC Container region)
44) Business -> Models -> Account -> AccountRegisterModel
45) Business -> Services -> UserService -> Add
46) Business -> Services -> AccountService -> Register
47) Business -> Services -> CountryService -> Query (MvcWebUI -> Program.cs -> IoC Container region)
48) Business -> Services -> CountryService -> GetList
49) Business -> Services -> CountryService -> GetItem (id parametreli)
50) Business -> Services -> CityService -> Query (MvcWebUI -> Program.cs -> IoC Container region)
51) Business -> Services -> CityService -> GetList (countryId parametreli)
52) Business -> Services -> CityService -> GetList (parametresiz)
53) Business -> Services -> CityService -> GetItem (id parametreli)
54) MvcWebUI -> Controllers -> CitiesController -> GetCities (countryId parametreli)
55) MvcWebUI -> Areas -> Accounts -> Controllers -> Home -> Register Action'ları ve View'ı
56) MvcWebUI -> Areas -> Accounts -> Controllers -> Home -> Login Action'ları ve View'ı (MvcWebUI -> Program.cs -> Authentication)
57) MvcWebUI -> Views -> Shared -> _Layout.cshtml -> User üzerinden giriş yapan kullanıcıya göre menü link'lerinin gösterimi
58) MvcWebUI -> Views -> Home -> Welcome.cshtml -> User üzerinden kullanıcı adı gösterimi
59) MvcWebUI -> Controllers -> Categories, Products ve Stores -> Authorize attribute'ları
60) MvcWebUI -> Views -> Categories ve Products -> Index.cshtml ve Details.cshtml -> User üzerinden giriş yapan kullanıcıya göre link'lerin gösterimi

Raporlama:
61) Business -> Services -> CategoryService -> GetListAsync methodu
62) MvcWebUI -> ViewComponents -> CategoriesViewComponent
63) MvcWebUI -> Views -> Shared -> Components -> Categories -> Default.cshtml
64) Business -> Models -> Report -> ReportItemModel ve ReportFilterModel
65) Business -> Services -> Report -> ReportService -> GetList (MvcWebUI -> Program.cs -> IoC Container region)
66) MvcWebUI -> Areas -> Report -> Models -> HomeIndexViewModel
67) MvcWebUI -> Areas -> Report -> Controllers -> Home -> Index
68) MvcWebUI -> Areas -> Report -> Views -> Home -> Index

Tek İmaj Yükleme (Veritabanı):
69) DataAccess -> Entities -> Product -> Image ve ImageExtension özellikleri eklenmesi
70) Business -> Models -> ProductModel -> Image, ImageExtension ve ImgSrcDisplay özellikleri eklenmesi
71) Business -> Services -> ProductService -> Query methodunda Image, ImageExtension ve ImgSrcDisplay özellikleri atanması
72) Business -> Services -> ProductService -> Add methodunda Image ve ImageExtension özellikleri atanması
73) Business -> Services -> ProductService -> Update methodunda Image ve ImageExtension özellikleri atanması
74) Business -> Services -> ProductService -> DeleteImage
75) MvcWebUI -> appsettings.json ve appsettings.Development.json -> AppSettings section
76) MvcWebUI -> Settings -> AppSettings
77) MvcWebUI -> Program.cs -> AppSettings region
78) MvcWebUI -> Views -> _ViewImports.cshtml -> AppSettings için MvcWebUI.Settings using'i eklenmesi
79) MvcWebUI -> Views -> Shared -> _Layout.cshtml -> AppSettings.Title kullanımı
80) MvcWebUI -> Views -> Home -> Welcome.cshtml -> AppSettings.Title kullanımı
81) MvcWebUI -> Controllers -> ProductsController -> UpdateImage ve DeleteImage
82) MvcWebUI -> Views -> Products -> Index ve _Details view'larında ImgSrcDisplay özelliği kullanımı
83) MvcWebUI -> Views -> Products -> Create ve Edit view'larında Image özelliği kullanımı, Edit view'ında Delete Image link'i

Bootstrap Icons:
84) MvcWebUI -> Views -> Shared -> _Layout.cshtml
85) MvcWebUI -> Views -> Products -> Index.cshtml
86) MvcWebUI -> Views -> Categories -> Index.cshtml
87) MvcWebUI -> Views -> Stores -> Index.cshtml
88) MvcWebUI -> Areas -> Report -> Views -> Home -> Index.cshtml

Session:
89) Business -> Models -> Cart -> CartItemModel
90) MvcWebUI -> Areas -> Cart -> Controllers -> HomeController
91) MvcWebUI -> Areas -> Account -> Controllers -> Login (Post) Action -> Sid type'lı claim'in eklenmesi 
92) Business -> Services -> AccountService -> Login -> User'ın Id'sinin atanması
93) MvcWebUI -> Areas -> Cart -> Views -> Cart.cshtml
94) MvcWebUI -> Views -> Shared -> _Layout.cshtml -> Cart link'i eklenmesi
95) MvcWebUI -> Views -> Products -> Index.cshtml -> Add to Cart link'i eklenmesi
96) MvcWebUI -> Program.cs -> Session region
97) Business -> Models -> Cart -> CartItemGroupByModel
98) MvcWebUI -> Areas -> Cart -> Controllers -> HomeController -> GroupBy method
99) MvcWebUI *> Areas -> Cart -> Views -> CartGroupBy.cshtml