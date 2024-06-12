using BnFurniture.Application.Controllers.OrderStatusController.Commands;
using BnFurniture.Application.Controllers.OrderStatusController.DTO;
using BnFurniture.Application.Controllers.OrderStatusController.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrderStatus([FromServices] CreateOrderStatusHandler handler, [FromBody] CreateOrderStatusDTO dto)
        {
            var command = new CreateOrderStatusCommand(dto);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderStatus([FromServices] DeleteOrderStatusHandler handler, int id )
        {
            var command = new DeleteOrderStatusCommand(id);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderStatus([FromServices] UpdateOrderStatusHandler handler, [FromBody] UpdateOrderStatusDTO dto)
        {
            var command = new UpdateOrderStatusCommand(dto);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderStatus([FromServices] GetAllOrderStatusHandler handler)
        {
            var query = new GetAllOrderStatusQuery();
            var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }
    }
}
