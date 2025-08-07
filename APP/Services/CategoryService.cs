using APP.Domain;
using APP.Models;
using CORE.APP.Models;
using CORE.APP.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    /// <summary>
    /// Service class responsible for handling CRUD operations for Category entities.
    /// Implements the IService interface using CategoryRequest and CategoryQueryResponse types.
    /// </summary>
    public class CategoryService : ETradeDbService, IService<CategoryRequest, CategoryQueryResponse>
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
        /// <returns>A list of <see cref="CategoryQueryResponse"/> objects representing the categories.</returns>
        public List<CategoryQueryResponse> GetList()
        {
            return _db.Categories
                .OrderBy(entity => entity.Name)
                .Select(entity => new CategoryQueryResponse()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description
                }).ToList();
        }

        /// <summary>
        /// Retrieves a single category based on the provided ID.
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <returns>
        /// A <see cref="CategoryQueryResponse"/> object if the category is found; otherwise, <c>null</c>.
        /// </returns>
        public CategoryQueryResponse GetItem(int id)
        {
            var entity = _db.Categories.SingleOrDefault(entity => entity.Id == id);
            if (entity is null)
                return null;

            return new CategoryQueryResponse()
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
            var entity = _db.Categories.SingleOrDefault(entity => entity.Id == id);
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
            if (_db.Categories.Any(entity => entity.Name.ToUpper() == request.Name.ToUpper().Trim()))
                return Error("Category with the same name exists!");

            var entity = new Category()
            {
                Name = request.Name?.Trim(),
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
            if (_db.Categories.Any(entity => entity.Id != request.Id && entity.Name.ToUpper() == request.Name.ToUpper().Trim()))
                return Error("Category with the same name exists!");

            var entity = _db.Categories.SingleOrDefault(entity => entity.Id == request.Id);
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
                .Include(entity => entity.Products) // get the relational product data related to this category (Entity Framework Eager Loading)
                .SingleOrDefault(entity => entity.Id == id);

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

            return Success("Category deleted successfully.", entity.Id);
        }
    }
}
