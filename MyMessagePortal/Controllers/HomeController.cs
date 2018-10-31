using Microsoft.AspNetCore.Mvc;

namespace MyMessagePortal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("ObservedList", "Channel");
            }

            return View();
        }
    }
}
