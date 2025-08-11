namespace CORE.APP.Models
{
    /// <summary>
    /// Represents a base response object that contains a unique identifier.
    /// This abstract class can be used as a base for various response models 
    /// that require an identifier field.
    /// </summary>
    public abstract class Response
    {
        /// <summary>
        /// Gets or sets the unique identifier of the response.
        /// Typically used to correlate responses with database entities.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Constructor with parameter to set the Id from a sub (child) class
        /// constructor using Constructor Chaining.
        /// </summary>
        /// <param name="id">The unique identifier field.</param>
        protected Response(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Default constructor (constructor without any parameters)
        /// that will set the Id to the integer default value (0).
        /// </summary>
        protected Response()
        {
        }
    }
}
