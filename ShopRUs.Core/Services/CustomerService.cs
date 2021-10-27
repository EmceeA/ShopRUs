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
        public CustomerService(ShopRUsContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }

      

        public async Task<CustomerSignUpResponseDto> CustomerSignUp(CustomerSignUpRequestDto signUpModel)
        {
            try
            {
               
                var getCustomer = await _context.Customers.Where(d => d.UserName == signUpModel.Username).AnyAsync();
                if (getCustomer)
                {
                    return new CustomerSignUpResponseDto
                    {
                        Status = "Customer Already Enrolled"
                    };
                }
               

                if(signUpModel.IsEmployee == false)
                {
                    var newCustomer = new Customer()
                    {
                        CustomerType = CustomerType.NonEmployee,
                        UserName = signUpModel.Username,
                        FirstName = signUpModel.FirstName,
                        LastName = signUpModel.LastName,
                        Password = EncodePassword(signUpModel.Password),
                        CreatedAt = DateTime.Now
                    };
                    await _context.Customers.AddAsync(newCustomer);
                    await _context.SaveChangesAsync();
                    return new CustomerSignUpResponseDto
                    {
                        UserName = signUpModel.Username,
                        FirstName = signUpModel.FirstName,
                        LastName = signUpModel.LastName,
                        Status = "Success Customer Account Created"
                    };

                }
                else 
                {
                    var newCustomer = new Customer()
                    {
                        CustomerType = CustomerType.Employee,
                        UserName = signUpModel.Username,
                        FirstName = signUpModel.FirstName,
                        LastName = signUpModel.LastName,
                        Password = EncodePassword(signUpModel.Password),
                        CreatedAt = DateTime.Now
                    };
                    await _context.Customers.AddAsync(newCustomer);
                    await _context.SaveChangesAsync();
                    return new CustomerSignUpResponseDto
                    {
                        FirstName = signUpModel.FirstName,
                        LastName = signUpModel.LastName,
                        Status = "Success Employee Account Created"
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


    }
}
