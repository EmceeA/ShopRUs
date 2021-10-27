using System;
using System.Collections.Generic;
using System.Text;

namespace ShopRUs.Core.DTO
{
    public class CustomerItemResponseDto
    {
        public string Status { get; set; }

        public string Response { get; set; }
        public double Sum { get; set; }
    }
}
