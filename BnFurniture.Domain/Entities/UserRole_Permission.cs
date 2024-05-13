using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class UserRole_Permission
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserRoleId { get; set; }
    public Guid PermissionId { get; set; }

    // Nav
    [ForeignKey(nameof(UserRoleId))]
    public UserRole UserRole { get; set; } = null!;

    [ForeignKey(nameof(PermissionId))]
    public Permission Permission { get; set; } = null!;
}
