using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Online_Shop.Models
{
    public class StoreModel
    {
        [Key] 
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ProductModel> Products { get; set; }
        public ICollection<SellerModel> Sellers { get; set; }
    }
}