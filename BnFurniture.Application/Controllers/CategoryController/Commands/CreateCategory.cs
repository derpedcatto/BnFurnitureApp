using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CategoryController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Enums;
using BnFurniture.Domain.Responses;
using System.Net;

namespace BnFurniture.Application.Controllers.CategoryController.Commands;

public sealed record CreateCategoryCommand(CreateCategoryDTO dto);

public sealed class CreateCategoryHandler : CommandHandler<CreateCategoryCommand>
{
    private readonly CreateCategoryDTOValidator _validator;
    private readonly IAppImageService _appImageService;

    public CreateCategoryHandler(CreateCategoryDTOValidator validator, IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
        _appImageService = appImageService;
    }

    public override async Task<ApiCommandResponse> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var dto = command.dto;

        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Валідація не пройшла перевірку",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        var newCategory = new ProductCategory()
        {
            Id = Guid.NewGuid(),
            ParentId = dto.ParentId,
            Name = dto.Name,
            Slug = dto.Slug,
            Priority = dto.Priority,
        };

        await HandlerContext.DbContext.AddAsync(newCategory, cancellationToken);
        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        if (dto.PromoCardThumbnailImage != null)
        {
            await _appImageService.AddImageAsync(
                AppEntityType.ProductCategory,
                newCategory.Id,
                AppEntityImageType.PromoCardThumbnail,
                dto.PromoCardThumbnailImage,
                cancellationToken);
        }

        return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
        {
            Message = "Category created."
        };
    }
}