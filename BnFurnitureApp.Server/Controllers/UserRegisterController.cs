using Mediator;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using BnFurniture.Application.Controllers.UserRegisterController.DTO;
using BnFurniture.Application.Controllers.ExampleController.Commands;
using BnFurniture.Application.Controllers.UserRegisterController.Commands;

namespace BnFurnitureApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserRegisterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserRegisterController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpDTO? model)
        {

            var query = new SignUpCommand.Command(model);
            var response = await _mediator.Send(query);

            return Ok(response);
        }
        

           
           

       
    }
}
