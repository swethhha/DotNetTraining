using ShopTrackPro.Core.DTO;

namespace ShopTrackPro.Core.DTO;

public class OrderResponseDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemResponseDTO> Items { get; set; } = new();
}
