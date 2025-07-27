using CORE.APP.Models;

namespace APP.Models
{
    /// <summary>
    /// Represents the response model for category data sent back to clients.
    /// Inherits from the base <see cref="QueryResponse"/> class, which may contain common response metadata such as ID, status or error information.
    /// This DTO (Data Transfer Object) is typically used to structure the data returned by API endpoints or services.
    /// </summary>
    public class CategoryQueryResponse : QueryResponse
    {
        /// <summary>
        /// Gets or sets the name of the category.
        /// This property reflects the category's display or identifier name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the category.
        /// Provides additional details or context about the category.
        /// </summary>
        public string Description { get; set; }
    }
}
