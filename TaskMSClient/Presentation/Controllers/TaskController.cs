using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
    }
}
