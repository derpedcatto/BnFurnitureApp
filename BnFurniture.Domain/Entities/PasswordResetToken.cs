using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class PasswordResetToken
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string TokenValue { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }

    // Nav

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}
