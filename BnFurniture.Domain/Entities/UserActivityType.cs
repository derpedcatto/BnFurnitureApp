using System.ComponentModel.DataAnnotations;

namespace BnFurniture.Domain.Entities;

public class UserActivityType
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Nav
    public ICollection<AuditLog> AuditLogs { get; set; } = null!;
}
