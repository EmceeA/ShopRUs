using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopRUs.Core;
using ShopRUs.Core.DTO;
using ShopRUs.Core.Interfaces;
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

        public CustomerController(ICustomer customer, ILogger<CustomerController> logger, IHttpContextAccessor accessor, ShopRUsContext context)
        {
            _customer = customer;
            _logger = logger;
            _accessor = accessor;
            _context = context;
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
    }
}
