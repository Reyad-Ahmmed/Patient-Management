using Microsoft.AspNetCore.Mvc;

namespace Customer_Info_Frontend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
