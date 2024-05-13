using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class User_UserRole
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid UserRoleId { get; set; }

    // Nav
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [ForeignKey(nameof(UserRoleId))]
    public UserRole UserRole { get; set; } = null!;
}
