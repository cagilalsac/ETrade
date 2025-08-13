using CORE.APP.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    /// <summary>
    /// Represents the data transfer object (DTO) used for creating or updating a store via forms in views.
    /// Inherits from the CORE Request abstract class and encapsulates the input fields required from the user 
    /// when submitting store-related data.
    /// </summary>
    public class StoreRequest : Request
    {
        /// <summary>
        /// Gets or sets the name of the store.
        /// This field is required and must not exceed 150 characters.
        /// Validation attributes ensure user-friendly error messages in views.
        /// </summary>
        [Required(ErrorMessage = "{0} is required!"), StringLength(150, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the store is virtual (online).
        /// Used to distinguish between physical and online stores in the application.
        /// </summary>
        [DisplayName("Virtual")]
        public bool IsVirtual { get; set; }
    }
}
