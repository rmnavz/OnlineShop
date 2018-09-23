using Microsoft.AspNetCore.Mvc;
using Online_Shop.ViewModels;

namespace Online_Shop.Controllers
{
    public class VariantController : ControllerBase
    {
        public VariantController(DatabaseContext DbContext) : base(DbContext)
        {
        }
        public IActionResult Create(int ID)
        {
            ViewData["VariantID"] = ID;
            return PartialView();
        }
    }
}