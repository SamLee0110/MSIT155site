using Microsoft.AspNetCore.Mvc;
using MSIT155site.Models;
using MSIT155site.Models.DTO;
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
            Thread.Sleep(5000);
            //int x=10;
            //int y=0;
            //int z = x / y;
            return Content("Content 測試!","text/plain",Encoding.UTF8);
           // return View();
        }
        public IActionResult Register(userDTO _user)
        {
            if (string.IsNullOrEmpty(_user.Name))
            {
                _user.Name = "guest";
            }
            return Content($"Hello {_user.Name},您{_user.Age}歲了","text/plain",Encoding.UTF8);
        }

        public IActionResult Cities()
        {
            var cities = _context.Addresses.Select(c => c.City).Distinct();
            return Json(cities);
        }

        public IActionResult Avatar(int id = 1)
        {
            Member? member = _context.Members.Find(id);
            if(member != null)
            {
                byte[] img = member.FileData;
                if(img != null)
                {
                    return File(img, "image/jpeg");
                }
            }
            return NotFound();
        }
    }
}
