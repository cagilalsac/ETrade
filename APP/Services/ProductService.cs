using APP.Domain;
using APP.Models;
using CORE.APP.Models;
using CORE.APP.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace APP.Services
{
    /// <summary>
    /// Provides business logic and CRUD operations for <see cref="Product"/> entities.
    /// Implements the <see cref="IService{ProductRequest, ProductResponse}"/> interface.
    /// Handles product listing, retrieval, creation, update, and deletion, including
    /// mapping between entity and model types, and formatting data for UI display.
    /// </summary>
    public class ProductService : DbService, IService<ProductRequest, ProductResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class with the provided database context.
        /// </summary>
        /// <param name="db">The database context used for product operations.</param>
        public ProductService(Db db) : base(db)
        {
        }

        /// <summary>
        /// Retrieves all products with their related categories, ordered by continued status,
        /// stock amount, and name. Maps each product to a <see cref="ProductResponse"/> and
        /// formats fields for display (e.g., price, stock, expiration date).
        /// </summary>
        /// <returns>
        /// A list of <see cref="ProductResponse"/> objects representing the products.
        /// </returns>
        public List<ProductResponse> GetList()
        {
            // p: Product entity delegate
            return _db.Products.Include(p => p.Category)
                .OrderByDescending(p => p.IsContinued)
                .ThenBy(p => p.StockAmount)
                .ThenBy(p => p.Name)
                .Select(p => new ProductResponse()
                {
                    CategoryName = p.Category.Name,
                    CategoryId = p.CategoryId,
                    Description = p.Description,
                    Id = p.Id,
                    IsContinued = p.IsContinued,
                    Name = p.Name,
                    UnitPrice = p.UnitPrice,
                    ExpirationDate = p.ExpirationDate,

                    IsContinuedF = p.IsContinued ? "Continued" : "Discontinued",

                    // Managing nullable types for StockAmount:
                    // Way 1: Use .Value if not null, otherwise 0
                    //StockAmount = p.StockAmount is not null ? p.StockAmount.Value : 0,
                    // Way 2: Use .HasValue property
                    //StockAmount = p.StockAmount.HasValue ? p.StockAmount.Value : 0,
                    // Way 3: Null-coalescing operator (preferred for brevity)
                    StockAmount = p.StockAmount ?? 0,

                    // Formatting ExpirationDate:
                    // Way 1: Specify CultureInfo explicitly (e.g., "en-US" or "tr-TR")
                    //ExpirationDateF = p.ExpirationDate.HasValue ? p.ExpirationDate.Value.ToString("MM/dd/yyyy", new CultureInfo("en-US")) : string.Empty,
                    // Way 2: Use default CultureInfo set in ServiceBase (preferred for consistency), "MM/dd/yyyy HH:mm:ss" is used for date and time format
                    ExpirationDateF = p.ExpirationDate.HasValue ? p.ExpirationDate.Value.ToString("MM/dd/yyyy") : string.Empty,

                    // Formatting UnitPrice:
                    // Way 1: "N2" for two decimals, append currency manually
                    //UnitPriceF = p.UnitPrice.ToString("N2") + " Dollars"
                    // Way 2: "C2" for currency format with two decimals (preferred for localization)
                    UnitPriceF = p.UnitPrice.ToString("C2")
                }).ToList();
        }

        /// <summary>
        /// Retrieves a single product by its unique identifier, including its related category.
        /// Maps the product entity to a <see cref="ProductResponse"/> and formats fields for display.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>
        /// A <see cref="ProductResponse"/> object if found; otherwise, <c>null</c>.
        /// </returns>
        public ProductResponse GetItem(int id)
        {
            var entity = _db.Products.Include(p => p.Category).SingleOrDefault(p => p.Id == id);
            if (entity is null)
                return null;

            return new ProductResponse()
            {
                CategoryName = entity.Category.Name,
                CategoryId = entity.CategoryId,
                Description = entity.Description,
                Id = entity.Id,
                IsContinued = entity.IsContinued,
                Name = entity.Name,
                UnitPrice = entity.UnitPrice,
                ExpirationDate = entity.ExpirationDate,

                IsContinuedF = entity.IsContinued ? "Continued" : "Discontinued",

                // Managing nullable types for StockAmount:
                // Way 1: Use .Value if not null, otherwise 0
                //StockAmount = entity.StockAmount is not null ? entity.StockAmount.Value : 0,
                // Way 2: Use .HasValue property
                //StockAmount = entity.StockAmount.HasValue ? entity.StockAmount.Value : 0,
                // Way 3: Null-coalescing operator (preferred for brevity)
                StockAmount = entity.StockAmount ?? 0,

                // Formatting ExpirationDate:
                // Way 1: Specify CultureInfo explicitly (e.g., "en-US" or "tr-TR")
                //ExpirationDateF = entity.ExpirationDate.HasValue ? entity.ExpirationDate.Value.ToString("MM/dd/yyyy", new CultureInfo("en-US")) : string.Empty,
                // Way 2: Use default CultureInfo set in ServiceBase (preferred for consistency), "MM/dd/yyyy HH:mm:ss" is used for date and time format
                ExpirationDateF = entity.ExpirationDate.HasValue ? entity.ExpirationDate.Value.ToString("MM/dd/yyyy") : string.Empty,

                // Formatting UnitPrice:
                // Way 1: "N2" for two decimals, append currency manually
                //UnitPriceF = entity.UnitPrice.ToString("N2") + " Dollars"
                // Way 2: "C2" for currency format with two decimals (preferred for localization)
                UnitPriceF = entity.UnitPrice.ToString("C2")
            };
        }

        /// <summary>
        /// Creates a new product in the database.
        /// Checks for uniqueness of the product name (case-insensitive).
        /// Maps the request model to a new <see cref="Product"/> entity and saves it.
        /// </summary>
        /// <param name="request">The product data to be created.</param>
        /// <returns>
        /// A <see cref="CommandResponse"/> indicating success or failure.
        /// </returns>
        public CommandResponse Create(ProductRequest request)
        {
            if (_db.Products.Any(p => p.Name.ToUpper() == request.Name.ToUpper().Trim()))
                return Error("Product with the same name exists!");

            var entity = new Product()
            {
                CategoryId = request.CategoryId ?? 0,
                Description = request.Description?.Trim(),
                ExpirationDate = request.ExpirationDate,
                Id = request.Id,
                IsContinued = request.IsContinued,
                Name = request.Name?.Trim(),
                StockAmount = request.StockAmount,
                UnitPrice = request.UnitPrice
            };

            _db.Products.Add(entity); // _db.Add(entity); can also be written
            _db.SaveChanges();

            return Success("Product created successfully.", entity.Id);
        }

        /// <summary>
        /// Retrieves a single product by its unique identifier for editing.
        /// Maps the product entity to a <see cref="ProductRequest"/> model.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>
        /// A <see cref="ProductRequest"/> object if found; otherwise, <c>null</c>.
        /// </returns>
        public ProductRequest GetItemForEdit(int id)
        {
            var entity = _db.Products.Include(p => p.Category).SingleOrDefault(p => p.Id == id);
            if (entity is null)
                return null;

            return new ProductRequest()
            {
                CategoryId = entity.CategoryId,
                Description = entity.Description,
                Id = entity.Id,
                IsContinued = entity.IsContinued,
                Name = entity.Name,
                UnitPrice = entity.UnitPrice,
                ExpirationDate = entity.ExpirationDate,
                StockAmount = entity.StockAmount
            };
        }

        /// <summary>
        /// Updates an existing product's details in the database.
        /// Checks for duplicate product names (excluding the current record).
        /// Updates the entity with new values from the request and saves changes.
        /// </summary>
        /// <param name="request">The product data to update, including the ID.</param>
        /// <returns>
        /// A <see cref="CommandResponse"/> indicating the result of the update.
        /// </returns>
        public CommandResponse Update(ProductRequest request)
        {
            if (_db.Products.Any(p => p.Id != request.Id && p.Name.ToUpper() == request.Name.ToUpper().Trim()))
                return Error("Product with the same name exists!");

            var entity = _db.Products.Find(request.Id);
            entity.CategoryId = request.CategoryId ?? 0;
            entity.Description = request.Description?.Trim();
            entity.ExpirationDate = request.ExpirationDate;
            entity.Id = request.Id;
            entity.IsContinued = request.IsContinued;
            entity.Name = request.Name?.Trim();
            entity.StockAmount = request.StockAmount;
            entity.UnitPrice = request.UnitPrice;

            _db.Products.Update(entity); // _db.Update(entity); can also be written
            _db.SaveChanges();

            return Success("Product updated successfully.", entity.Id);
        }

        /// <summary>
        /// Deletes a product from the database by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>
        /// A <see cref="CommandResponse"/> indicating the result of the deletion.
        /// </returns>
        public CommandResponse Delete(int id)
        {
            var entity = _db.Products.Find(id);
            if (entity is null)
                return null;

            _db.Products.Remove(entity); // _db.Remove(entity); can also be written
            _db.SaveChanges();

            return Success("Product deleted successfully.");
        }
    }
}
