using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopRUs.Core.Models
{
    public class CustomerType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CustomerTypeName { get; set; }
        public Discount Discount { get; set; }
        public int DiscountId { get; set; }
    }
}
