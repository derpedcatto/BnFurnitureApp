namespace BnFurniture.Application.Controllers.ProductTypeController.DTO.Response;

public class ProductTypeDTO
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int? Priority { get; set; }

    public string CardImageUri { get; set; } = string.Empty;
}