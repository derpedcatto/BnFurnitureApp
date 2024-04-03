namespace BnFurniture.Domain.Entities
{
    public class User_UserRole
    {
        public Guid Id { get; set; }
        public Guid User_Id { get; set; }
        public Guid UserRole_Id { get; set; }

        // Navigation property for the related UserRole
        public UserRole UserRole { get; set; } = null!;

        // Navigation property for the related UserRole
        public User User_Us_UsRole { get; set; } = null!;
    }
}
