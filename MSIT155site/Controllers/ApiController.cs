using Microsoft.AspNetCore.Mvc;
using MSIT155site.Models;
using System.Text;

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
            return Content("Content 測試!","text/plain",Encoding.UTF8);
           // return View();
        }

        public IActionResult Cities()
        {
            var cities = _context.Addresses.Select(c => c.City).Distinct();
            return Json(cities);
        }


    }
}
