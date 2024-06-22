using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductArticleController.DTO.Request;
using BnFurniture.Application.Extensions;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Enums;
using BnFurniture.Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductArticleController.Commands;

public sealed record SetProductArticleImagesCommand(SetProductArticleImagesDTO Dto);

public sealed class SetProductArticleImagesHandler : CommandHandler<SetProductArticleImagesCommand>
{
    private readonly IAppImageService _appImageService;
    private readonly SetProductArticleImagesDTOValidator _validator;

    public SetProductArticleImagesHandler(
        SetProductArticleImagesDTOValidator validator,
        IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
        _appImageService = appImageService;
    }

    public override async Task<ApiCommandResponse> Handle(
        SetProductArticleImagesCommand request, CancellationToken cancellationToken)
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

        var imageResult = await _appImageService.AddImagesAsync(
            AppEntityType.ProductArticle,
            request.Dto.Article,
            AppEntityImageType.Gallery,
            request.Dto.GalleryImages,
            cancellationToken);
        
        return new ApiCommandResponse
            (imageResult.IsSuccess, imageResult.StatusCode)
        {
            Message = imageResult.Message
        };
    }
}