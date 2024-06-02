using System;

namespace BnFurniture.Application.Controllers.ProductCharacteristicController.DTO
{
    public class CharacteristicValueDTO
    {
        public Guid Id { get; set; }
        public Guid CharacteristicId { get; set; }
        public string Value { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int? Priority { get; set; }
    }
}
