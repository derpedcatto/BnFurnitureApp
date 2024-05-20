namespace BnFurniture.Application.Controllers.CategoryController.DTO;

public class ProductCategoryDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int? Priority { get; set; }

    public List<ProductCategoryDTO>? SubCategories { get; set; }
}
