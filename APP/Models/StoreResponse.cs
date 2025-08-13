using CORE.APP.Models;
using System.ComponentModel;

namespace APP.Models
{
    /// <summary>
    /// Represents the data transfer object (DTO) used for returning store information to the client,
    /// typically for display in views or API responses.
    /// Inherits from CORE Response abstract class and includes store details and summary information 
    /// about associated products.
    /// </summary>
    public class StoreResponse : Response
    {
        /// <summary>
        /// Gets or sets the name of the store.
        /// Used for display in UI components and reports.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the store is virtual (online).
        /// Used to distinguish between physical and online stores in the application logic.
        /// </summary>
        public bool IsVirtual { get; set; }

        /// <summary>
        /// Gets or sets a formatted string representation of the store's virtual status.
        /// Typically used for display purposes in views (e.g., "Virtual" or "Physical" instead of true/false).
        /// </summary>
        [DisplayName("Status")]
        public string IsVirtualF { get; set; }

        /// <summary>
        /// Gets or sets a string containing a summary or list of products associated with the store.
        /// This may be a comma-separated list of product names or a formatted summary for display in views.
        /// </summary>
        public string Products { get; set; }

        /// <summary>
        /// Gets or sets the total number of products associated with the store.
        /// Useful for quick reference and display in summary tables or dashboards.
        /// </summary>
        [DisplayName("Product Count")]
        public int ProductCount { get; set; }
    }
}
