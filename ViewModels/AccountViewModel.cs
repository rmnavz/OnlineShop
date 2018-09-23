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
        
        public bool isRemembered { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Nickname { get; set; }
        [Required]
        [DataType (DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [DataType (DataType.Password)]
        public string Password { get; set; }
    }
}