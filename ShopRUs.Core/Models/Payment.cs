using System;
using System.Collections.Generic;
using System.Text;

namespace ShopRUs.Core.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string itemName { get; set; }
        public ItemType ItemType { get; set; }
        public double ItemAmount { get; set; }
    }
}
