using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class AuditLog
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid UserActivityTypeId { get; set; }
    public DateTime Timestamp { get; set; }
    public string? Description { get; set; }

    // Nav
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!; 

    [ForeignKey(nameof(UserActivityTypeId))]
    public UserActivityType UserActivityType { get; set; } = null!;
}