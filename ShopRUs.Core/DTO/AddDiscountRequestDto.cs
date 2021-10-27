using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopRUs.Core.DTO
{
   public class AddDiscountRequestDto
    {
        public string DiscountName { get; set; }
        public ItemType DiscountType { get; set; }

        public int DiscountPercent { get; set; }
    }
}
