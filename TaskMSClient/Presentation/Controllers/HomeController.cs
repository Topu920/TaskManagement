using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.Models;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }


        
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Privacy(string logInDto)
        {
            var user = JsonConvert.DeserializeObject<LogInDto>(logInDto);
            HttpContext.Session.SetString("userId", user.userId.ToString());
            HttpContext.Session.SetString("empId", user.empId.ToString());
            HttpContext.Session.SetString("empNo", user.empNo.ToString());
            HttpContext.Session.SetString("userName", user.userName);
            HttpContext.Session.SetString("usrDesign", user.usrDesign);
            return RedirectToAction(nameof(Dashboard));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}