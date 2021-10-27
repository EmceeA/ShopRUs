using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopRUs.Core.DTO;
using ShopRUs.Core.Interfaces;
using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Core.Services
{
    public class PaymentService : IPayment
    {

        private readonly ShopRUsContext _context;
        private IConfiguration _config;
        private readonly IHttpContextAccessor _http;
        public PaymentService(ShopRUsContext context, IConfiguration config, IHttpContextAccessor http)
        {
            _context = context;
            _config = config;
            _http = http;
        }

        public async Task<AddItemResponseDto> AddItem(AddItemRequestDto itemModel)
        {
            try
            {

                var getDiscount = await _context.Items.Where(d => d.ItemName == itemModel.ItemName).AnyAsync();
                if (getDiscount)
                {
                    return new AddItemResponseDto
                    {
                        Status = "Item Already Exists"
                    };
                }

                else
                {
                    var createdDate = DateTime.Now;
                    var newItem = new Item()
                    {
                        ItemName = itemModel.ItemName,
                        ItemType = itemModel.ItemType,
                        ItemAmount = itemModel.ItemAmount
                       

                    };
                    await _context.Items.AddAsync(newItem);
                    await _context.SaveChangesAsync();
                }

                return new AddItemResponseDto
                {
                   ItemName = itemModel.ItemName,
                    Status = "Success"
                };



            }
            catch (Exception ex)
            {
                return new AddItemResponseDto
                {
                    Status = ex.Message
                };
            }
        }


        public async Task<List<GetAllItemDto>> GetAllItem()
        {
            var getAllItem = _context.Items.Select(x => new GetAllItemDto
            {
               
                Id = x.id,
                ItemName = x.ItemName,
                ItemType = x.ItemType,
                ItemAmount = x.ItemAmount
            }).ToList();
            return getAllItem;
        }
    }
}
