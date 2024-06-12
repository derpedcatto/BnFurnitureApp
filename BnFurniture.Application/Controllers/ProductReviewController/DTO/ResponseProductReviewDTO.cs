using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductReviewController.DTO
{
    public   class ResponseProductReviewDTO
    {
       
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }      
        public Guid AuthorId { get; set; }      
        public int Rating { get; set; }      
        public string Text { get; set; } = string.Empty;      
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
