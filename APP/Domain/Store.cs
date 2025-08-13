using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Domain
{
    /// <summary>
    /// Represents a store entity in the system, which can be either a physical or virtual (online) store.
    /// Inherits common entity properties from the base <see cref="Entity"/> class (e.g., Id).
    /// </summary>
    public class Store : Entity
    {
        /// <summary>
        /// Gets or sets the name of the store.
        /// This field is required and has a maximum length of 150 characters.
        /// </summary>
        [Required, StringLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the store is virtual (online).
        /// If false, the store is considered a physical location.
        /// </summary>
        public bool IsVirtual { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="ProductStore"/> entities that associate products with this store.
        /// This establishes a many-to-many relationship between stores and products.
        /// </summary>
        public List<ProductStore> ProductStores { get; set; } = new List<ProductStore>();

        /// <summary>
        /// Gets or sets the list of product IDs associated with this store.
        /// Setting this property updates the <see cref="ProductStores"/> collection accordingly.
        /// There is no column for this property in the Stores table since NotMapped is defined.
        /// </summary>
        [NotMapped]
        public List<int> ProductIds
        {
            get => ProductStores.Select(productStore => productStore.ProductId).ToList();
            set => ProductStores = value.Select(productId => new ProductStore() { ProductId = productId }).ToList();
        }
    }
}
