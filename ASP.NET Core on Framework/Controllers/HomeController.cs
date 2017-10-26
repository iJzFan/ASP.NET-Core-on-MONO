using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_Core_on_Framework.Models;

namespace ASP.NET_Core_on_Framework.Controllers
{
    public class HomeController : Controller
    {
        private MySqlDbContext _context;

        public HomeController(MySqlDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            var cangjingkong = _context.Teachers.FirstOrDefault();
            var dongnidamu = _context.Students.FirstOrDefault();

            ViewData["Teacher"] = cangjingkong.Name + "的学生是" + cangjingkong.Students.FirstOrDefault().Name;

            ViewData["Student"] = dongnidamu.Name + "的老师是" + dongnidamu.Teachers.FirstOrDefault().Name;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
