using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopTrackPro.Core.DTO;
using ShopTrackPro.Core.Interfaces;

namespace ShopTrackPro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "orderitems")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _service;
        public OrderItemController(IOrderItemService service) => _service = service;

        [HttpGet("{orderId}")]
        [Authorize(Roles = "Admin,Seller,User")]
        public async Task<ActionResult<IEnumerable<OrderItemResponseDTO>>> GetByOrderId(int orderId)
        {
            var items = await _service.GetOrderItemsByOrderIdAsync(orderId);
            return Ok(items);
        }

        [HttpPost("{orderId}")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<ActionResult<OrderItemResponseDTO>> AddOrderItem(int orderId, [FromBody] OrderItemRequestDTO dto)
        {
            var createdItem = await _service.AddOrderItemAsync(orderId, dto);
            return CreatedAtAction(nameof(GetByOrderId), new { orderId }, createdItem);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteOrderItemAsync(id);
            return Ok(new { message = "Order item deleted successfully." });
        }
    }
}
