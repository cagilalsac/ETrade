#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CORE.APP.Services;
using APP.Models;

// Generated from Custom MVC Template.

namespace MVC.Controllers
{
    public class StoresController : Controller
    {
        // Service injections:
        private readonly IService<StoreRequest, StoreResponse> _storeService;

        /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
        //private readonly IService<EntityRequest, EntityResponse> _EntityService;

        public StoresController(
			IService<StoreRequest, StoreResponse> storeService

            /* Can be uncommented and used for many to many relationships. "Entity" may be replaced with the related entiy name in the controller and views. */
            //, IService<EntityRequest, EntityResponse> EntityService
        )
        {
            _storeService = storeService;

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

        // GET: Stores
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _storeService.GetList();
            return View(list);
        }

        // GET: Stores/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _storeService.GetItem(id);
            return View(item);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Stores/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(StoreRequest store)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _storeService.Create(store);
                if (result.IsSuccessful)
                {
                    SetTempData(result.Message);
                    return RedirectToAction(nameof(Details), new { id = result.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(store);
        }

        // GET: Stores/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _storeService.GetItemForEdit(id);
            SetViewData();
            return View(item);
        }

        // POST: Stores/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(StoreRequest store)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _storeService.Update(store);
                if (result.IsSuccessful)
                {
                    SetTempData(result.Message);
                    return RedirectToAction(nameof(Details), new { id = result.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(store);
        }

        // GET: Stores/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _storeService.GetItem(id);
            return View(item);
        }

        // POST: Stores/Delete
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _storeService.Delete(id);
            SetTempData(result.Message);
            return RedirectToAction(nameof(Index));
        }
	}
}
