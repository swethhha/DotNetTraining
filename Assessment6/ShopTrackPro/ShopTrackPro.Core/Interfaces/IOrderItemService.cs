using ShopTrackPro.Core.DTO;

namespace ShopTrackPro.Core.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderItemResponseDTO> AddOrderItemAsync(int orderId, OrderItemRequestDTO dto);
        Task<IEnumerable<OrderItemResponseDTO>> GetOrderItemsByOrderIdAsync(int orderId);
        Task DeleteOrderItemAsync(int id);
    }
}
