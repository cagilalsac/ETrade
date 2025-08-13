using CORE.APP.Domain;

namespace APP.Domain
{
    /// <summary>
    /// Represents the association entity for the many-to-many relationship between products and stores.
    /// Inherits common entity properties from the base <see cref="Entity"/> class (e.g., Id).
    /// </summary>
    public class ProductStore : Entity
    {
        /// <summary>
        /// Gets or sets the foreign key for the related product.
        /// Associates this entry with a specific <see cref="Product"/>.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for the related product.
        /// Used for ORM mapping to enable navigation from a product-store association to its product.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the related store.
        /// Associates this entry with a specific <see cref="Store"/>.
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for the related store.
        /// Used for ORM mapping to enable navigation from a product-store association to its store.
        /// </summary>
        public Store Store { get; set; }
    }
}
