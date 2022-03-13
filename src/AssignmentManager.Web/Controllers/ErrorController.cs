using Microsoft.AspNetCore.Mvc;

namespace AssignmentManager.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/404")]
        public IActionResult PageNotFound()
        {
            return View("NotFound");
        }
    }
}
