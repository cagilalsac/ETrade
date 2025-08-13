using Microsoft.EntityFrameworkCore;

namespace APP.Domain
{
    /// <summary>
    /// Represents the application's database context for Entity Framework Core.
    /// Provides access to the database and manages entity sets for querying and saving instances of <see cref="Product"/> and <see cref="Category"/>.
    /// Inherits from <see cref="DbContext"/>, the core class responsible for database communication.
    /// </summary>
    public class Db : DbContext
    {
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> representing the collection of products in the database.
        /// Enables querying, adding, updating, and deleting <see cref="Product"/> entities.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> representing the collection of categories in the database.
        /// Enables querying, adding, updating, and deleting <see cref="Category"/> entities.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> representing the collection of stores in the database.
        /// Enables querying, adding, updating, and deleting <see cref="Store"/> entities.
        /// </summary>
        public DbSet<Store> Stores { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> representing the collection of product stores in the database.
        /// Enables querying, adding, updating, and deleting <see cref="ProductStore"/> entities.
        /// </summary>
        public DbSet<ProductStore> ProductStores { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Db"/> class using the specified options.
        /// These options are typically configured in the application's dependency injection setup,
        /// and may include settings such as the database provider and connection string.
        /// </summary>
        /// <param name="options">The options to configure the context.</param>
        public Db(DbContextOptions options) : base(options)
        {
        }
    }
}
