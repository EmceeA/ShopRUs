using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopRUs.Core.DTO
{
   public class InvoiceDetail
    {
       // public int Id { get; set; }
        public int ItemId { get; set; }
        public double ItemPrice { get; set; }
        public int ItemQuantity { get; set; }
        public double ItemTotalSum { get; set; }
        public double Discount { get; set; }
       // public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }
       // public Item Item { get; set; }
    }
}
