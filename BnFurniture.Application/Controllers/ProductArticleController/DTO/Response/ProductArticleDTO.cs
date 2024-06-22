namespace BnFurniture.Application.Controllers.ProductArticleController.DTO.Response;

public class ProductArticleDTO
{
    public Guid Article { get; set; }
    public Guid ProductId { get; set; }
    public Guid AuthorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public decimal Price { get; set; }
    public int Discount { get; set; }
    public decimal FinalPrice
    {
        get
        {
            // Calculate discounted amount
            decimal discountAmount = Price * Discount / 100;

            // Calculate final price
            decimal finalPrice = Price - discountAmount;

            // Round to 2 decimal places
            return Math.Round(finalPrice, 2, MidpointRounding.AwayFromZero);
        }
    }
    public bool Active { get; set; }

    public string ThumbnailImageUri { get; set; } = string.Empty;
    public List<string> GalleryImages { get; set; } = [];

    public string ProductTypeName { get; set; } = string.Empty;
}
