using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopRUs.Core.DTO
{
    public class AddCustomerTypeRequestDto
    {
       // public int Id { get; set; }
        public string CustomerTypeName { get; set; }
       // public Discount Discount { get; set; }
        public int DiscountId { get; set; }
    }
}
