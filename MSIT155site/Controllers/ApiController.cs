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
        public IActionResult Register(Member _member)
        {            
            return Content($"Hello {_member.Name},您{_member.Age}歲了,電子郵件是{_member.Email}", "text/plain", Encoding.UTF8);
        }
        public IActionResult CheckAccount(Member _member)
        {
            var m =_context.Members.FirstOrDefault(member => member.Name == _member.Name);

            if (m != null) { 
            return Content("用戶已經存在,請重新確認", "text/plain", Encoding.UTF8);
            }
            else
            {
            return Content("此姓名可以使用", "text/plain", Encoding.UTF8);
            }
            
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
