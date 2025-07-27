using CORE.APP.Models;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    /// <summary>
    /// Represents the request model used for creating or updating a category.
    /// Inherits from the base <see cref="Request"/> class, which may include metadata such as request IDs or timestamps.
    /// This DTO (Data Transfer Object) is typically used in API layer or service layer communication.
    /// </summary>
    public class CategoryRequest : Request
    {
        /// <summary>
        /// Gets or sets the name of the category.
        /// This field is required and must be between 3 and 100 characters in length.
        /// Used to validate and transfer the category name input from clients.
        /// </summary>
        [Required, StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the optional description of the category.
        /// Used to transfer additional context or information about the category.
        /// </summary>
        public string Description { get; set; }
    }
}
