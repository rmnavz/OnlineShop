using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Online_Shop.Models;

namespace Online_Shop.Controllers
{
    public class SearchController : ControllerBase
    {
        public SearchController(DatabaseContext DbContext) : base(DbContext)
        {
        }

        [HttpGet]
        public IActionResult Index(string searchstring)
        {
            char[] separators  = {' ',',','/'};
            string[] keywords;

            foreach(char separator in separators)
            {
                keywords = searchstring.Split(separator);
            }

            var SearchData = new SearchModel()
            {
                Products = Db.Products.Where(p => 
                    p.Name.Contains(searchstring) ||
                    p.Description.Contains(searchstring)
                    ).ToList(),
                Stores = Db.Stores.Where(s =>
                    s.Name.Contains(searchstring) ||
                    s.Description.Contains(searchstring)
                    ).ToList()
            };

            return PartialView();
        }
    }
}