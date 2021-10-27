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
                        ItemPrice = itemModel.ItemAmount


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
                ItemTypeId = x.ItemTypeId,
                ItemAmount = x.ItemPrice
            }).ToList();
            return getAllItem;
        }


        public async Task<CustomerItemResponseDto> AddInvoice(CustomerInvoiceRequest itemModel)
        {
            var invoiceDetail = new Models.InvoiceDetail();
            double itemTotalSum = 0;
            var invoice = new Invoice()
            {
               // Id = itemModel.Id,
                //InvoiceDetails = itemModel.InvoiceDetails,


            };

            foreach (var item in itemModel.InvoiceDetails)
            {
                invoiceDetail.ItemTotalSum = item.ItemPrice * item.ItemQuantity * item.Item.Discount.DiscountPercent;
                invoiceDetail.ItemQuantity = item.ItemQuantity;
                invoiceDetail.ItemPrice = item.ItemPrice;
                invoiceDetail.InvoiceId = item.InvoiceId;
                invoiceDetail.Discount = item.Discount;
                itemTotalSum += invoiceDetail.ItemTotalSum;

                await _context.InvoiceDetails.AddAsync(invoiceDetail);
            }

            invoice.TotalAmount = itemTotalSum;
            await _context.Invoices.AddAsync(invoice);

            return new CustomerItemResponseDto
            {
                Status = "Success"
            };
        }


        public async Task<AddCustomerTyperesponseDto> AddItemType(AddItemTypeRequestDto itemType)
        {
            try
            {

                var getItemType = await _context.ItemTypes.Where(d => d.ItemTypeName == itemType.ItemTypeName).AnyAsync();
                if (getItemType)
                {
                    return new AddCustomerTyperesponseDto
                    {
                        Status = "Item type Already Exists"
                    };
                }

                else
                {
                    var createdDate = DateTime.Now;
                    var newItemType = new ItemType()
                    {
                        ItemTypeName = itemType.ItemTypeName,
                        //DiscountType = discountModel.DiscountType

                    };
                    await _context.ItemTypes.AddAsync(newItemType);
                    await _context.SaveChangesAsync();
                }

                return new AddCustomerTyperesponseDto
                {
                    CustomerTypeName = itemType.ItemTypeName,
                    Status = "Success"
                };



            }
            catch (Exception ex)
            {
                return new AddCustomerTyperesponseDto
                {
                    Status = ex.Message
                };
            }
        }



    }
}
