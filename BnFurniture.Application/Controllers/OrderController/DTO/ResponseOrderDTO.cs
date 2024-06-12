using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.OrderController.DTO
{
    public class ResponseOrderDTO
    {
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("statusId")]
        public int StatusId { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("modifiedAt")]
        public DateTime? ModifiedAt { get; set; } = DateTime.UtcNow;
    }
}
