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
        /// This field is required and must be maximum 100 characters in length.
        /// Used to validate and transfer the category name input from clients.
        /// </summary>
        [Required, StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the optional description of the category.
        /// Used to transfer additional context or information about the category.
        /// </summary>
        public string Description { get; set; }
    }

    /*
    Some commonly used data annotation attributes in C#:
    [Required]           // Ensures the property must have a value.
    [StringLength]       // Sets maximum (and optionally minimum) length for strings.
    [MinLength]          // Specifies the minimum length for strings or collections.
    [MaxLength]          // Specifies the maximum length for strings or collections.
    [Range]              // Defines the allowed range for numeric values.
    [RegularExpression]  // Validates the property value against a regex pattern.
    [EmailAddress]       // Validates that the property is a valid email address.
    [Phone]              // Validates that the property is a valid phone number.
    [Url]                // Validates that the property is a valid URL.
    [Compare]            // Compares two properties for equality (e.g., password confirmation).
    [DisplayName]        // Sets a friendly name for the property (used in error messages/UI).
    [DataType]           // Specifies the data type (e.g., Date, Time, Currency) for formatting/UI hints.
    */
}
