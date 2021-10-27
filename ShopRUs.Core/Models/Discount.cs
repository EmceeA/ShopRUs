using System;
using System.Collections.Generic;
using System.Text;

namespace ShopRUs.Core.Models
{
   public class Discount
    {
        public int Id { get; set; }
        public ItemType DiscountType { get; set; }
        public string DiscountName { get; set; }
        public int DiscountPercent { get; set; }
        public decimal DiscountPercentCalculation { get; set; }
    }

    
}
