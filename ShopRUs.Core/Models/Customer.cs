using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopRUs.Core.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public CustomerType CustomerType { get; set; }
        public int CustomerTypeId { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
