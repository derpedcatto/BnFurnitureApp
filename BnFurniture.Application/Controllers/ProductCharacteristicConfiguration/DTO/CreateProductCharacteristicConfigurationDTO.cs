using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductCharacteristicConfiguration.DTO
{
    public class CreateProductCharacteristicConfigurationDTO
    {
        public Guid ArticleId { get; set; }
        public Guid CharacteristicId { get; set; }
        public Guid CharacteristicValueId { get; set; }
    }
}
