using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartcomStore.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using SmartcomStore.Services.Interfaces;
using SmartcomStore.Models.RequetModels.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SmartcomStore.Models.RequestModels.Product;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Routing;
using SmartcomStore.Models;
using SmartcomStore.ResourceParameters;
using static SmartcomStore.Data.Models.Order;
using SmartcomStore.Models.ResponseModels;

namespace SmartcomStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateOrderDto order)
        {
            var result = await _orderService.CreateOrderAsync(order);

            if (!result.Status)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }


        [HttpGet]
        public async Task<IActionResult> GetById([Required] Guid id)
        {
            var result = await _orderService.GetById(id);

            if (!result.Status)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Data);

        }

        [HttpGet("customer")]
        public async Task<IActionResult> GetOrdersByCustomer([Required] Guid id, string status)
        {
            var result = new BaseResponseModel<IEnumerable<OrderDto>>();

            OrderStatus orderStatus;
            if (Enum.TryParse(status, true, out orderStatus))
            {
                result = await _orderService.GetOrderByCustomer(id, orderStatus);
            }
            else
            {
                result = await _orderService.GetOrderByCustomer(id);
            }


            if (!result.Status)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Data);
        }

        [HttpPost("updatestatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] ConfirmOrderDto confirmOrder)
        {
            var result = await _orderService.UpdateOrderStatus(confirmOrder.Id, confirmOrder.ShipmentDate);

            if (!result.Status)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Data);
        }


    }
}


