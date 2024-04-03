namespace BnFurniture.Domain.Entities
{
    public class UserRole_Permission
    {
        public Guid Id { get; set; }
        public Guid Role_Id { get; set; }
        public Guid Permission_Id { get; set; }

        // Navigation property for the related Permission
        public Permission Permission { get; set; } = null!;

        // Navigation property for the related UserRole
        public UserRole UserRole { get; set; } =null!;
    }
}
