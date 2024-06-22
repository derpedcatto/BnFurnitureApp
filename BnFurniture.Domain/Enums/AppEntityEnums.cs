namespace BnFurniture.Domain.Enums;

public enum AppEntityType
{
    ProductCategory,
    ProductType,
    Product,
    ProductArticle,
}

public enum AppEntityImageType
{
    Thumbnail,
    PromoCardThumbnail,
    Gallery,
}

/*
https://{AzureApi}/{AppEntityType}/{Entity Id}/{AppEntityImageType}/image.jpg
*/