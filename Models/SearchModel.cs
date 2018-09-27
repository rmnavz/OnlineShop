using System.Collections.Generic;

namespace Online_Shop.Models
{
    public class SearchModel
    {
        public virtual ProductModel Product { get; set; }
        public virtual StoreModel Store { get; set; }
        public virtual string Name { get; set; }
        public virtual string Link { get; set; }
    }
}