using Microsoft.AspNetCore.Mvc;

namespace MSIT155site.Controllers
{
    public class HomeWorkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
