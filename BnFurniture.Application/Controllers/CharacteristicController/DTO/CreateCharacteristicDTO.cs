using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.CharacteristicController.DTO
{
    public class CreateCharacteristicDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int? Priority { get; set; }
    }
}
