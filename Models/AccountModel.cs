using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Online_Shop.Models
{
    public class AccountModel
    {
        
        [Key] 
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public virtual AccountRoles Role { get; set; }
        public virtual AccountStatus Status { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual SellerModel Seller { get; set; }
        public virtual CustomerModel Customer { get; set; }

        public virtual ICollection<ReviewModel> Reviews { get; set; }
        public virtual ICollection<ViewCountModel> ViewCounts { get; set; }

        public string getName()
        {
            return (string.IsNullOrEmpty(this.Nickname)) ? this.FirstName + ' ' + this.LastName : this.Nickname ;
        }
    }

    public class SellerModel
    {
        [Key] 
        public int ID { get; set; }

        public int AccountID { get; set; }
        public virtual AccountModel Account { get; set; }

        public virtual ICollection<StoreSellerModel> StoreSeller { get; set; }
    }

    public class CustomerModel
    {
        [Key]
        public int ID { get; set; }

        public int AccountID { get; set; }
        public virtual AccountModel Account { get; set; }

        public virtual ICollection<CartModel> Carts { get; set; }
    }

    #region Enum

            public enum AccountRoles
            {
                SuperAdministrator,
                Administrator,
                Seller,
                Customer,
                Guest
            }

            public enum AccountStatus
            {
                Enabled,
                Disabled,
                Deleted
            }

        #endregion
}