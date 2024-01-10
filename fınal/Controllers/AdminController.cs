using Microsoft.AspNetCore.Mvc;

namespace fınal.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
