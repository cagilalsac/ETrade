namespace CORE.APP.Models
{
    /// <summary>
    /// Represents the result of executing a command operation (e.g., create, update, delete).
    /// Includes unique identifier, success status and a operation result message.
    /// </summary>
    public class CommandResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the response.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets a value indicating whether the command was executed successfully.
        /// </summary>
        public bool IsSuccessful { get; }

        /// <summary>
        /// Gets a message that provides additional information about the command execution.
        /// This may include error details, confirmation messages, or custom status descriptions.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResponse"/> class.
        /// </summary>
        /// <param name="isSuccessful">Indicates whether the command was successful.</param>
        /// <param name="message">Optional message related to the result of the command.</param>
        /// <param name="id">Optional identifier associated with the response or entity.</param>
        public CommandResponse(bool isSuccessful, string message = "", int id = 0)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            Id = id;
        }
    }
}
