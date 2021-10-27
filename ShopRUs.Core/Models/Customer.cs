using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopRUs.Core.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public CustomerType CustomerType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
