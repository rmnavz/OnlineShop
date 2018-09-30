using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Online_Shop.Models;

namespace Online_Shop.Code.FileUpload
{
    public class ImageUpload
    {
        public static ImageModel ImageToByte(IFormFile image)
        {
            var result = new ImageModel();
            var uploads = Path.Combine(Globals.environment.WebRootPath, "images");
            using (MemoryStream ms = new MemoryStream())
            {
                image.OpenReadStream().CopyTo(ms);
                var fileName = Guid.NewGuid().ToString().Replace("-", "") + DateTime.Now.ToString("MM_DD_yyyy_HH_mm_ss") + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(uploads, fileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                    result = new ImageModel() { 
                        Name = fileName,
                        ContentType = image.ContentType,
                        Image = ms.ToArray(),
                        FileLocation = filePath,
                        DateCreated = DateTime.Now
                    };
                }
            }

            return result;
        }
    }
}