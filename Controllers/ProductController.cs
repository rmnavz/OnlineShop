using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Shop.Models;
using Online_Shop.ViewModels;
using Online_Shop.Code.FileUpload;

namespace Online_Shop.Controllers
{
    public class ProductController : ControllerBase
    {
        public ProductController(DatabaseContext DbContext) : base(DbContext)
        {
        }

        [Route("Product")]
        public IActionResult Index(int ProductID = 0)
        {
            if(ProductID == 0) return NotFound();

            var Product = Db.Products.Where(p => p.ID == ProductID).FirstOrDefault();

            if(Product == null) return NotFound();

            Product.ConvertProductPrice();
            
            return View(Product);
        }

        public IActionResult Create(int storeID)
        {
            var model = new CreateProductViewModel();
            model.StoreID = storeID;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if(!ModelState.IsValid){ return View(model); }

            using(var Transaction = Db.Database.BeginTransaction())
            {
                var product = new ProductModel(){
                    Name = model.Name,
                    Description = model.Description,
                    StoreID = model.StoreID,
                    Store = Db.Stores.Where(s => s.ID == model.StoreID).FirstOrDefault(),
                    DateCreated = DateTime.Now,
                    Status = ProductStatus.Available,
                    Images = new List<ImageModel>(),
                    Variants = new List<VariantModel>()
                };

                foreach (var formFile in model.Images)
                {
                    if (formFile.Length > 0)
                    {
                        var image = ImageUpload.ImageToByte(formFile);
                        image.Product = product;
                        product.Images.Add(image);
                    }
                }

                await Db.Images.AddRangeAsync(product.Images);

                foreach(var variant in model.Variants)
                {
                    var temp = new VariantModel(){
                        Name = variant.Name,
                        Description = variant.Description,
                        Stock = variant.Stock,
                        Price = variant.Price,
                        Currency = CultureInfo.CurrentCulture,
                        Images = new List<ImageModel>(),
                        DateCreated = DateTime.Now
                    };

                    foreach (var formFile in variant.Images)
                    {
                        if (formFile.Length > 0)
                        {
                            var image = ImageUpload.ImageToByte(formFile);
                            image.Variant = temp;
                            temp.Images.Add(image);
                        }
                    }

                    product.Variants.Add(temp);
                    await Db.Variants.AddAsync(temp);
                    await Db.Images.AddRangeAsync(temp.Images);
                }

                await Db.Products.AddAsync(product);
                await Db.SaveChangesAsync();

                Transaction.Commit();
            }

            return RedirectToAction("List");
        }

        [Route("Products")]
        public IActionResult List(int? storeID)
        {
            var Products = (storeID.HasValue) ? 
                Db.Products.Where(p => p.StoreID == storeID).ToList() : Db.Products.ToList();

            var productview = new ListProductViewModel()
            {
                Products = Products,
                StoreID = storeID
            };
            
            if(Products.Count() == 0) return View("Empty", productview);

            return View(productview);
        }

        public IActionResult Update(int ID)
        {
            var product = Db.Products.Where(p => p.ID == ID).FirstOrDefault();

            if(product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductModel model)
        {
            if(!ModelState.IsValid){ return View(model); }

            ProductModel product = Db.Set<ProductModel>().SingleOrDefault(p => p.ID == model.ID);

            if(product == null) return NotFound(); 

            product.Name = model.Name;
            product.Description = model.Description;
            product.DateUpdated = model.DateUpdated;
            product.Status = model.Status;
            product.Images = model.Images;
            product.Variants = model.Variants;

            await Db.SaveChangesAsync();

            return View(model);
        }

        public IActionResult Delete(int ID)
        {
            var product = Db.Set<ProductModel>().SingleOrDefault(p => p.ID == ID);

            if(product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductModel model)
        {
            var product = Db.Set<ProductModel>().SingleOrDefault(p => p.ID == model.ID);

            if(product == null) return NotFound();

            Db.Entry(product).State = EntityState.Deleted;
            await Db.SaveChangesAsync();

            return RedirectToAction("List");
        }


    }
}