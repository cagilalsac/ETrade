#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CORE.APP.Services;
using APP.Models;

// Generated from Custom MVC Template.

namespace MVC.Controllers
{
    public class ProductsController : Controller
    {
        // Service injections:
        private readonly IService<ProductRequest, ProductResponse> _productService;
        private readonly IService<CategoryRequest, CategoryResponse> _categoryService;

        /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
        //private readonly IService<EntityRequest, EntityResponse> _EntityService;

        public ProductsController(
			IService<ProductRequest, ProductResponse> productService
            , IService<CategoryRequest, CategoryResponse> categoryService

            /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
            //, EntityService EntityService
        )
        {
            _productService = productService;
            _categoryService = categoryService;

            /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
            //_EntityService = EntityService;
        }

        private void SetViewData()
        {
            /* 
            ViewBag and ViewData are the same collection (dictionary).
            They carry extra data other than the model from a controller action to its view, or between views.
            */

            // Related items service logic to set ViewData (Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["CategoryId"] = new SelectList(_categoryService.GetList(), "Id", "Name");
            
            /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
            //ViewBag.EntityIds = new MultiSelectList(_EntityService.GetList(), "Id", "Name");
        }

        private void SetTempData(string message, string key = "Message")
        {
            /*
            TempData is used to carry extra data to the redirected controller action's view.
            */

            TempData[key] = message;
        }

        // GET: Products
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _productService.GetList();
            return View(list);
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _productService.GetItem(id);
            return View(item);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Products/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(ProductRequest product)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _productService.Create(product);
                if (result.IsSuccessful)
                {
                    SetTempData(result.Message);
                    return RedirectToAction(nameof(Details), new { id = result.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _productService.GetItemForEdit(id);
            SetViewData();
            return View(item);
        }

        // POST: Products/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(ProductRequest product)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _productService.Update(product);
                if (result.IsSuccessful)
                {
                    SetTempData(result.Message);
                    return RedirectToAction(nameof(Details), new { id = result.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _productService.GetItem(id);
            return View(item);
        }

        // POST: Products/Delete
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _productService.Delete(id);
            SetTempData(result.Message);
            return RedirectToAction(nameof(Index));
        }
	}
}
