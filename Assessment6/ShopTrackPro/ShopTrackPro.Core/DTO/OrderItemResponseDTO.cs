﻿namespace ShopTrackPro.Core.DTO;

public class OrderItemResponseDTO
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
