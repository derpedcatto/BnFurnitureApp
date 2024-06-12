using BnFurniture.Application.Controllers.UserWishlistItemController.Queries;
using BnFurniture.Application.Controllers.UserWishlistItemController.Commands;
using BnFurniture.Application.Controllers.UserWishlistItemController.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWishlistItemController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllUserWishlistItems([FromServices] GetAllUserWishlistItemsHandler handler)
        {
            var query = new GetAllUserWishlistItemsQuery();
            var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserWishlistItem([FromServices] CreateUserWishlistItemHandler handler, [FromBody] CreateUserWishlistItemDTO dto)
        {
            var command = new CreateUserWishlistItemCommand(dto);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserWishlistItem([FromServices] DeleteUserWishlistItemHandler handler, Guid id)
        {
            var command = new DeleteUserWishlistItemCommand(id);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }
    }
}
