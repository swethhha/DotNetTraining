namespace ShopTrackPro.Core.DTO;

public class UserRequestDTO
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!; // plain password (will be hashed in service)
}
