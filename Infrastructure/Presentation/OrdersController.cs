using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.OrdersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController(IServiceManger serviceManger) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderRequestDto request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManger.OrderService.CreateOrderAsync(request, email);
            return Ok(result);
        }

        [HttpGet] // GET: /api/orders
        public async Task<IActionResult> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManger.OrderService.GetOrderByUserEmailAsync(email);
            return Ok(result);
        }

        [HttpGet("{id}")] // GET: /api/orders/{id}
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManger.OrderService.GetOrderByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("delivery-methods")] // ✅ Uniquely routed: /api/orders/delivery-methods
        public async Task<IActionResult> GetAllDeliveryMethods()
        {
            var result = await serviceManger.OrderService.GetAllDeliveryMethods();
            return Ok(result);
        }
    }

}
