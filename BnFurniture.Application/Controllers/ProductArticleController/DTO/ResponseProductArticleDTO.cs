namespace BnFurniture.Application.Controllers.ProductArticleController.DTO;

public class ResponseProductArticleDTO
{
    public Guid Article { get; set; }
    public Guid ProductId { get; set; }
    public Guid AuthorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public decimal Price { get; set; }
    public int Discount { get; set; }
    public bool Active { get; set; }
}
