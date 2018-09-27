using System;
using Online_Shop.Models;

namespace Online_Shop.Models
{
    public class ReviewModel
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public int Ratings { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual AccountModel Account { get; set; }
        public virtual StoreModel Store { get; set; }
        public virtual ProductModel Product { get; set; }
    }

    public class ViewCountModel
    {
        public int ID { get; set; }
        public string IP { get; set; }
        public string Location { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual AccountModel Account { get; set; }
        public virtual StoreModel Store { get; set; }
        public virtual ProductModel Product { get; set; }
    }
}