using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopRUs.Core.DTO
{
   public class CustomerSignUpResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public CustomerType CustomerType { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
