using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopRUs.Core.DTO
{
    public class AddItemRequestDto
    {
       public string ItemName { get; set; }
        public ItemType ItemType { get; set; }
    }
}
