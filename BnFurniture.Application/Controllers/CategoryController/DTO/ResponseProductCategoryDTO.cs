namespace BnFurniture.Application.Controllers.CategoryController.DTO;

public class ResponseProductCategoryDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int? Priority { get; set; }

    public string CardImageUri { get; set; } = string.Empty;

    public List<ResponseProductCategoryDTO>? SubCategories { get; set; }
}
