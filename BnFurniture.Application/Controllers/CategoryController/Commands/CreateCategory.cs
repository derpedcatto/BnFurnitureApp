using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CategoryController.DTO.Request;
using BnFurniture.Application.Extensions;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Enums;
using BnFurniture.Domain.Responses;
using System.Net;

namespace BnFurniture.Application.Controllers.CategoryController.Commands;

public sealed record CreateCategoryCommand(CreateCategoryDTO Dto);

public sealed class CreateCategoryHandler : CommandHandler<CreateCategoryCommand>
{
    private readonly CreateCategoryDTOValidator _validator;
    private readonly IAppImageService _appImageService;

    public CreateCategoryHandler(
        CreateCategoryDTOValidator validator,
        IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
        _appImageService = appImageService;
    }

    public override async Task<ApiCommandResponse> Handle(
        CreateCategoryCommand request,
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

        var newCategory = new ProductCategory()
        {
            Id = Guid.NewGuid(),
            ParentId = request.Dto.ParentId,
            Name = request.Dto.Name,
            Slug = request.Dto.Slug,
            Priority = request.Dto.Priority,
        };

        await HandlerContext.DbContext.AddAsync(newCategory, cancellationToken);
        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        if (request.Dto.PromoCardThumbnailImage != null)
        {
            await _appImageService.AddImageAsync(
                AppEntityType.ProductCategory,
                newCategory.Id,
                AppEntityImageType.PromoCardThumbnail,
                request.Dto.PromoCardThumbnailImage,
                cancellationToken);
        }

        return new ApiCommandResponse
            (true, (int)HttpStatusCode.Created)
        {
            Message = "Category created."
        };
    }
}