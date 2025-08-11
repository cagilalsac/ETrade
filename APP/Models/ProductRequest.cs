using CORE.APP.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    /// <summary>
    /// Represents a request to create or update a product, including details such as name, price, stock, and category.
    /// </summary>
    /// <remarks>This class is used to encapsulate the data required for product-related operations, such as
    /// creating or updating product information in a system. It includes fields for product identification, pricing,
    /// inventory, and optional metadata like descriptions and expiration dates. Some fields, such as <see
    /// cref="Name"/>, are required, while others are optional and can be null.</remarks>
    public class ProductRequest : Request
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// This field is required and has a maximum length of 200 and minimum length of 3 characters.
        /// Optional parameters such as ErrorMessage can also be used to customize the attribute's behavior.
        /// </summary>
        [DisplayName("Product Name")]
        [Required(ErrorMessage = "{0} is required!")] // {0}: display name ("Product Name") if exists otherwise property name ("Name")
        [StringLength(200, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")] // {1}: 200, {2}: 3
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
        [DisplayName("Unit Price")]
        [Range(0.01, 1000000, ErrorMessage = "{0} must be between {1} and {2}!")] // {0}: "Unit Price", {1}: 0.01, {2}: 1000000
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the optional amount of stock available for this product.
        /// Null if the stock amount is not being tracked.
        /// </summary>
        [DisplayName("Stock Amount")]
        [Range(1, double.MaxValue, ErrorMessage = "{0} must be a positive number!")]
        public int? StockAmount { get; set; }

        /// <summary>
        /// Gets or sets the optional expiration date of the product.
        /// Useful for perishable goods or time-sensitive inventory.
        /// </summary>
        [DisplayName("Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the product is still being sold or supported.
        /// If false, the product may be discontinued.
        /// </summary>
        [DisplayName("Continued")]
        public bool IsContinued { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the related category.
        /// Associates the product with a category for classification purposes.
        /// Defined as nullable and required therefore if the user selects "-- Select --" 
        /// option through the select HTML tag for category in the view, Required attribute's 
        /// error message can be shown.
        /// </summary>
        [DisplayName("Category")]
        [Required(ErrorMessage = "{0} is required!")]
        public int? CategoryId { get; set; }
    }
}
