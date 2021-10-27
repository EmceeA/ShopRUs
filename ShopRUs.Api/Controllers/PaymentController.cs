using AutoMapper;
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
    public class PaymentController : Controller
    {
        private readonly IPayment _payment;
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<PaymentController> _logger;
        private readonly ShopRUsContext _context;
        private readonly IMapper _mapper;

        public PaymentController(IPayment payment, ILogger<PaymentController> logger, IHttpContextAccessor accessor, ShopRUsContext context, IMapper mapper)
        {
            _payment = payment;
            _logger = logger;
            _accessor = accessor;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(AddItemRequestDto addItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newItem = await _payment.AddItem(addItem);
            if (newItem.Status == "Success")
            {
                return Ok(newItem);
            }
            return BadRequest(newItem.Status);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllItem()
        {
            var itemList = await _payment.GetAllItem();
            return Ok(itemList);
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoice(CustomerInvoiceRequest addInvoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newInvoice = await _payment.AddInvoice(addInvoice);
            if (newInvoice.Status == "Success")
            {
                return Ok(newInvoice);
            }
            return BadRequest(newInvoice.Status);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemType(AddItemTypeRequestDto additemType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createItemType = await _payment.AddItemType(additemType);
            if (createItemType.Status == "Success")
            {
                return Ok(createItemType);
            }
            return BadRequest(createItemType.Status);
        }
    }
}
