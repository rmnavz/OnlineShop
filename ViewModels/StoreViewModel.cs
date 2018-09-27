using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Online_Shop.ViewModels
{
    public class CreateStoreViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}