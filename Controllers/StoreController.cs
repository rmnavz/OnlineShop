using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Shop.Models;
using Online_Shop.ViewModels;
using Online_Shop.Code.FileUpload;

namespace Online_Shop.Controllers
{
    public class StoreController : ControllerBase
    {
        public StoreController(DatabaseContext DbContext) : base(DbContext)
        {
        }

        [Route("Store")]
        public IActionResult Index(int StoreID = 0)
        {
            if(StoreID == 0) return NotFound();

            var Store = Db.Stores.Where(s => s.ID == StoreID).FirstOrDefault();

            if(Store == null) return NotFound();
            
            return View(Store);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateStoreViewModel model)
        {
            if(!ModelState.IsValid){ return View(model); }

            var store = new StoreModel(){
                Name = model.Name,
                Description = model.Description,
                DateCreated = DateTime.Now,
                Images = new List<ImageModel>()
            };

            foreach (var formFile in model.Images)
            {
                if (formFile.Length > 0)
                {
                    var image = ImageUpload.ImageToByte(formFile);
                    image.Store = store;
                    store.Images.Add(image);
                }
            }

            await Db.Images.AddRangeAsync(store.Images);
            await Db.Stores.AddAsync(store);
            await Db.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [Route("Stores")]
        public IActionResult List()
        {
            var stores = Db.Stores.ToList();

            if(stores.Count() == 0) return View("Empty");

            return View(stores);
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