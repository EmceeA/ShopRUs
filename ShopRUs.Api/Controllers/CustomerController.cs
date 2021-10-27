using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopRUs.Core;
using ShopRUs.Core.DTO;
using ShopRUs.Core.Interfaces;
using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopRUs.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomer _customer;
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<CustomerController> _logger;
        private readonly ShopRUsContext _context;
        private readonly IMapper _mapper;

        public CustomerController(ICustomer customer, ILogger<CustomerController> logger, IHttpContextAccessor accessor, ShopRUsContext context, IMapper mapper)
        {
            _customer = customer;
            _logger = logger;
            _accessor = accessor;
            _context = context;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> CustomerSignUp(CustomerSignUpRequestDto signUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var register = await _customer.CustomerSignUp(signUp);
            if (register.Status == "Success")
            {
                return Ok(register);
            }
            return BadRequest(register.Status);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerSignIn(CustomerSignInRequestDto SignIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var register = await _customer.CustomerSignIn(SignIn);
          
            _logger.LogInformation($"User [{SignIn.UserName}] logged in the system.");
            if (register.Response == "Successfully Logged In")
            {
                return Ok(new CustomerSignInResponseDto
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Response = register.Response,
                    Status = register.Status

                }); ;
            }
            return BadRequest(register);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _customer.GetAll();
            var model = _mapper.Map<IList<Customer>>(customers);
            return Ok(model);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllCustomers()
        {
            var customerList = await _customer.GetAllCustomer();
            return Ok(customerList);
        }

        [HttpGet]

        public async Task<IActionResult> GetCustomerbyId(int customerId)
        {
            var getCustomerById = await _customer.GetCustomerById(customerId);
            return Ok(getCustomerById);
        }
    }

}
