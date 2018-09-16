using System.ComponentModel.DataAnnotations;

namespace Online_Shop.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DataType (DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [DataType (DataType.Password)]
        public string Password { get; set; }
    }
}