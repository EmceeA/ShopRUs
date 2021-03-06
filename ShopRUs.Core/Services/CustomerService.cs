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
    
    public class CustomerService : ICustomer
    {
        private readonly ShopRUsContext _context;
        private IConfiguration _config;
        private readonly IHttpContextAccessor _http;
        public CustomerService(ShopRUsContext context, IConfiguration config, IHttpContextAccessor http)
        {
            _context = context;
            _config = config;
            _http = http;
        }

      

        public async Task<CustomerSignUpResponseDto> CustomerSignUp(CustomerSignUpRequestDto signUpModel)
        {
            try
            {
               
                var getCustomer = await _context.Customers.Where(d => d.UserName == signUpModel.UserName).AnyAsync();
                if (getCustomer)
                {
                    return new CustomerSignUpResponseDto
                    {
                        Status = "Customer Already Enrolled"
                    };
                }
                else
                {
                    var newCustomer = new Customer()
                    {
                        CustomerTypeId = signUpModel.CustomerTypeId,
                        UserName = signUpModel.UserName,
                        FirstName = signUpModel.FirstName,
                        LastName = signUpModel.LastName,
                        Password = EncodePassword(signUpModel.Password),
                        EntryDate = DateTime.Now
                    };
                    await _context.Customers.AddAsync(newCustomer);
                    await _context.SaveChangesAsync();
                    return new CustomerSignUpResponseDto
                    {
                        UserName = signUpModel.UserName,
                        FirstName = signUpModel.FirstName,
                        LastName = signUpModel.LastName,
                        Status = "Success Customer Account Created"
                    };

                }
               
                

              /*  //09064615283
                return new CustomerSignUpResponseDto
                {
                    Status = "Failed"
                };*/
            }
            catch (Exception ex)
            {
                return new CustomerSignUpResponseDto
                {
                    Status = ex.Message
                };
            }
        }


        public async Task<CustomerSignInResponseDto> CustomerSignIn(CustomerSignInRequestDto signInModel)
        {
            var getUserDetail = _context.Customers.Where(s => s.UserName == signInModel.UserName).FirstOrDefault();

            if (getUserDetail == null)
            {
                return new CustomerSignInResponseDto
                {
                    Response = "User Not Found",
                    Status = "Valid User is Required"
                };
            }
            string password = DecodePin(getUserDetail.Password);

            getUserDetail.LastLoginDate = DateTime.Now;
           await  _context.SaveChangesAsync();
            return new CustomerSignInResponseDto
            {
                FirstName = getUserDetail.FirstName,
                LastName = getUserDetail.LastName,
                Response = "Successfully Logged In"
            };
        }


        public string EncodePassword(string password)
        {
            byte[] requestPassword = Encoding.ASCII.GetBytes(password);
            var encoded = Convert.ToBase64String(requestPassword);
            return encoded;
        }

        public string DecodePin(string requestPassword)
        {
            var decode = Convert.FromBase64String(requestPassword);
            var decoded = Encoding.ASCII.GetString(decode);
            return decoded;
        }


        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers;
        }

        public Customer GetById(int id)
        {
            return _context.Customers.Find(id);
        }

        public Customer GetByName(string name)
        {
            return _context.Customers.Find(name);
        }

        public async Task<List<GetAllCustomerDto>> GetAllCustomer()
        {
            var getAllCustomer = _context.Customers.Select(x => new GetAllCustomerDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName
            }).ToList();
            return getAllCustomer;
        }

        public async Task<List<GetAllCustomerDto>> GetCustomerById(int customerId)
        {
            var getCustomerbyId = await _context.Customers.Where(c => c.Id == customerId)
                .Select(w => new GetAllCustomerDto
                {
                    Id = w.Id,
                   FirstName = w.FirstName,
                   LastName = w.LastName,
                   UserName =w.UserName
                }).ToListAsync();
            return getCustomerbyId;
        }

        public async Task<AddCustomerTyperesponseDto> AddCustomerType(AddCustomerTypeRequestDto customerType)
        {
            try
            {

                var getCustomerType = await _context.CustomerTypes.Where(d => d.CustomerTypeName == customerType.CustomerTypeName).AnyAsync();
                if (getCustomerType)
                {
                    return new AddCustomerTyperesponseDto
                    {
                        Status = "Customer type Already Exists"
                    };
                }

                else
                {
                    var createdDate = DateTime.Now;
                    var newCustomerType = new CustomerType()
                    {
                        CustomerTypeName = customerType.CustomerTypeName,
                        DiscountId = customerType.DiscountId
                        //DiscountType = discountModel.DiscountType

                    };
                    await _context.CustomerTypes.AddAsync(newCustomerType);
                    await _context.SaveChangesAsync();
                }

                return new AddCustomerTyperesponseDto
                {
                    CustomerTypeName = customerType.CustomerTypeName,
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
