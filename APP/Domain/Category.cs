using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Domain
{
    /// <summary>
    /// Represents a category that groups related products.
    /// Inherits common properties from the base <see cref="Entity"/> class (e.g., Id).
    /// Categories help in organizing and classifying products.
    /// </summary>
    public class Category : Entity
    {
        /// <summary>
        /// Gets or sets the name of the category.
        /// This property is required and must be maximum 100 characters in length.
        /// Used to identify and label product categories.
        /// Required and StringLength attributes (data annotations) for string properties are commonly used in entities.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the optional description of the category.
        /// Can be used to provide more context about the category's purpose or contents.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the collection of products associated with this category.
        /// This establishes a one-to-many relationship where one category can contain multiple products.
        /// </summary>
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
