using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Online_Shop.Models
{
    public class ProductModel
    {
        [Key] 
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public ProductStatus Status { get; set; }

        public int StoreID { get; set; }
        public StoreModel Store { get; set; }

        public ICollection<ImageModel> Images { get; set; }
        public ICollection<VariantModel> Variants { get; set; }
    }

    public class VariantModel
    {
        [Key] 
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public CultureInfo Currency { get; set; }
        public DateTime DateCreated { get; set; }

        public int ProductID { get; set; }
        public ProductModel Product { get; set; }

        public ICollection<ImageModel> Images { get; set; }
        public ICollection<OrderModel> Orders { get; set; }

        public string TwoLetterISOLanguageName
        {
            get { return Currency == null ? null : Currency.TwoLetterISOLanguageName; }
            set { Currency = CultureInfo.GetCultureInfo(value); }
        }
    }

    public class ImageModel
    {
        [Key] 
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public DateTime DateCreated { get; set; }

        public int ProductID { get; set; }
        public ProductModel Product { get; set; }
        public int VariantID { get; set; }
        public VariantModel Variant { get; set; }
    }

    #region enum

        public enum ProductStatus
        {
            Available,
            Unavailable
        }

    #endregion
}