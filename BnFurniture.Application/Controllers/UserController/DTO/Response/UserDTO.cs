namespace BnFurniture.Application.Controllers.UserController.DTO.Response;

public class UserDTO
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public DateTime RegisteredAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
}
