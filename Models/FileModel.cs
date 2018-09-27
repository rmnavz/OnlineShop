using System;
using System.ComponentModel.DataAnnotations;

namespace Online_Shop.Models
{
    public class ImageModel
    {
        [Key] 
        public int ID { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Image { get; set; }
        public string FileLocation { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ProductModel Product { get; set; }
        public virtual VariantModel Variant { get; set; }
        public virtual StoreModel Store { get; set; }
    }
}