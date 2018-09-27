using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Online_Shop.Models
{
    public class CartModel
    {
        [Key]
        public Guid ID { get; set; }
        public CartStatus Status { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }

        public int CustomerID { get; set; }
        public virtual CustomerModel Customer { get; set; }

        public virtual ICollection<OrderModel> Orders { get; set; }
    }

    public class OrderModel
    {
        [Key]
        public Guid ID { get; set; }
        public int Quantity { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateCreated { get; set; }

        public int VariantID { get; set; }
        public virtual VariantModel Variant { get; set; }
        public Guid CartID { get; set; }
        public virtual CartModel Cart { get; set; }
    }

    #region enum
        
        public enum CartStatus
        {
            Purchased,
            Pending,
            Saved,
            Deleted
        }

        public enum OrderStatus
        {
            Pending,
            Accepted,
            Done
        }

    #endregion
}