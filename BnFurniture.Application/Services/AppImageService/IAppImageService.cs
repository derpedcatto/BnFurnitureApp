using BnFurniture.Domain.Enums;
using BnFurniture.Domain.Responses;
using Microsoft.AspNetCore.Http;

namespace BnFurniture.Application.Services.AppImageService;

public interface IAppImageService
{
    Task<StatusResponse<IEnumerable<string>>> GetImagesAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        CancellationToken cancellationToken);

    Task<StatusResponse> AddImageAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        IFormFile file,
        CancellationToken cancellationToken);

    Task<StatusResponse> AddImagesAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        IEnumerable<IFormFile> files,
        CancellationToken cancellationToken);

    Task<StatusResponse> DeleteImageAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        int imageIndex,
        CancellationToken cancellationToken);



    Task<StatusResponse> SwapImageOrderAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        int firstImageIndex,
        int secondImageIndex,
        CancellationToken cancellationToken);
}

/*
    Task<StatusResponse<string>> GetImageAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        CancellationToken cancellationToken);

    Task<StatusResponse> UpdateImageAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        int imageIndex,
        IFormFile file,
        CancellationToken cancellationToken);
*/