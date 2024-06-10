using BnFurniture.Application.Controllers.ProductTypeController.DTO;

namespace BnFurniture.Application.Controllers.CategoryController.DTO;

public class ResponseProductCategoryWithProductTypesDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int? Priority { get; set; }

    public string CardImageUri { get; set; } = string.Empty;

    public List<ResponseProductCategoryWithProductTypesDTO>? SubCategories { get; set; }
    public List<ResponseProductTypeDTO>? ProductTypes { get; set; }
}
