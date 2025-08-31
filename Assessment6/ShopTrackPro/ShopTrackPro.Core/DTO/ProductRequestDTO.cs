namespace ShopTrackPro.Core.DTO;

public class ProductRequestDTO
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
