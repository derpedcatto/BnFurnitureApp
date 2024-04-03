namespace ASP_Work.Data.Entity
{
    public class UserActivityType
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = null!;
        public String? Description { get; set; }

        // Navigation property for the related AuditLog
        public ICollection<AuditLog> AuditLogs_UsAcT { get; set; } = null!;
    }
}
