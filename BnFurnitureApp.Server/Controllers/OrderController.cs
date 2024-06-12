using BnFurniture.Application.Controllers.Order.DTO;
using BnFurniture.Application.Controllers.OrderController.Commands;
using BnFurniture.Application.Controllers.OrderController.Queries;
using BnFurniture.Application.Controllers.OrderItem.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BnFurniture.Application.Controllers.OrderController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromServices] CreateOrderHandler handler, [FromBody] CreateOrderDTO dto)
        {
            _logger.LogInformation("CreateOrder called with DTO: {@dto}", dto);
            var command = new CreateOrderCommand(dto);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromServices] GetAllOrdersHandler handler)
        {
            _logger.LogInformation("GetAllOrders called.");
            var query = new GetAllOrdersQuery();
            var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromServices] UpdateOrderHandler handler, [FromBody] UpdateOrderDTO dto)
        {
            _logger.LogInformation("UpdateOrder called with DTO: {@dto}", dto);
            var command = new UpdateOrderCommand(dto);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }
    }
}