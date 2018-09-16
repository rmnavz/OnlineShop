using System;

namespace Online_Shop.Models
{
    public class AccountModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public RoleModel Role { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }

        public string getName()
        {
            return (string.IsNullOrEmpty(this.Nickname)) ? this.FirstName + ' ' + this.LastName : this.Nickname ;
        }
    }
}