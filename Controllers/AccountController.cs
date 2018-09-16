using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Online_Shop.Models;
using Online_Shop.ViewModels;

namespace Online_Shop.Controllers
{
    public class AccountController : Controller
    {
        #region Protected Members

        protected DatabaseContext Db;

        #endregion
        
        public AccountController(DatabaseContext DbContext)
        {
            Db = DbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid){ return View(model); }

            AccountModel account = Db.Accounts.Where(m => m.EmailAddress == model.EmailAddress && m.Password == model.Password).FirstOrDefault();

            if(account == null){ return View(model); }

            var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, account.getName()),
                new Claim(ClaimTypes.Email, account.EmailAddress),
                new Claim(ClaimTypes.Role, account.getRole())
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);
            
            return RedirectToAction("Index","Index");
        }

        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index","Index");
        }
    }
}
