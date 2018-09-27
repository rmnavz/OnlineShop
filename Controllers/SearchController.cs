using System.Collections.Generic;
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

        [Route("Search")]
        public IActionResult Index(string searchstring)
        {
            char[] separators  = {' ',',','/'};
            var keywords = new List<string>();
            var SearchData = new List<SearchModel>();

            keywords.Add(searchstring);

            foreach(char separator in separators)
            {
                keywords.AddRange(searchstring.Split(separator));
            }

            foreach(string keyword in keywords)
            {
                foreach(ProductModel product in Db.Products.Where(p => p.Name.Contains(keyword)).ToList())
                {
                    if(SearchData.Where(s => s.Product.ID == product.ID).ToList().Count == 0)
                    {
                        SearchData.Add(new SearchModel(){ Product = product});
                    }
                }

                foreach(ProductModel product in Db.Products.Where(p => p.Description.Contains(keyword)).ToList())
                {
                    if(SearchData.Where(s => s.Product.ID == product.ID).ToList().Count == 0)
                    {
                        SearchData.Add(new SearchModel(){ Product = product});
                    }
                }

                foreach(StoreModel store in Db.Stores.Where(s => s.Name.Contains(keyword)).ToList())
                {
                    if(SearchData.Where(s => s.Store.ID == store.ID).ToList().Count == 0)
                    {
                        SearchData.Add(new SearchModel(){ Store = store});
                    }
                }

                foreach(StoreModel store in Db.Stores.Where(s => s.Description.Contains(keyword)).ToList())
                {
                    if(SearchData.Where(s => s.Store.ID == store.ID).ToList().Count == 0)
                    {
                        SearchData.Add(new SearchModel(){ Store = store});
                    }
                }
            }

            return PartialView(SearchData);
        }
    }
}