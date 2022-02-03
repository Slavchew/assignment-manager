using AssignmentManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentManager.Web.Controllers
{
    public class ClassesController : Controller
    {
        private readonly IClassesService classesService;

        public ClassesController(IClassesService classesService)
        {
            this.classesService = classesService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Welcome()
        {
            return View();
        }
    }
}
