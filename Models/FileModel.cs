using System;
using System.ComponentModel.DataAnnotations;

namespace Online_Shop.Models
{
    public class ImageModel
    {
        [Key] 
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public DateTime DateCreated { get; set; }

        public ProductModel Product { get; set; }
        public VariantModel Variant { get; set; }
        public StoreModel Store { get; set; }
    }
}