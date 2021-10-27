using System;
using System.Collections.Generic;
using System.Text;

namespace ShopRUs.Core.Models
{
  public class Item
    {
        public int id { get; set; }
        public string ItemName { get; set; }
        public ItemType ItemType { get; set; }

        public double ItemAmount { get; set; }
    }
}
