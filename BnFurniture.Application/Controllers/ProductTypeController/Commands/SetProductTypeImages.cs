using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductTypeController.DTO.Request;
using BnFurniture.Application.Extensions;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Enums;
using BnFurniture.Domain.Responses;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductTypeController.Commands;

public sealed record SetProductTypeImagesCommand(SetProductTypeImageDTO Dto);

public sealed class SetProductTypeImagesHandler : CommandHandler<SetProductTypeImagesCommand>
{
    private readonly SetProductTypeImageDTOValidator _validator;
    private readonly IAppImageService _appImageService;

    public SetProductTypeImagesHandler(
        SetProductTypeImageDTOValidator validator,
        IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
        _appImageService = appImageService;
    }

    public override async Task<ApiCommandResponse> Handle(
            SetProductTypeImagesCommand request, CancellationToken cancellationToken)
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

        var cardImageResult = await _appImageService.AddImageAsync(
            AppEntityType.ProductType,
            request.Dto.Id,
            AppEntityImageType.PromoCardThumbnail,
            request.Dto.CardImage,
            cancellationToken);

        var thumbImageResult = await _appImageService.AddImageAsync(
            AppEntityType.ProductType,
            request.Dto.Id,
            AppEntityImageType.Thumbnail,
            request.Dto.ThumbnailImage, 
            cancellationToken);

        if (cardImageResult.IsSuccess && thumbImageResult.IsSuccess)
        {
            return new ApiCommandResponse
                (true, (int)HttpStatusCode.OK)
            {
                Message = "Product Type images set success"
            };
        }

        return new ApiCommandResponse
            (false, cardImageResult.StatusCode)
        {
            Message = $"Card - {cardImageResult.Message}; Thumbnail - {thumbImageResult.Message}"
        };
    }
}