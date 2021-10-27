using System;
using System.Collections.Generic;
using System.Text;

namespace ShopRUs.Core.DTO
{
    public class payForItemRequestDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public double TotalAmount { get; set; }
    }
}
