using BnFurniture.Application.Controllers.OrderController;
using BnFurniture.Application.Controllers.OrderItem.Commands;
using BnFurniture.Application.Controllers.OrderItem.DTO;
using BnFurniture.Application.Controllers.OrderItem.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        public OrderItemController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems([FromServices] GetAllOrderItemsHandler handler)
        {
            var query = new GetAllOrderItemsQuery();
            var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem([FromServices] CreateOrderItemHandler handler, [FromBody] CreateOrderItemDTO dto)
        {
            _logger.LogInformation("CreateOrderItem called with DTO: {@dto}", dto);
            var command = new CreateOrderItemCommand(dto);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem([FromServices] DeleteOrderItemHandler handler, Guid id)
        {
            //_logger.LogInformation("DeleteOrderItem called with ID: {id}", id);
            var command = new DeleteOrderItemCommand(id);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }
    }
}
