using BnFurniture.Application.Controllers.ProductCharacteristicController.DTO;
using System;
using System.Collections.Generic;

namespace BnFurniture.Application.Controllers.ProductController.DTO
{
    public class ResponseCharacteristicWithValuesDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int? Priority { get; set; }
        public List<CharacteristicValueDTO> Values { get; set; } = new List<CharacteristicValueDTO>();
    }
}
