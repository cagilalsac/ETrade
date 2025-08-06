using APP.Models;
using CORE.APP.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    /// <summary>
    /// MVC Controller responsible for handling category-related views and actions.
    /// Uses a generic service for data operations on categories.
    /// </summary>
    public class CategoriesController : Controller
    {
        private readonly IService<CategoryRequest, CategoryQueryResponse> _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name="service">
        /// The injected service used for performing category operations (CRUD).
        /// </param>
        public CategoriesController(IService<CategoryRequest, CategoryQueryResponse> service)
        {
            _service = service;
        }

        /// <summary>
        /// Displays the list of all categories.
        /// </summary>
        /// <returns>The CategoryList view with the list of categories.</returns>
        // GET Route: Categories/List
        //[HttpGet] // Optional to write because the default is HttpGet if not written.
        public IActionResult List()
        {
            var list = _service.GetList();

            // Returns the view with the given name (CategoryList) if the view's name is different than the action name
            // in the Categories folder (controller name) in the Views folder.
            return View("CategoryList", list);
        }

        /// <summary>
        /// Displays the details of a specific category.
        /// </summary>
        /// <param name="id">The ID of the category to display.</param>
        /// <returns>The Details view with the category's information.</returns>
        // GET Route: Categories/Details/1
        public IActionResult Details(int id)
        {
            var item = _service.GetItem(id);

            // Returns the view which has the action name (Details)
            // in the Categories folder (controller name) in the Views folder.
            return View(item);
        }

        /// <summary>
        /// Displays the create form for a new category.
        /// </summary>
        /// <returns>The Create view.</returns>
        // GET Route: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the form POST request to create a new category.
        /// Validates the input and shows error messages if creation fails.
        /// </summary>
        /// <param name="request">The category data submitted from the form.</param>
        /// <returns>
        /// Redirects to List action on success; returns the Create view with validation messages on failure.
        /// </returns>
        // HttpPost because the form in the Create view has a post method, ValidateAntiForgeryToken checks if the request comes from the Create view.
        // POST Route: Categories/Create
        [HttpPost, ValidateAntiForgeryToken] 
        public IActionResult Create(CategoryRequest request)
        {
            // Request model validation using data annotations.
            if (ModelState.IsValid)
            {
                var response = _service.Create(request);
                if (response.IsSuccessful)
                {
                    // Way 1: Redirect to the given action of the controller
                    // return RedirectToAction("List");
                    // Way 2: Redirect to the given action of the given controller
                    // return RedirectToAction("List", "Categories");
                    // Way 3: Redirect using method name for maintainability
                    return RedirectToAction(nameof(List));
                }

                // Show extra service-level error to the user in validation summary of the view
                ModelState.AddModelError("", response.Message);
            }

            // Return form with validation errors
            return View(request);
        }

        /// <summary>
        /// Displays the edit form for an existing category. Edit view will be scaffolded.
        /// </summary>
        /// <returns>The Edit view.</returns>
        // GET Route: Categories/Edit/1
        public IActionResult Edit(int id)
        {
            var item = _service.GetItemForEdit(id);
            return View(item);
        }

        /// <summary>
        /// Handles the form POST request to update an existing category.
        /// Validates the input and shows error messages if update fails.
        /// </summary>
        /// <param name="request">The updated category data submitted from the form.</param>
        /// <returns>
        /// Redirects to List action on success; returns the Edit view with validation messages on failure.
        /// </returns>
        // HttpPost because the form in the Edit view has a post method, ValidateAntiForgeryToken checks if the request comes from the Edit view.
        // POST Route: Categories/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = _service.Update(request);
                if (response.IsSuccessful)
                {
                    return RedirectToAction(nameof(List));
                }

                ModelState.AddModelError("", response.Message);
            }

            return View(request);
        }

        /// <summary>
        /// Displays the delete confirmation page for a category. Delete view will be scaffolded.
        /// </summary>
        /// <param name="id">The ID of the category to be deleted.</param>
        /// <returns>The Delete view with the category details.</returns>
        // GET Route: Categories/Delete/1
        public IActionResult Delete(int id)
        {
            var item = _service.GetItem(id);
            return View(item);
        }

        /// <summary>
        /// Handles the POST request to confirm and perform category deletion.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>Redirects to the List action after deletion.</returns>
        // HttpPost because the form in the Delete view has a post method, ValidateAntiForgeryToken checks if the request comes from the Delete view.
        // ActionName changes the route by the given paramater, here the route becomes Categories/Delete/1 instead of Categories/DeleteConfirmed/1.
        // POST Route: Categories/Delete/1
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var response = _service.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
