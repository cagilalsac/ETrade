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
        [DisplayName("Category")]
        public string CategoryName { get; set; }
    }
}

