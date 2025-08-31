using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopTrackPro.Core.DTO;
using ShopTrackPro.Core.Interfaces;
using System.Security.Claims;

namespace ShopTrackPro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service) => _service = service;

        // Admin + Seller can see all orders, User sees only own
        [HttpGet]
        [Authorize(Roles = "Admin,Seller,User")]
        public async Task<ActionResult<IEnumerable<OrderResponseDTO>>> GetAll()
        {
            if (User.IsInRole("User"))
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var userOrders = await _service.GetOrdersByUserIdAsync(userId);
                return Ok(userOrders);
            }

            // Admin & Seller
            return Ok(await _service.GetAllOrdersAsync());
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        [Authorize(Roles = "Admin,Seller,User")]
        public async Task<ActionResult<OrderResponseDTO>> GetById(int id)
        {
            var order = await _service.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            if (User.IsInRole("User"))
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (order.UserId != userId) return Forbid();
            }

            return Ok(order);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Seller,User")]
        public async Task<ActionResult<OrderResponseDTO>> Create([FromBody] OrderRequestDTO dto)
        {
            if (User.IsInRole("User"))
                dto.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var order = await _service.CreateOrderAsync(dto);
            return CreatedAtRoute("GetOrderById", new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<ActionResult> Update(int id, [FromBody] OrderRequestDTO dto)
        {
            await _service.UpdateOrderAsync(id, dto);
            return Ok("Order updated successfully.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteOrderAsync(id);
            return Ok("Order deleted successfully.");
        }
    }
}
