using System;

namespace BnFurniture.Application.Controllers.ProductCharacteristicController.DTO
{
    public class ResponseCharacteristicDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int? Priority { get; set; }
    }
}

