using System.ComponentModel.DataAnnotations;

namespace BnFurniture.Domain.Entities;

public class Permission
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Nav
    public ICollection<UserRole_Permission> UserRole_Permissions { get; set; } = null!;
}
