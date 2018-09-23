using System.ComponentModel.DataAnnotations;

namespace Online_Shop.ViewModels
{
    public class CreateStoreViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}