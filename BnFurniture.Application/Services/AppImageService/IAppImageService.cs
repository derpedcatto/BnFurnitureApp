using BnFurniture.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace BnFurniture.Application.Services.AppImageService;

public interface IAppImageService
{
    Task<IEnumerable<string>> GetProductCategoryImages(Guid categoryId, AppImageType imageType, CancellationToken cancellationToken);
    Task<string> AddProductCategoryImageAsync(IFormFile file, AppImageType imageType, CancellationToken cancellationToken);
    Task<bool> DeleteProductCategoryImageAsync(Guid categoryId, AppImageType imageType, string filename, CancellationToken cancellationToken);


}
