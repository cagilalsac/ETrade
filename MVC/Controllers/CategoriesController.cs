using APP.Models;
using CORE.APP.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IService<CategoryRequest, CategoryQueryResponse> _service;

        public CategoriesController(IService<CategoryRequest, CategoryQueryResponse> service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetList());
        }
    }
}
