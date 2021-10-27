using ShopRUs.Core.DTO;
using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Core.Interfaces
{
   public interface IDiscount
    {
        Task<AddDiscountResponseDto> AddDiscount(AddDiscountRequestDto discountModel);
        IEnumerable<Discount> GetAll();
        Discount GetById(int id);
        Discount GetByName(string type);
        Task<List<GetAllDiscountDto>> GetAllDiscount();
    }
}
