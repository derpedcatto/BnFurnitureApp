using BnFurniture.Application.Controllers.OrderController;
using BnFurniture.Application.Controllers.ProductArticle_OrderItem.Commands;
using BnFurniture.Application.Controllers.ProductArticle_OrderItem.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductArticle_OrderItemController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public ProductArticle_OrderItemController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductArticle_OrderItem([FromServices] CreateProductArticle_OrderItemHandler handler, [FromBody] CreateProductArticle_OrderItemDTO dto)
        {
            _logger.LogInformation("CreateOrder called with DTO: {@dto}", dto);
            var command = new CreateProductArticle_OrderItemCommand(dto);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }
    }
}
