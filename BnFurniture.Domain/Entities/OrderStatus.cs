﻿namespace BnFurniture.Domain.Entities
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public String Name { get; set; } = null!;

        // Navigation property for the related Order
        public ICollection<Order>? Orders { get; set; }  
       

    }
}