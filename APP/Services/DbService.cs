using APP.Domain;
using CORE.APP.Services;
using System.Globalization;

namespace APP.Services
{
    /// <summary>
    /// Provides a base implementation for services that require access to the ETrade database context.
    /// Inherits from <see cref="ServiceBase"/> and supplies shared infrastructure to derived service classes.
    /// </summary>
    public abstract class DbService : ServiceBase
    {
        /// <summary>
        /// The protected database context instance used by derived services to access database entities.
        /// </summary>
        protected readonly Db _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbService"/> class with the specified database context.
        /// </summary>
        /// <param name="db">The database context to be used by the service.</param>
        protected DbService(Db db)
        {
            _db = db;

            // The CultureInfo (e.g., for formatting, parsing, etc.) is initialized to "en-US" in the ServiceBase constructor.
            // To override it (e.g., to Turkish), assign it explicitly like:
            // CultureInfo = new CultureInfo("tr-TR");
        }
    }
}
