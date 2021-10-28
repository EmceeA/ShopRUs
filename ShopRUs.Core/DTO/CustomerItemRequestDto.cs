using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopRUs.Core.DTO
{
    public class CustomerItemRequestDto
    {
        public string UserName { get; set; }

        public ICollection<Item> Item { get; set; }

       // public int Quantity { get; set; }

        public Discount Discount { get; set; }

        public double TotalSum { get; set; }
        //public double UnitPrice { get; set; }
    }
}
