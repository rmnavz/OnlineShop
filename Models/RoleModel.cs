using System.Collections.Generic;

namespace Online_Shop.Models
{
    public class RoleModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<AccountModel> Accounts { get; set; }
    }
}