using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Domain
{
    /// <summary>
    /// Represents a product entity in the system, typically used for inventory, sales, or catalog purposes.
    /// Inherits common entity properties from the base <see cref="Entity"/> class (e.g., Id).
    /// </summary>
    public class Product : Entity
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// This field is required and has a maximum length of 200 characters.
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the optional description of the product.
        /// This can provide additional details for display or management purposes.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// Represents the cost per unit and is used for pricing and transactions.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the optional amount of stock available for this product.
        /// Null if the stock amount is not being tracked.
        /// </summary>
        public int? StockAmount { get; set; }

        /// <summary>
        /// Gets or sets the optional expiration date of the product.
        /// Useful for perishable goods or time-sensitive inventory.
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the product is still being sold or supported.
        /// If false, the product may be discontinued.
        /// </summary>
        public bool IsContinued { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the related category.
        /// Associates the product with a category for classification purposes.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for the category to which this product belongs.
        /// Used for ORM mapping to enable category-product relationships.
        /// </summary>
        public Category Category { get; set; }
    }
}
