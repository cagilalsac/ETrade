namespace CORE.APP.Models
{
    /// <summary>
    /// Represents a base query response object that contains a unique identifier.
    /// This abstract class can be used as a base for various query response models 
    /// that require an identifier field.
    /// </summary>
    public abstract class QueryResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the response.
        /// Typically used to correlate responses with database entities.
        /// </summary>
        public int Id { get; set; }
    }
}
