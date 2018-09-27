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
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<ProductModel> Products { get; set; }
        public virtual ICollection<ImageModel> Images { get; set; }
        public virtual ICollection<ReviewModel> Reviews { get; set; }
        public virtual ICollection<ViewCountModel> ViewCounts { get; set; }

        public virtual ICollection<StoreSellerModel> StoreSeller { get; set; }
    }

    public class StoreSellerModel
    {
        public int StoreID { get; set; }
        public virtual StoreModel Store { get; set; }
        
        public int SellerID { get; set; }
        public virtual SellerModel Seller { get; set; }
    }
}