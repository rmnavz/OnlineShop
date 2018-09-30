using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Online_Shop.Code.RestSharp.CurrencyConverterApi;

namespace Online_Shop.Models
{
    public class ProductModel
    {
        [Key] 
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public ProductStatus Status { get; set; }

        public int StoreID { get; set; }
        public virtual StoreModel Store { get; set; }

        public virtual ICollection<ImageModel> Images { get; set; }
        public virtual ICollection<VariantModel> Variants { get; set; }
        public virtual ICollection<ReviewModel> Reviews { get; set; }
        public virtual ICollection<ViewCountModel> ViewCounts { get; set; }
        
        public virtual ICollection<ProductCategoryModel> ProductCategory { get; set; }

        public void ConvertProductPrice()
        {
            var converter = new FreeCurrencyConverterApi();
            
            foreach(var Variant in Variants)
            {
                ConvertResult result = converter.GetExchangeRate(Variant.Currency);
                Variant.Price = Variant.Price * result.val;
                Variant.Currency = CultureInfo.CurrentCulture;
            }

        }
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
        public virtual ProductModel Product { get; set; }

        public virtual ICollection<ImageModel> Images { get; set; }
        public virtual ICollection<OrderModel> Orders { get; set; }

        public string TwoLetterISOLanguageName
        {
            get { return Currency == null ? null : Currency.Name; }
            set { Currency = CultureInfo.GetCultureInfo(value); }
        }
    }

    public class CategoryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }
        public virtual ICollection<ProductCategoryModel> ProductCategory { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class ProductCategoryModel
    {
        public int ProductID { get; set; }
        public virtual ProductModel Product { get; set; }
        public int CategoryID { get; set; }
        public virtual CategoryModel Category { get; set; }
    }

    #region enum

        public enum ProductStatus
        {
            Available,
            Unavailable
        }

    #endregion
}