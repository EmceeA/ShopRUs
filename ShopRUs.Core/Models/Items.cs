using System;
using System.Collections.Generic;
using System.Text;

namespace ShopRUs.Core.Models
{
  public class Items
    {
        public int id { get; set; }
        public string ItemName { get; set; }
        public ItemType ItemType { get; set; }
    }
}
