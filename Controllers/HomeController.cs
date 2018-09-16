using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Online_Shop.Models;

namespace Online_Shop.Controllers
{
    public class HomeController : Controller
    {
        #region Protected Members

        protected DatabaseContext Db;

        #endregion
        
        public HomeController(DatabaseContext DbContext)
        {
            Db = DbContext;
            DbContext.Database.EnsureCreated();
        }

        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

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
