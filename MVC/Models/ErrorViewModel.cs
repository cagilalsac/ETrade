namespace MVC.Models
{
    /// <summary>
    /// ViewModel used to represent error information to be displayed in the error view.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// The unique identifier for the current request.
        /// This is useful for tracing and diagnosing errors that occurred during a specific request.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Indicates whether the RequestId should be shown.
        /// Returns true if the RequestId is not null or empty.
        /// Typically used in the view to conditionally display the request ID to the user or for logging purposes.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
