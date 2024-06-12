using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.UserWishlistItemController.DTO
{
    public class ResponseUserWishlistItemDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("productArticleId")]
        public Guid ProductArticleId { get; set; }

        [JsonPropertyName("userWishlistId")]
        public Guid UserWishlistId { get; set; }

        [JsonPropertyName("addedAt")]
        public DateTime AddedAt { get; set; }
    }
}
