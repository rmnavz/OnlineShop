using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Online_Shop.Models;

namespace Online_Shop.ViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int StoreID { get; set; }
        public List<CreateVariantViewModel> Variants { get; set; }
        public List<IFormFile> Images { get; set; }
    }

    public class CreateVariantViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public List<IFormFile> Images { get; set; }
    }

    public class ListProductViewModel
    {
        public ICollection<ProductModel> Products { get; set; }
        public int? StoreID { get; set; }
    }

    public class ProductViewModel
    {
        public ProductModel Product { get; set; }
        public int VariantID { get; set; }
        public int Quantity { get; set; }
    }
}