using ShopRUs.Core.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopRUs.Core.Models
{
    public class Invoice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemType ItemType { get; set; }
        public int ItemTypeId { get; set; }
        public double TotalAmount { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
