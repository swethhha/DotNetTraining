using ShopTrackPro.Core.DTO;

namespace ShopTrackPro.Core.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDTO> CreateOrderAsync(OrderRequestDTO dto);
        Task<IEnumerable<OrderResponseDTO>> GetAllOrdersAsync();
        Task<OrderResponseDTO?> GetOrderByIdAsync(int id);
        Task UpdateOrderAsync(int id, OrderRequestDTO dto);
        Task DeleteOrderAsync(int id);

        // ✅ Add this method
        Task<IEnumerable<OrderResponseDTO>> GetOrdersByUserIdAsync(int userId);
    }
}
