using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using MSIT155site.Models;
using MSIT155site.Models.DTO;
using System.Text;

namespace MSIT155site.Controllers
{
    public class ApiController : Controller
    {
        public MyDBContext _context;
        public IWebHostEnvironment _environment;
        public ApiController(MyDBContext context,IWebHostEnvironment environment) {
          _context=context;
          _environment=environment;
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
        public IActionResult Register(Member _user,IFormFile Avatar)
        {
            // return Content($"Hello {_user.Name},您{_user.Age}歲了,電子郵件是{_user.Email}", "text/plain", Encoding.UTF8);
            // return Content($"{_user.Avatar?.FileName}-{_user.Avatar?.Length}-{_user.Avatar?.ContentType}","text/plain", Encoding.UTF8);
            string fileName = "empty.jpg";
            if(Avatar != null)
            {
                fileName = Avatar.FileName;
            }
            string filePath = Path.Combine(_environment.WebRootPath, "uploads",fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Avatar?.CopyTo(fileStream);
            }
            
            _user.FileName = fileName;

            byte[]? imgByte = null;
            using (var Stream = new MemoryStream())
            {
                Avatar?.CopyTo(Stream);
                imgByte = Stream.ToArray();
            }
            _user.FileData = imgByte;

            _context.Members.Add(_user);
            _context.SaveChanges();

            return Content(filePath,"text/plain",Encoding.UTF8);
        }
        public IActionResult District(string city)
        {
            var districts = _context.Addresses.Where(c => c.City == city ).Select(s=>s.SiteId).Distinct();
            return Json(districts);
        }
        public IActionResult Road(string city,string sitId)
        {
            var roads = _context.Addresses.Where(c => c.City == city && c.SiteId == sitId).Select(r=>r.Road).Distinct();
            return Json(roads);
        }
        public IActionResult CheckAccount(Member _member)
        {
            var m =_context.Members.FirstOrDefault(member => member.Name == _member.Name);

            if (m != null ) { 
            return Content("1", "text/plain", Encoding.UTF8);
            }
            else
            {
            return Content("0", "text/plain", Encoding.UTF8);
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
        public IActionResult Spots([FromBody]SearchDTO _search) {

            var spots =_search.CategoryId == 0 ? _context.SpotImagesSpots : _context.SpotImagesSpots.Where(s=>s.CategoryId ==_search.CategoryId);

            if(!string.IsNullOrEmpty(_search.Keyword))
            {
                spots=spots.Where(s=>s.SpotTitle.Contains(_search.Keyword)|| s.SpotDescription.Contains(_search.Keyword));
            }

            switch (_search.SortBy)
            {
                case "spotTitle":
                    spots = _search.SortType == "asc" ? spots.OrderBy(s => s.SpotTitle) : spots.OrderByDescending(s => s.SpotTitle);
                    break;
                case "categoryId":
                    spots = _search.SortType == "asc" ? spots.OrderBy(s => s.CategoryId) : spots.OrderByDescending(s => s.CategoryId);
                    break;
                default:
                    spots = _search.SortType == "asc" ? spots.OrderBy(s => s.SpotId) : spots.OrderByDescending(s => s.SpotId);
                    break;
            }

            int totalCount = spots.Count();

            int pageSize = _search.PageSize ?? 9;

            int totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);

            int page = _search.Page ?? 1;

            spots= spots.Skip((int)(page-1)*pageSize).Take(pageSize);

            SpotsPagingDTO spotsPaging = new SpotsPagingDTO();
            spotsPaging.TotalPages = totalPages;
            spotsPaging.SpotsResult =spots.ToList();

            return Json(spotsPaging);
        }

        public IActionResult SpotTitle(string keyword)
        {
            var spotTitle = _context.Spots.Where(s => s.SpotTitle.Contains(keyword)).Select(s => s.SpotTitle).Take(5);
            return Json(spotTitle);
        }
    }
}
