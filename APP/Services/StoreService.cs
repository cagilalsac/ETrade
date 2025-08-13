using APP.Domain;
using APP.Models;
using CORE.APP.Models;
using CORE.APP.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    /// <summary>
    /// Provides business logic and data access operations for <see cref="Store"/> entities.
    /// Implements CRUD operations and queries for stores, including related product information.
    /// </summary>
    public class StoreService : DbService, IService<StoreRequest, StoreResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreService"/> class with the specified database context.
        /// </summary>
        /// <param name="db">The database context to be used by the service.</param>
        public StoreService(Db db) : base(db)
        {
        }

        /// <summary>
        /// Retrieves a list of all stores, including their associated products.
        /// The result is ordered by virtual status (virtual stores first) and then by name.
        /// Each store includes a count of its products and a formatted string of product names.
        /// </summary>
        /// <returns>
        /// A list of <see cref="StoreResponse"/> objects representing all stores with summary product information.
        /// </returns>
        public List<StoreResponse> GetList()
        {
            // Query stores, including their related ProductStores and Products (Entity Framework Eeager Loading).
            // Order by IsVirtual (virtual stores first), then by Name.
            // Project each store to a StoreResponse with product count and product names.
            return _db.Stores.Include(storeEntity => storeEntity.ProductStores)
                .ThenInclude(productStoreEntity => productStoreEntity.Product)
                .OrderByDescending(storeEntity => storeEntity.IsVirtual)
                .ThenBy(storeEntity => storeEntity.Name)
                .Select(storeEntity => new StoreResponse
                {
                    Id = storeEntity.Id,
                    Name = storeEntity.Name,
                    IsVirtual = storeEntity.IsVirtual,
                    IsVirtualF = storeEntity.IsVirtual ? "Virtual" : "Physical",

                    // Count of products associated with the store.
                    ProductCount = storeEntity.ProductStores.Count,

                    // HTML-formatted list of product names, separated by line breaks.
                    Products = string.Join("<br>", storeEntity.ProductStores
                        .Select(productStoreEntity => productStoreEntity.Product.Name)
                        .ToList())
                }).ToList();
        }

        /// <summary>
        /// Retrieves a single store by its unique identifier, including its associated products.
        /// </summary>
        /// <param name="id">The unique identifier of the store to retrieve.</param>
        /// <returns>
        /// A <see cref="StoreResponse"/> object representing the specified store, or null if not found.
        /// </returns>
        public StoreResponse GetItem(int id)
        {
            // Find the store by ID, including related ProductStores and Products.
            var entity = _db.Stores.Include(storeEntity => storeEntity.ProductStores)
                .ThenInclude(productStoreEntity => productStoreEntity.Product)
                .SingleOrDefault(storeEntity => storeEntity.Id == id);

            if (entity is null)
                return null;

            // Map the store entity to a StoreResponse with product summary.
            return new StoreResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                IsVirtual = entity.IsVirtual,
                IsVirtualF = entity.IsVirtual ? "Virtual" : "Physical",

                // Count of products associated with the store.
                ProductCount = entity.ProductStores.Count,

                // HTML-formatted list of product names, separated by line breaks.
                Products = string.Join("<br>", entity.ProductStores
                    .Select(productStoreEntity => productStoreEntity.Product.Name)
                    .ToList())
            };
        }

        /// <summary>
        /// Creates a new store using the provided request data.
        /// Ensures that no other store with the same name and virtual status exists.
        /// </summary>
        /// <param name="request">The request object containing the data for the new store.</param>
        /// <returns>
        /// A <see cref="CommandResponse"/> indicating the result of the creation operation.
        /// </returns>
        public CommandResponse Create(StoreRequest request)
        {
            // Check for an existing store with the same name (case-insensitive) and virtual status.
            var existingEntity = _db.Stores.SingleOrDefault(
                s => s.Name.ToUpper() == request.Name.ToUpper() && s.IsVirtual == request.IsVirtual);

            if (existingEntity is not null)
                return Error($"{(existingEntity.IsVirtual ? "Virtual" : "Physical")} store with the same name exists!");

            // Create the new store entity.
            var entity = new Store
            {
                Name = request.Name?.Trim(),
                IsVirtual = request.IsVirtual
            };
            _db.Stores.Add(entity);
            _db.SaveChanges();

            return Success("Store created successfully.", entity.Id);
        }

        /// <summary>
        /// Retrieves a store for editing by its unique identifier.
        /// Returns a <see cref="StoreRequest"/> populated with the store's current data.
        /// </summary>
        /// <param name="id">The unique identifier of the store to edit.</param>
        /// <returns>
        /// A <see cref="StoreRequest"/> object for editing, or null if the store is not found.
        /// </returns>
        public StoreRequest GetItemForEdit(int id)
        {
            // Find the store by ID.
            var entity = _db.Stores.Find(id);
            if (entity is null)
                return null;

            // Map the store entity to a StoreRequest for editing.
            return new StoreRequest
            {
                Id = entity.Id,
                Name = entity.Name,
                IsVirtual = entity.IsVirtual
            };
        }

        /// <summary>
        /// Updates an existing store using the provided request data.
        /// Ensures that no other store with the same name and virtual status exists.
        /// </summary>
        /// <param name="request">The request object containing updated values for the store.</param>
        /// <returns>
        /// A <see cref="CommandResponse"/> indicating the result of the update operation.
        /// </returns>
        public CommandResponse Update(StoreRequest request)
        {
            // Check for another store with the same name (case-insensitive) and virtual status, excluding the current store.
            var existingEntity = _db.Stores
                .SingleOrDefault(s => s.Id != request.Id && s.Name.ToUpper() == request.Name.ToUpper() && s.IsVirtual == request.IsVirtual);

            if (existingEntity is not null)
                return Error($"{(existingEntity.IsVirtual ? "Virtual" : "Physical")} store with the same name exists!");

            // Find the store to update.
            var entity = _db.Stores.Find(request.Id);
            if (entity is null)
                return Error("Store not found!");

            // Update store properties.
            entity.Name = request.Name?.Trim();
            entity.IsVirtual = request.IsVirtual;
            _db.Stores.Update(entity);
            _db.SaveChanges();

            return Success("Store updated successfully.", entity.Id);
        }

        /// <summary>
        /// Deletes an existing store by its unique identifier.
        /// Also removes all associated <see cref="ProductStore"/> relationships.
        /// </summary>
        /// <param name="id">The unique identifier of the store to delete.</param>
        /// <returns>
        /// A <see cref="CommandResponse"/> indicating the result of the deletion operation.
        /// </returns>
        public CommandResponse Delete(int id)
        {
            // Find the store by ID, including related ProductStores.
            var entity = _db.Stores.Include(s => s.ProductStores).SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return Error("Store not found!");

            // Remove all product-store relationships for this store.
            _db.ProductStores.RemoveRange(entity.ProductStores);

            // Remove the store itself.
            _db.Stores.Remove(entity);
            _db.SaveChanges();

            return Success("Store deleted successfully.");
        }
    }
}
