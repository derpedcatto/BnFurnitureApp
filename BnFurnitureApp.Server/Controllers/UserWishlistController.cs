using BnFurniture.Application.Controllers.UserWishlistController.Command;
using BnFurniture.Application.Controllers.UserWishlistController.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserWishlistController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUserWishlist([FromServices] CreateUserWishlistHandler handler, [FromBody] CreateUserWishlistDTO dto)
        {
            var command = new CreateUserWishlistCommand(dto);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }
    }
}
