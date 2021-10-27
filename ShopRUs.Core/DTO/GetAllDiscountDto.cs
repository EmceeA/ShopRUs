using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopRUs.Core.DTO
{
   public class GetAllDiscountDto
    {
        public int Id { get; set; }
       // public ItemType DiscountType { get; set; }
        public string DiscountName { get; set; }
        public int DiscountPercent { get; set; }
    }
}
