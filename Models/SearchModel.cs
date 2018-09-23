using System.Collections.Generic;

namespace Online_Shop.Models
{
    public class SearchModel
    {
        public ICollection<ProductModel> Products { get; set; }
        public ICollection<StoreModel> Stores { get; set; }
        public int Score { get; set; }
    }
}