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


        public async Task<CustomerItemResponseDto> CustomerItem(CustomerItemRequestDto itemModel)
        {
            try
            {

                var getUser = await _context.Customers.Where(d => d.UserName == itemModel.UserName).AnyAsync();
                if (getUser == false)
                {
                    return new CustomerItemResponseDto
                    {
                        Status = "Customer does not exist"
                    };
                }

                else
                {
                    var createdDate = DateTime.Now;
                    var newItem = new CustomerItemRequestDto()
                    {
                        Item1 = itemModel.Item1,
                        Item2 = itemModel.Item2,
                        Item3 = itemModel.Item3,
                        Item4 = itemModel.Item4,
                        Item5 = itemModel.Item5,

                    };
                    /*await _context.Items.AddAsync(newItem);
                    await _context.SaveChangesAsync();*/
                }

                /*return new CustomerItemResponseDto
                {
                    Sum = newItem.
                    ItemName = itemModel.ItemName,
                    Status = "Success"
                };*/



            }
            catch (Exception ex)
            {
               /* return new AddItemResponseDto
                {
                    Status = ex.Message
                };*/
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



        public async Task<CustomerItemResponseDto> PayVendor(payForItemRequestDto payForItem)
        {
            //var user = await _context.Customers.Where(e => e.UserName == payForItem.UserName).FirstOrDefaultAsync();

            var user = await _context.Customers.Where(e => e.UserName == payForItem.UserName).AnyAsync();

            if (!user)
            {
                return new CustomerItemResponseDto
                {
                   Response = "User does not exit"
                    
                };
            }

            if(user)
            {
                var addItemList = await CreateCustomerAccount(new DedicatedAccountRequest
                {
                    Email = signUpModel.Email,
                    FirstName = signUpModel.FirstName,
                    LastName = signUpModel.LastName
                });

            }

            if (user.Balance < payForItem.Amount)
            {
                return new AddCardResponse
                {
                    ResponseCode = ResponseCodes.Response88Code,
                    ResponseMessage = ResponseCodes.Response88Message,
                };
            }


            var destinationWallet = await _context.WalletInfos.Where(e => e.WalletId == payForItem.DestinationWallet).FirstOrDefaultAsync();


            if (destinationWallet == null)
            {
                return new AddCardResponse
                {
                    ResponseCode = ResponseCodes.Response33Code,
                    ResponseMessage = ResponseCodes.Response33Message.Replace("@object", "Destination Wallet"),
                };
            }

            //
            //Call PayStack API here to do debit and credit

            user.Balance -= payForItem.Amount;
            destinationWallet.Balance += payForItem.Amount;
            var customer = new CustomerInflow
            {
                Amount = payForItem.Amount,
                Channel = "REZQ",
                CurrentBalance = user.Balance,
                CustomerId = user.CustomerId,
                CustomerName = user.CustomerName,
                DateCreated = DateTime.Now,
                Narration = "REZQ" + "-" + $"{Guid.NewGuid().ToString().Replace("-", "").ToUpper()}-{user.CustomerId}",
                PhoneNumber = user.PhoneNumber,
                TransactionType = TransactionTypes.CREDIT,
                TransactionTypeDescription = TransactionTypes.OUTFLOW,
                WalletId = user.WalletId
            };


            var vendor = new CustomerInflow
            {
                Amount = payForItem.Amount,
                Channel = "REZQ",
                CurrentBalance = destinationWallet.Balance,
                CustomerId = destinationWallet.CustomerId,
                CustomerName = destinationWallet.CustomerName,
                DateCreated = DateTime.Now,
                Narration = "REZQ" + "-" + $"{Guid.NewGuid().ToString().Replace("-", "").ToUpper()}-{user.CustomerId}",
                PhoneNumber = destinationWallet.PhoneNumber,
                TransactionType = TransactionTypes.DEBIT,
                TransactionTypeDescription = TransactionTypes.INFLOW,
                WalletId = destinationWallet.WalletId
            };

            List<CustomerInflow> customers = new List<CustomerInflow>();
            customers.Add(customer);
            customers.Add(vendor);

            await _context.CustomerInflows.AddRangeAsync(customers);
            await _context.SaveChangesAsync();

            return new AddCardResponse
            {
                ResponseCode = ResponseCodes.Response00Code,
                ResponseMessage = ResponseCodes.Response00Message
            };

        }
    }
}
