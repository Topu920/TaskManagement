using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        
        public IActionResult GetProject(Guid id)
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            @ViewBag.Id = id.ToString();
            return View();
        }
    }
}
