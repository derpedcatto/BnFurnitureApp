using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductTypeController.DTO.Request;
using BnFurniture.Application.Extensions;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Enums;
using BnFurniture.Domain.Responses;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductTypeController.Commands;

public sealed record CreateProductTypeCommand(CreateProductTypeDTO Dto);

public sealed class CreateProductTypeHandler : CommandHandler<CreateProductTypeCommand>
{
    private readonly CreateProductTypeDTOValidator _validator;
    private readonly IAppImageService _appImageService;

    public CreateProductTypeHandler(
        CreateProductTypeDTOValidator validator,
        IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
        _appImageService = appImageService;
    }

    public override async Task<ApiCommandResponse> Handle(
        CreateProductTypeCommand request, CancellationToken cancellationToken)
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

        var newProductType = new ProductType()
        {
            Id = Guid.NewGuid(),
            CategoryId = request.Dto.CategoryId,
            Name = request.Dto.Name,
            Slug = request.Dto.Slug,
            Priority = request.Dto.Priority,
        };
        
        await HandlerContext.DbContext.ProductType.AddAsync(newProductType, cancellationToken);
        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        var cardImageResult = await _appImageService.AddImageAsync(
            AppEntityType.ProductType,
            newProductType.Id,
            AppEntityImageType.PromoCardThumbnail,
            request.Dto.CardImage,
            cancellationToken);

        var thumbImageResult = await _appImageService.AddImageAsync(
            AppEntityType.ProductType,
            newProductType.Id,
            AppEntityImageType.Thumbnail,
            request.Dto.ThumbnailImage,
            cancellationToken);

        return new ApiCommandResponse
            (true, (int)HttpStatusCode.Created)
        {
            Message = "Product Type created."
        };
    }
}