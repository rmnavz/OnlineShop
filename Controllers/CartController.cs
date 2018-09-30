using Microsoft.AspNetCore.Mvc;
using Online_Shop.ViewModels;

namespace Online_Shop.Controllers
{
    public class CartController : ControllerBase
    {
        public CartController(DatabaseContext DbContext) : base(DbContext)
        {
        }

        public IActionResult Add(ProductViewModel model)
        {
            return View();
        }
    }
}