#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CORE.APP.Services;
using APP.Models;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class CategoriesTestController : Controller
    {
        // Service injections:
        private readonly IService<CategoryRequest, CategoryQueryResponse> _categoryService;

        /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
        //private readonly IService<EntityRequest, EntityQueryResponse> _EntityService;

        public CategoriesTestController(
			IService<CategoryRequest, CategoryQueryResponse> categoryService

            /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
            //, EntityService EntityService
        )
        {
            _categoryService = categoryService;

            /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
            //_EntityService = EntityService;
        }

        private void SetViewData()
        {
            // Related items service logic to set ViewData (Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
            //ViewBag.EntityIds = new MultiSelectList(_EntityService.GetList(), "Id", "Name");
        }

        // GET: CategoriesTest
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _categoryService.GetList();
            return View(list);
        }

        // GET: CategoriesTest/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _categoryService.GetItem(id);
            return View(item);
        }

        // GET: CategoriesTest/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: CategoriesTest/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CategoryRequest category)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _categoryService.Create(category);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = category.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(category);
        }

        // GET: CategoriesTest/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _categoryService.GetItemForEdit(id);
            SetViewData();
            return View(item);
        }

        // POST: CategoriesTest/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryRequest category)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _categoryService.Update(category);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = category.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(category);
        }

        // GET: CategoriesTest/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _categoryService.GetItem(id);
            return View(item);
        }

        // POST: CategoriesTest/Delete
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _categoryService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
