using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductController.DTO
{
    public class ResponseProductQueryDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("characteristics")]
        public List<ResponseCharacteristicWithValuesDTO> Characteristics { get; set; } = new List<ResponseCharacteristicWithValuesDTO>();
    }

}
