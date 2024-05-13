//using BnFurniture.Application.Abstractions;
//using BnFurniture.Application.Controllers.UserRegisterController.DTO;
//using BnFurniture.Application.Extensions;
//using BnFurniture.Domain.Responses;
//using BnFurniture.Shared.Utilities.Hash;
//using FluentValidation;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using System.Net;

//namespace BnFurniture.Application.Controllers.UserRegisterController.Commands;

//    public sealed record PassForgotCommand(UserPassForgotDTO entityForm);

//public sealed class PassForgotHandler : CommandHandler<PassForgotCommand>
//{
//    //private readonly PassForgotDTOValidator _validator;
//    /////private readonly IHashService _hashService;

//    //public PassForgotHandler(PassForgotDTOValidator validator,
//    //IHandlerContext context) : base(context)
//    //{
//    //    _validator = validator;
//    //}

//    public override async Task<ApiCommandResponse> Handle(PassForgotCommand request, CancellationToken cancellationToken = default)
//    {
//        var dto = request.entityForm;
//        var httpContext = HandlerContext.HttpContextAccessor.HttpContext;

//        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
//        if (!validationResult.IsValid)
//        {
//            return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
//            {
//                Message = "Валідація не пройшла перевірку",
//                Errors = validationResult.ToApiResponseErrors()
//            };
//        }
//        if (!string.IsNullOrEmpty(httpContext.Session.GetString("AuthUserId")))
//        {
//            return new ApiCommandResponse(true, (int)HttpStatusCode.Conflict)
//            {
//                Message = "Користувач вже авторизований."
//            };
//        }
//        var user = await HandlerContext.DbContext.User.FirstOrDefaultAsync(
//           u => u.Email == dto.Email, cancellationToken);

//        httpContext.Session.SetString("AuthUserId", user.Id.ToString());

//        return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
//        {
//            Message = "Користувач успішно авторизований."
//        };
//    }
//}

