using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class CalendarController : Controller
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
