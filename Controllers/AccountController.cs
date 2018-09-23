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
    public class AccountController : ControllerBase
    {
        public AccountController(DatabaseContext DbContext) : base(DbContext)
        {
        }

        [Route("Account")]
        public IActionResult Index(int? ID)
        {
            var Account = (ID.HasValue) ? Db.Accounts.Where(a => a.ID == ID).FirstOrDefault() : CurrentAccount;

            if(Account == null) return RedirectToAction("Login");
            
            return View();
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid){ return View(model); }

            AccountModel result = Db.Accounts.Where(m => m.EmailAddress == model.EmailAddress).FirstOrDefault();

            if(result == null){ return View(model); }

            if(PasswordSecurity.VerifyPassword(model.Password, result.Password))
            {
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, result.getName()),
                    new Claim(ClaimTypes.Email, result.EmailAddress),
                    new Claim(ClaimTypes.Role, result.Role.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, result.ID.ToString())
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties{ IsPersistent = model.isRemembered }
                );
                
                return RedirectToAction("Index","Home");

            }

            return View(model);

        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid){ return View(model); }

            AccountModel result = Db.Accounts.Where(m => m.EmailAddress == model.EmailAddress).FirstOrDefault();

            if(result != null){ return View(model); }

            AccountModel Account = new AccountModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Nickname = model.Nickname,
                EmailAddress = model.EmailAddress,
                Password = PasswordSecurity.CreateHash(model.Password),
                Role = AccountRoles.Customer,
                Status = AccountStatus.Enabled,
                DateCreated = DateTime.Now
            };

            await Db.Accounts.AddAsync(Account);
            await Db.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index","Home");
        }

        public IActionResult Disabled() => View();
    }
}
