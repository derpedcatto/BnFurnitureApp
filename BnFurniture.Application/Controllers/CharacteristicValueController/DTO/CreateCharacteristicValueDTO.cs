using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.CharacteristicValueController.DTO
{
    public class CreateCharacteristicValueDTO
    {
        public Guid CharacteristicId { get; set; }
        public string Value { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int? Priority { get; set; }
    }
}
