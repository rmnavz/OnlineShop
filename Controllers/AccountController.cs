using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Online_Shop.Code.Security;
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

        public ActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid){ return View(model); }

            AccountModel result = Db.Accounts.Where(m => m.EmailAddress == model.EmailAddress).FirstOrDefault();

            if(result == null && PasswordSecurity.VerifyPassword(model.Password, result.Password)){ return View(model); }

            var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, result.getName()),
                new Claim(ClaimTypes.Email, result.EmailAddress)
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);
            
            return RedirectToAction("Index","Home");
        }

        public ActionResult Register() => View();

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid){ return View(model); }

            AccountModel result = Db.Accounts.Where(m => m.EmailAddress == model.EmailAddress).FirstOrDefault();

            if(result != null){ return View(model); }

            AccountModel Account = new AccountModel();

            Account.FirstName = model.FirstName;
            Account.LastName = model.LastName;
            Account.Nickname = model.Nickname;
            Account.EmailAddress = model.EmailAddress;
            Account.Password = PasswordSecurity.CreateHash(model.Password);
            Account.DateCreated = DateTime.Now;

            await Db.Accounts.AddAsync(Account);
            await Db.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index","Index");
        }
    }
}
