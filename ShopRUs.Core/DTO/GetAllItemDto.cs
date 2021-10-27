using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopRUs.Core.DTO
{
    public class GetAllItemDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public ItemType ItemType { get; set; }

        public double ItemAmount { get; set; }
    }
}
