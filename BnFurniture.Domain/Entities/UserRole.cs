using System.ComponentModel.DataAnnotations;

namespace BnFurniture.Domain.Entities;

public class UserRole
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Nav
    public ICollection<User_UserRole> User_UserRoles { get; set; } = null!;
    public ICollection<UserRole_Permission> UserRole_Permissions { get; set; } = null!;
}
