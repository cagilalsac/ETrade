using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    /// <summary>
    /// The HomeController handles requests for the default home pages of the application,
    /// including the Index, Privacy, and Error views.
    /// </summary>
    public class HomeController : Controller
    {
        // Logger instance for recording application runtime information, errors, and diagnostics.
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Constructor for HomeController.
        /// The ILogger is injected via dependency injection to enable logging capabilities.
        /// </summary>
        /// <param name="logger">An ILogger instance for HomeController.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Action for the home page.
        /// Returns the default "Index" view.
        /// </summary>
        /// <returns>View for the Index page.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Action for the privacy policy page.
        /// Returns the "Privacy" view.
        /// </summary>
        /// <returns>View for the Privacy page.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Action for handling application errors.
        /// This method is decorated with the ResponseCache attribute to prevent caching of the response.
        /// It provides an ErrorViewModel to the view, including the current request ID for debugging purposes.
        /// </summary>
        /// <returns>Error view populated with error details.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
