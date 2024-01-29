using Microsoft.AspNetCore.Mvc;
using MSIT155site.Models;

namespace MSIT155site.Controllers
{
    public class ApiController : Controller
    {
        public MyDBContext _context;
        public ApiController(MyDBContext context) {
        _context=context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cities()
        {
            var cities = _context.Addresses.Select(c => c.City).Distinct();
            return Json(cities);
        }


    }
}
