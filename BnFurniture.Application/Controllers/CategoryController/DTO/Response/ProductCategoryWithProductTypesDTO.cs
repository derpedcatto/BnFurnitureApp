using BnFurniture.Application.Controllers.ProductTypeController.DTO.Response;

namespace BnFurniture.Application.Controllers.CategoryController.DTO.Response;

public class ProductCategoryWithProductTypesDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int? Priority { get; set; }

    public string CardImageUri { get; set; } = string.Empty;

    public List<ProductCategoryWithProductTypesDTO>? SubCategories { get; set; }
    public List<ProductTypeDTO>? ProductTypes { get; set; }
}
