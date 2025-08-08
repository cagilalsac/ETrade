using APP.Domain;
using APP.Models;
using APP.Services;
using CORE.APP.Services;
using Microsoft.EntityFrameworkCore;

// Create a WebApplicationBuilder, which sets up the application's services and configuration.
var builder = WebApplication.CreateBuilder(args);



// ---------------------------------------------------
// Add services to the container (Dependency Injection).
// ---------------------------------------------------
// Registers the application's DbContext (named 'Db') with the dependency injection container.
// Configures the DbContext to use SQL Server as the database provider.
// The connection string named "Db" is retrieved from the application's configuration settings (e.g., appsettings.json).
// This setup enables the application to connect to the specified SQL Server database when interacting with entity sets.
builder.Services.AddDbContext<Db>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Db")));

// Registers the CategoryService with the dependency injection container as a scoped service.
// It maps the generic interface IService<CategoryRequest, CategoryQueryResponse> to the concrete implementation CategoryService.
// Scoped lifetime means a new instance of CategoryService is created per client request.
// This enables constructor injection of IService<CategoryRequest, CategoryQueryResponse> wherever needed in the application.
builder.Services.AddScoped<IService<CategoryRequest, CategoryQueryResponse>, CategoryService>();

/*
 * Service Lifetimes in ASP.NET Core Dependency Injection:
 *
 * 1. AddScoped:
 *    - Lifetime: Scoped to a single HTTP request (or scope).
 *    - Behavior: Creates one instance of the service per HTTP request.
 *    - Use case: Use when you want to maintain state or dependencies that last only during a single request.
 *    - Example: DbContext, which should be shared across operations within a request.
 *
 * 2. AddSingleton:
 *    - Lifetime: Singleton for the entire application lifetime.
 *    - Behavior: Creates only one instance of the service for the whole app lifecycle.
 *    - Use case: Use for stateless services or global shared data/services.
 *    - Example: Caching services, configuration providers, logging services.
 *
 * 3. AddTransient:
 *    - Lifetime: Transient (short-lived).
 *    - Behavior: Creates a new instance every time the service is requested.
 *    - Use case: Use for lightweight, stateless services that are cheap to create.
 *    - Example: Utility/helper classes without state.
 *
 * Important Notes:
 * - Injecting a Scoped service into a Singleton can cause issues due to lifetime mismatch.
 * - ASP.NET Core DI container will warn about such mismatches.
 *
 * Summary:
 * | Method        | Lifetime                | Instance Created             | Typical Use Case                  |
 * |---------------|-------------------------|------------------------------|-----------------------------------|
 * | AddScoped     | Per HTTP request        | One instance per request     | DbContext, per-request services   |
 * | AddSingleton  | Application-wide        | One instance for app lifetime| Caching, config, logging          |
 * | AddTransient  | Every time requested    | New instance each time       | Lightweight stateless helpers     |
 */



// Adds support for controllers and views (MVC pattern).
// This registers the necessary services for model binding, validation, action execution, etc.
builder.Services.AddControllersWithViews();



// ---------------------------------------------------
// Build the WebApplication from the configured builder.
// ---------------------------------------------------
var app = builder.Build();



// ---------------------------------------------------
// Configure the HTTP request pipeline (middleware).
// ---------------------------------------------------
// Check if the environment is not Development (e.g., Production or Staging).
if (!app.Environment.IsDevelopment())
{
    // Use the custom error handler page at /Home/Error for unhandled exceptions.
    app.UseExceptionHandler("/Home/Error");

    // Use HTTP Strict Transport Security (HSTS) to inform browsers that the site should only be accessed using HTTPS.
    // The default duration is 30 days.
    app.UseHsts();
}



// Redirect all HTTP requests to HTTPS.
app.UseHttpsRedirection();



// Enable serving static files (e.g., CSS, JavaScript, images) from the wwwroot folder.
app.UseStaticFiles();



// Add routing middleware to match incoming requests to route templates.
app.UseRouting();



// Add authorization middleware to enforce authorization policies (if configured).
app.UseAuthorization();



// Define the default route for MVC controllers.
// The route template follows the pattern: /{controller}/{action}/{id?}
// If no controller or action is specified, it defaults to HomeController and Index action.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



// Run the application and start listening for incoming HTTP requests.
app.Run();
