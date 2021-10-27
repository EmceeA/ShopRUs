using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopRUs.Core.Models
{
   public class Discount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DiscountName { get; set; }
        public int DiscountPercent { get; set; }
    }

    
}
