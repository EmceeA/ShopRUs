using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopRUs.Core.Models
{
  public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string ItemName { get; set; }
        public ItemType ItemType { get; set; }
        public int ItemTypeId { get; set; }
        public double ItemPrice { get; set; }
        public Discount Discount { get; set; }
        public int DiscountId { get; set; }
    }
}
