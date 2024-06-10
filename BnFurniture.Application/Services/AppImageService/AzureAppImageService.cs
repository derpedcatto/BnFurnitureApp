using BnFurniture.Domain.Enums;
using BnFurniture.Domain.Responses;
using BnFurniture.Shared.Utilities.AzureBlob;
using Microsoft.AspNetCore.Http;

namespace BnFurniture.Application.Services.AppImageService;

public class AzureAppImageService : IAppImageService
{
    private readonly IAzureImageBlobService _azureBlobService;

    public AzureAppImageService(IAzureImageBlobService azureBlobService)
    {
        _azureBlobService = azureBlobService;
    }

    public async Task<StatusResponse<IEnumerable<string>>> GetImagesAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        CancellationToken cancellationToken)
    {
        var prefixPath = GetImageFolderPath(entityType, entityId, imageType);

        var uriList = await _azureBlobService.GetBlobsAsync(
            includeURLs: true,
            pathPrefix: prefixPath,
            cancellationToken: cancellationToken);

        return new StatusResponse<IEnumerable<string>>(true, 200, "Images fetch success", uriList);
    }

    public async Task<StatusResponse> AddImageAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        var prefixPath = GetImageFolderPath(entityType, entityId, imageType);

        await _azureBlobService.UploadFileBlobAsync(
            file: file,
            pathPrefix: prefixPath,
            cancellationToken: cancellationToken);

        return new StatusResponse(true, 201, "Image added successfully");
    }

    public async Task<StatusResponse> AddImagesAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        IEnumerable<IFormFile> files,
        CancellationToken cancellationToken)
    {
        var prefixPath = GetImageFolderPath(entityType, entityId, imageType);

        await _azureBlobService.UploadFilesBlobAsync(
            files: files,
            pathPrefix: prefixPath,
            cancellationToken: cancellationToken);

        return new StatusResponse(true, 201, "Images added successfully");
    }

    public async Task<StatusResponse> DeleteImageAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        int imageIndex,
        CancellationToken cancellationToken)
    {
        var prefixPath = GetImageFolderPath(entityType, entityId, imageType);

        await _azureBlobService.DeleteBlobAsync(prefixPath, cancellationToken);

        return new StatusResponse(true, 200, "Image deleted successfully");
    }

    public async Task<StatusResponse> SwapImageOrderAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        int firstImageIndex,
        int secondImageIndex,
        CancellationToken cancellationToken)
    {
        var prefixPath = GetImageFolderPath(entityType, entityId, imageType);

        await _azureBlobService.SwapBlobNamesAsync(firstImageIndex, secondImageIndex, prefixPath, cancellationToken);

        return new StatusResponse(true, 200, "Image order swapped successfully");
    }

    private static string GetImageFolderPath(AppEntityType entityType, Guid entityId, AppEntityImageType imageType)
    {
        return $"{entityType.ToString()}/{entityId.ToString()}/{imageType.ToString()}";
    }
}



/*
public async Task<StatusResponse<string>> GetImageAsync(
    AppEntityType entityType,
    Guid entityId,
    AppEntityImageType imageType,
    CancellationToken cancellationToken)
{
    var blobPath = GetImageFolderPath(entityType, entityId, imageType);
    var imageUri = await _azureBlobService.GetBlobURLAsync(blobPath, cancellationToken);

    return new StatusResponse<string>(true, 200, "Image fetch success", imageUri);
}
*/

/*
    public StatusResponse<string> GetImageAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        CancellationToken cancellationToken)
    {
        var directoryPath = ImagePathConfig.GetFolderPath(entityType, entityId, imageType);
        if (!Directory.Exists(directoryPath))
        {
            return new StatusResponse<string>(false, 404, "Directory does not exist");
        }

        var file = Directory.GetFiles(directoryPath).FirstOrDefault();
        if (file == null)
        {
            return new StatusResponse<string>(false, 404, "Directory is empty");
        }

        var url = new Uri(new Uri(_baseUri), file);
        return new StatusResponse<string>(true, 200, "Image fetch success", url.ToString());
    }

    public StatusResponse<string[]> GetImagesAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        CancellationToken cancellationToken)
    {
        var directoryPath = ImagePathConfig.GetFolderPath(entityType, entityId, imageType);
        if (!Directory.Exists(directoryPath))
        {
            return new StatusResponse<string[]>(false, 404, "Directory does not exist");
        }

        var files = Directory.GetFiles(directoryPath)
            .OrderBy(a => a)
            .ToArray();

        return new StatusResponse<string[]>(true, 200, "Image fetch success", files);
    }

    public async Task<StatusResponse> AddImageAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        var directoryPath = ImagePathConfig.GetFolderPath(entityType, entityId, imageType);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var nextFileNumber = GetFileCount(directoryPath) + 1;

        var newFileName = nextFileNumber + Path.GetExtension(file.FileName);
        var fullPath = Path.Combine(directoryPath, newFileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        return new StatusResponse(true, 201, "Image added successfully");
    }

    public async Task<StatusResponse> AddImagesAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        IEnumerable<IFormFile> files,
        CancellationToken cancellationToken)
    {
        var directoryPath = ImagePathConfig.GetFolderPath(entityType, entityId, imageType);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var nextFileNumber = GetFileCount(directoryPath) + 1;

        foreach (var file in files)
        {
            var newFileName = nextFileNumber + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(directoryPath, newFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            nextFileNumber++;
        }

        return new StatusResponse(true, 201, "Images added successfully");
    }

    public async Task<StatusResponse> DeleteImageAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        int imageIndex,
        CancellationToken cancellationToken)
    {
        var directoryPath = ImagePathConfig.GetFolderPath(entityType, entityId, imageType);

        var files = Directory.GetFiles(directoryPath).OrderBy(f => f).ToArray();
        if (imageIndex < 1 || imageIndex > files.Length)
        {
            return new StatusResponse(false, 400, "Invalid image index");
        }

        var fileToDelete = files[imageIndex - 1];
        if ( ! File.Exists(fileToDelete))
        {
            return new StatusResponse(false, 404, "File not found");
        }

        await Task.Run(() => File.Delete(fileToDelete), cancellationToken);

        // Renaming file after deleted file to stay consistent
        // image 3 out of 6 is deleted, so images 4, 5, 6 are renamed to 3, 4, 5
        for (int i = imageIndex; i < files.Length; i++)
        {
            var oldFile = files[i];
            var newFileName = (i) + Path.GetExtension(oldFile);
            var newFilePath = Path.Combine(directoryPath, newFileName);
            await Task.Run(() => File.Move(oldFile, newFilePath), cancellationToken);
        }

        return new StatusResponse(true, 200, "Image deleted successfully");
    }

    public async Task<StatusResponse> UpdateImageAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        int imageIndex,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        var directoryPath = ImagePathConfig.GetFolderPath(entityType, entityId, imageType);

        var files = Directory.GetFiles(directoryPath).OrderBy(f => f).ToArray();
        if (imageIndex < 1 || imageIndex > files.Length)
        {
            return new StatusResponse(false, 400, "Invalid image index");
        }

        var fileToUpdate = files[imageIndex - 1];
        if ( ! File.Exists(fileToUpdate))
        {
            return new StatusResponse(false, 404, "File not found");
        }

        File.Delete(fileToUpdate);

        var newFileName = imageIndex + Path.GetExtension(file.FileName);
        var fullPath = Path.Combine(directoryPath, newFileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        return new StatusResponse(true, 200, "Image updated successfully");
    }

    public async Task<StatusResponse> SwapImageOrderAsync(
        AppEntityType entityType,
        Guid entityId,
        AppEntityImageType imageType,
        int firstImageIndex,
        int secondImageIndex,
        CancellationToken cancellationToken)
    {
        var directoryPath = ImagePathConfig.GetFolderPath(entityType, entityId, imageType);
        if (!Directory.Exists(directoryPath))
        {
            return new StatusResponse(false, 404, "Directory not found");
        }

        var files = Directory.GetFiles(directoryPath).OrderBy(f => f).ToArray();
        if (firstImageIndex < 1 || firstImageIndex > files.Length ||
            secondImageIndex < 1 || secondImageIndex > files.Length)
        {
            return new StatusResponse(false, 400, "Invalid image index");
        }

        var firstFile = files[firstImageIndex - 1];
        var secondFile = files[secondImageIndex - 1];

        var firstFileNameNew = Path.Combine(directoryPath, $"{secondImageIndex}{Path.GetExtension(firstFile)}");
        var secondFileNameNew = Path.Combine(directoryPath, $"{firstImageIndex}{Path.GetExtension(secondFile)}");

        await Task.Run(() => File.Move(firstFile, firstFileNameNew), cancellationToken);
        await Task.Run(() => File.Move(secondFile, secondFileNameNew), cancellationToken);

        return new StatusResponse(true, 200, "Image order swapped successfully");
    }

    private static int GetFileCount(string directoryPath)
    {
        return Directory.GetFiles(directoryPath).Length;
    }
*/