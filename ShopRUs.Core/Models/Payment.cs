using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopRUs.Core.Models
{
    public class Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string itemName { get; set; }
        public ItemType ItemType { get; set; }
        public int ItemTypeId { get; set; }
        public double ItemAmount { get; set; }
    }
}
