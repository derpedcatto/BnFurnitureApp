namespace BnFurniture.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid Order_Id { get; set; }
        public Guid Article_Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }

        //Navigation property for the related Order
        public Order Order { get; set; } = null!;


    }
}
