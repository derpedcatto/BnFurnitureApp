namespace ASP_Work.Data.Entity
{
    public class Permission
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = null!;
        public String? Description { get; set; }

        // Navigation property for the related UserRole_Permission
        public ICollection<UserRole_Permission> UserRole_Permissions { get; set; } = null!;
    }
}
