using BnFurniture.Domain.Enums;

namespace BnFurniture.Infrastructure.Configuration;

public static class ImagePathConfig
{
    public static string BasePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

    public static string GetFolderPath(AppEntityType entityType, Guid itemId, AppEntityImageType imageType)
    {
        return Path.Combine(BasePath, entityType.ToString(), itemId.ToString(), imageType.ToString());
    }
}
