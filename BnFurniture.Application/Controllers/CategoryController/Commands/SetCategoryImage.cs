using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CategoryController.DTO.Request;
using BnFurniture.Application.Extensions;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Enums;
using BnFurniture.Domain.Responses;
using System.Net;

namespace BnFurniture.Application.Controllers.CategoryController.Commands;

public sealed record SetCategoryImageCommand(SetCategoryImageDTO Dto);

public sealed class SetCategoryImageHandler : CommandHandler<SetCategoryImageCommand>
{
    private readonly SetCategoryImageDTOValidator _validator;
    private readonly IAppImageService _appImageService;

    public SetCategoryImageHandler(
        SetCategoryImageDTOValidator validator,
        IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
        _appImageService = appImageService;
    }

    public override async Task<ApiCommandResponse> Handle(
        SetCategoryImageCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse
                (false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Валідація не пройшла перевірку",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        var result = await _appImageService.AddImageAsync(
            AppEntityType.ProductCategory,
            request.Dto.Id,
            AppEntityImageType.PromoCardThumbnail,
            request.Dto.Image,
            cancellationToken);

        return new ApiCommandResponse
            (result.IsSuccess, result.StatusCode)
        {
            Message = result.Message
        };
    }
}
