using APP.Domain;
using APP.Models;
using CORE.APP.Models;
using CORE.APP.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    /// <summary>
    /// Service class responsible for handling CRUD operations for Category entities.
    /// Implements the IService interface using CategoryRequest and CategoryResponse types.
    /// </summary>
    public class CategoryService : DbService, IService<CategoryRequest, CategoryResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class with the provided database context.
        /// </summary>
        /// <param name="db">The database context used for performing operations.</param>
        public CategoryService(Db db) : base(db)
        {
        }

        /// <summary>
        /// Retrieves a list of all categories ordered by their name.
        /// </summary>
        /// <returns>A list of <see cref="CategoryResponse"/> objects representing the categories.</returns>
        public List<CategoryResponse> GetList()
        {
            return _db.Categories
                .OrderBy(categoryEntity => categoryEntity.Name)
                .Select(categoryEntity => new CategoryResponse()
                {
                    Id = categoryEntity.Id,
                    Name = categoryEntity.Name,
                    Description = categoryEntity.Description
                }).ToList();
        }

        /// <summary>
        /// Retrieves a single category based on the provided ID.
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <returns>
        /// A <see cref="CategoryResponse"/> object if the category is found; otherwise, <c>null</c>.
        /// </returns>
        public CategoryResponse GetItem(int id)
        {
            // Way 1:
            //Category entity = _db.Categories.SingleOrDefault(categoryEntity => categoryEntity.Id == id);
            // Way 2:
            var entity = _db.Categories.SingleOrDefault(categoryEntity => categoryEntity.Id == id);

            /*
            Find: Finds an entity with the given primary key value.
            Returns null if not found. Uses the context's cache before querying the database.
            Example: var product = dbContext.Products.Find(5);
            
            Single: Returns the only element that matches the specified condition.
            Throws an exception if no element or more than one element is found.
            Example: var product = dbContext.Products.Single(p => p.Id == 5);
            
            SingleOrDefault: Returns the only element that matches the specified condition, or null if no such element exists.
            Throws an exception if more than one element is found.
            Example: var product = dbContext.Products.SingleOrDefault(p => p.Id == 5);
            
            First: Returns the first element that matches the specified condition.
            Throws an exception if no element is found.
            Example: var product = dbContext.Products.First();
            Example: var product = dbContext.Products.First(p => p.IsContinued);
            
            FirstOrDefault: Returns the first element that matches the specified condition, or null if no such element exists.
            Example: var product = dbContext.Products.FirstOrDefault();
            Example: var product = dbContext.Products.FirstOrDefault(p => p.IsContinued);
            
            Last: Returns the last element that matches the specified condition.
            Throws an exception if no element is found. Usually requires an OrderBy clause.
            Example: var product = dbContext.Products.Last();
            Example: var product = dbContext.Products.OrderBy(p => p.Id).Last(p => p.IsContinued);
            
            LastOrDefault: Returns the last element that matches the specified condition, or null if no such element exists.
            Usually requires an OrderBy clause.
            Example: var product = dbContext.Products.LastOrDefault();
            Example: var product = dbContext.Products.OrderBy(p => p.Id).LastOrDefault(p => p.IsContinued);

            SingleOrDefault is generally preferred.
            */

            if (entity is null)
                return null;

            return new CategoryResponse()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }

        /// <summary>
        /// Retrieves a single category based on the provided ID.
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <returns>
        /// A <see cref="CategoryRequest"/> object if the category is found; otherwise, <c>null</c>.
        /// </returns>
        public CategoryRequest GetItemForEdit(int id)
        {
            var entity = _db.Categories.SingleOrDefault(categoryEntity => categoryEntity.Id == id);
            if (entity is null)
                return null;

            return new CategoryRequest()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }

        /// <summary>
        /// Creates a new category in the database.
        /// Validates to ensure category name uniqueness (case-insensitive).
        /// </summary>
        /// <param name="request">The category data to be created.</param>
        /// <returns>
        /// A <see cref="CommandResponse"/> indicating success or failure of the operation.
        /// </returns>
        public CommandResponse Create(CategoryRequest request)
        {
            // Way 1:
            //var existingEntity = _db.Categories.SingleOrDefault(categoryEntity => categoryEntity.Name.ToUpper() == request.Name.ToUpper().Trim());
            //if (existingEntity is null) // if (existingEntity == null) can also be written
            //    return Error("Category with the same name exists!");
            // Way 2: ToLower string method can also be used for both sides instead of ToUpper
            if (_db.Categories.Any(categoryEntity => categoryEntity.Name.ToUpper() == request.Name.ToUpper().Trim()))
                return Error("Category with the same name exists!");

            var entity = new Category()
            {
                // Way 1: can be used since request.Name is required and cannot be null
                //Name = request.Name.Trim(),
                // Way 2:
                Name = request.Name?.Trim(), // if request.Name value is null assign null, else assign the trimmed value

                Description = request.Description?.Trim()
            };

            _db.Add(entity);
            _db.SaveChanges();

            return Success("Category created successfully.", entity.Id);
        }

        /// <summary>
        /// Updates an existing category's details.
        /// Checks for duplicate names (excluding current record).
        /// </summary>
        /// <param name="request">The category data to update, including the ID.</param>
        /// <returns>
        /// A <see cref="CommandResponse"/> indicating the result of the update.
        /// </returns>
        public CommandResponse Update(CategoryRequest request)
        {
            if (_db.Categories.Any(categoryEntity => categoryEntity.Id != request.Id && categoryEntity.Name.ToUpper() == request.Name.ToUpper().Trim()))
                return Error("Category with the same name exists!");

            var entity = _db.Categories.SingleOrDefault(categoryEntity => categoryEntity.Id == request.Id);
            if (entity is null)
                return Error("Category not found!");

            entity.Name = request.Name?.Trim();
            entity.Description = request.Description?.Trim();

            _db.Update(entity);
            _db.SaveChanges();

            return Success("Category updated successfully.", entity.Id);
        }

        /// <summary>
        /// Deletes a category and all related products from the database.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>
        /// A <see cref="CommandResponse"/> indicating the result of the deletion.
        /// </returns>
        public CommandResponse Delete(int id)
        {
            var entity = _db.Categories
                .Include(categoryEntity => categoryEntity.Products) // get the relational product data related to this category (Entity Framework Eager Loading)
                .SingleOrDefault(categoryEntity => categoryEntity.Id == id);

            if (entity is null)
                return Error("Category not found!");

            // Relational Products must be included to the query
            // Way 1: Remove all related products before deleting the category
            //_db.Products.RemoveRange(entity.Products);
            // Way 2: Check if there are any related products to the category entity before deleting, if any don't delete the category entity
            if (entity.Products.Count > 0) // if (entity.Products.Any()) can also be written
                return Error("Category can't be deleted because it has relational products!");

            // Delete the category
            _db.Categories.Remove(entity);
            _db.SaveChanges();

            return Success("Category deleted successfully.");
        }
    }
}
