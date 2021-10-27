using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopRUs.Core.Models
{
   public class InvoiceDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
        public double ItemPrice { get; set; }
        public int ItemQuantity { get; set; }
        public double ItemTotalSum { get; set; }
        public double Discount { get; set; }
        public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }
    }
}
