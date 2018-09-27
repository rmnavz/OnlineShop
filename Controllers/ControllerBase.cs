using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Online_Shop.Code;
using Online_Shop.Models;

namespace Online_Shop.Controllers
{
    public class ControllerBase : Controller
    {
        #region Protected Members

        protected DatabaseContext Db;
        protected AccountModel CurrentAccount;

        #endregion

        #region Public Members
        #endregion
        public ControllerBase(DatabaseContext DbContext)
        {
            Db = DbContext;
            DbContext.Database.EnsureCreated();

            if(Db.Accounts.Where(a => a.Role == AccountRoles.SuperAdministrator).FirstOrDefault() == null)
            {
                
            }

            if(User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                CurrentAccount = Db.Accounts.Where(a => a.ID == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))).FirstOrDefault();
                ViewBag.CurrentAccount = CurrentAccount;
            }
        }

        public IActionResult ApplicationSetup()
        {
            if(Db.Accounts.Where(a => a.Role == AccountRoles.SuperAdministrator).FirstOrDefault() != null)
            {
                return RedirectToAction("Index","Home");
            }
            
            return View();
        }
    }
}