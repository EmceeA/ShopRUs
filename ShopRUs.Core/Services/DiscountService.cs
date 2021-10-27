using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopRUs.Core.DTO;
using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Core.Services
{
   public class DiscountService
    {
        private readonly ShopRUsContext _context;
        private IConfiguration _config;
        private readonly IHttpContextAccessor _http;
        public DiscountService(ShopRUsContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }


        public async Task<AddDiscountResponseDto> AddDiscount(AddDiscountRequestDto discountModel)
        {
            try
            {

                var getDiscount = await _context.Discounts.Where(d => d.DiscountName == discountModel.DiscountName).AnyAsync();
                if (getDiscount)
                {
                    return new AddDiscountResponseDto
                    {
                        Status = "Discount Already Exists"
                    };
                }

                else
                {
                    var createdDate = DateTime.Now;
                    var newDiscount = new Discount()
                    {
                        DiscountName = discountModel.DiscountName,
                        DiscountType = discountModel.DiscountType
                    };
                    await _context.Discounts.AddAsync(newDiscount);
                    await _context.SaveChangesAsync();
                }

                return new AddDiscountResponseDto
                {
                    DiscountName = discountModel.DiscountName,
                    Status = "Success"
                };



            }
            catch (Exception ex)
            {
                return new AddDiscountResponseDto
                {
                    Status = ex.Message
                };
            }
        }



        public IEnumerable<Discount> GetAll()
        {
            return _context.Users;
        }

        public Discount GetById(int id)
        {
            return _context.Users.Find(id);
        }
    }
}
