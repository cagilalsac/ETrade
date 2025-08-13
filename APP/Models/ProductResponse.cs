using CORE.APP.Models;
using System.ComponentModel;

namespace APP.Models
{
    /// <summary>
    /// Represents the response model for a product, typically used for API or UI data transfer.
    /// </summary>
    public class ProductResponse : Response
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the formatted unit price of the product.
        /// </summary>
        [DisplayName("Unit Price")]
        public string UnitPriceF { get; set; }

        /// <summary>
        /// Gets or sets the amount of stock available for this product.
        /// </summary>
        [DisplayName("Stock Amount")]
        public int? StockAmount { get; set; }

        /// <summary>
        /// Gets or sets the expiration date of the product.
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the formatted expiration date of the product.
        /// </summary>
        [DisplayName("Expiration Date")]
        public string ExpirationDateF { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the product is still being sold or supported.
        /// </summary>
        public bool IsContinued { get; set; }

        /// <summary>
        /// Gets or sets a text indicating whether the product is still being sold or supported.
        /// </summary>
        [DisplayName("Continued")]
        public string IsContinuedF { get; set; }

        /// <summary>
        /// Gets or sets the id of the related category.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the related category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the list of store IDs where this product is available.
        /// This property contains the unique identifiers of the stores associated with the product,
        /// enabling efficient lookups and data binding scenarios where only store references are needed.
        /// </summary>
        public List<int> StoreIds { get; set; } = new List<int>();

        /// <summary>
        /// Gets or sets the list of detailed store information where this product is available.
        /// Each item in the list provides comprehensive data about a store, such as its name, type, and product summary.
        /// Useful for displaying store details in UI views or API responses alongside the product.
        /// </summary>
        public List<StoreResponse> Stores { get; set; } = new List<StoreResponse>();
    }
}

