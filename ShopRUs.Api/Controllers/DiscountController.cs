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
    public class DiscountController : Controller
    {
        private readonly IDiscount _discount;
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<DiscountController> _logger;
        private readonly ShopRUsContext _context;
        private readonly IMapper _mapper;

        public DiscountController(IDiscount discount, ILogger<DiscountController> logger, IHttpContextAccessor accessor, ShopRUsContext context, IMapper mapper)
        {
            _discount = discount;
            _logger = logger;
            _accessor = accessor;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddDiscount(AddDiscountRequestDto addDiscount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createDiscount = await _discount.AddDiscount(addDiscount);
            if (createDiscount.Status == "Success")
            {
                return Ok(createDiscount);
            }
            return BadRequest(createDiscount.Status);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var discounts = _discount.GetAll();
            var model = _mapper.Map<IList<Discount>>(discounts);
            return Ok(model);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllDiscount()
        {
            var discountList = await _discount.GetAllDiscount();
            return Ok(discountList);
        }

        [HttpGet]

        public async Task<IActionResult> GetDiscountbyId(int discountId)
        {
            var getDiscountById = await _discount.GetDiscountById(discountId);
            return Ok(getDiscountById);
        }

    }
}
