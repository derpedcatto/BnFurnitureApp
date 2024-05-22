namespace BnFurniture.Application.Controllers.ProductController.DTO;

public class ResponseProductDTO
{
    public Guid Id { get; set; }
    public Guid ProductTypeId { get; set; }
    public Guid AuthorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? ProductDetails { get; set; }
    public int? Priority { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
