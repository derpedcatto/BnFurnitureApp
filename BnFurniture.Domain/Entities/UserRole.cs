namespace BnFurniture.Domain.Entities
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = null!;
        public String? Description { get; set; }

        // Navigation property for the related UserRole
        public ICollection<UserRole_Permission> UserRole_Permissions_UsRol { get; set; } = null!;

        // Navigation property for the related UserRole
        public ICollection<User_UserRole> User_UserRoles { get; set; } = null!;
    }
}
