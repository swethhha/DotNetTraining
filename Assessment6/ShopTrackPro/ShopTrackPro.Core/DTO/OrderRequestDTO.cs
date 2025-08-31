namespace ShopTrackPro.Core.DTO;

public class OrderRequestDTO
{
    public int UserId { get; set; }
    public List<OrderItemRequestDTO> Items { get; set; } = new();
}
