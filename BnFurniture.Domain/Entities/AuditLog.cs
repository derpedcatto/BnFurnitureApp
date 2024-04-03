namespace BnFurniture.Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public Guid User_Id { get; set; }
        public Guid Useractivitytype_Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Description { get; set; }

        // Navigation property for the related User
        public User User_AuLog { get; set; } = null!;

        // Navigation property for the UserActivityType User
        public UserActivityType UserActivityType_AuLog { get; set; } = null!;
    }
}
