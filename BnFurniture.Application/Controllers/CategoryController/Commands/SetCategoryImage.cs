using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CategoryController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Enums;
using BnFurniture.Domain.Responses;
using System.Net;

namespace BnFurniture.Application.Controllers.CategoryController.Commands;

public sealed record SetCategoryImageCommand(SetCategoryImageDTO Dto);

public sealed class SetCategoryImageHandler : CommandHandler<SetCategoryImageCommand>
{
    private readonly IAppImageService _appImageService;
    private readonly SetCategoryImageDTOValidator _validator;

    public SetCategoryImageHandler(IAppImageService appImageService, SetCategoryImageDTOValidator validator,
        IHandlerContext context) : base(context)
    {
        _appImageService = appImageService;
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(SetCategoryImageCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command.Dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Валідація не пройшла перевірку",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        var result = await _appImageService.AddImageAsync(
            AppEntityType.ProductCategory,
            command.Dto.Id,
            AppEntityImageType.PromoCardThumbnail,
            command.Dto.Image,
            cancellationToken);

        return new ApiCommandResponse(result.IsSuccess, result.StatusCode)
        {
            Message = result.Message
        };
    }
}
