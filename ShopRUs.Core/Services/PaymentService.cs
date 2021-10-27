using ShopRUs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopRUs.Core.Services
{
    public class PaymentService : IPayment
    {

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
    }
}
