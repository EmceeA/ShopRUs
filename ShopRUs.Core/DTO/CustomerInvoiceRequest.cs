using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopRUs.Core.DTO
{
    public class CustomerInvoiceRequest
    {
       //public int Id { get; set; }
        //public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public ICollection<Models.InvoiceDetail> InvoiceDetails { get; set; }

    }
}
