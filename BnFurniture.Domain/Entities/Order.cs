namespace BnFurniture.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid User_Id { get; set; }
        public int Status_Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; } = null;

        // Navigation property for the related OrderItem
        public ICollection<OrderItem> OrderItems { get; set; } = null!;

        // Navigation property for the related OrderStatus
        public OrderStatus? Status { get; set; }=null!;

        // Navigation property for the related User
        public User? User { get; set; } = null!;

    }
}
