using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductController.DTO
{
    public class ProductsFilteredSearchRequestDTO
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("priceRange")]
        public string PriceRange { get; set; } = string.Empty;

        [JsonPropertyName("characteristics")]
        public Dictionary<string, string[]> Characteristics { get; set; } = new Dictionary<string, string[]>();

        [JsonPropertyName("page")]
        public int Page { get; set; } = 1;

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; } = 10;
    }

}
