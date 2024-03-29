﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSIT155site.Models;
using System.Diagnostics;

namespace MSIT155site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public MyDBContext _context;

        public HomeController(ILogger<HomeController> logger, MyDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult JsonRead()
        {
            return View();
        }
        public IActionResult First()
        {
            return View();
        }
        public IActionResult Register() {
            return View();
        }
        public IActionResult Address()
        {
            return View();
        }
        
        public IActionResult Travel()
        {
            return View();
        }
        public IActionResult Avatar()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Spots()
        {
            return View();
        }
        public IActionResult AutoComplete()
        {
            return View();
        }
        public IActionResult Cors()
        {
            return View(); 
            
        }
    }
}
