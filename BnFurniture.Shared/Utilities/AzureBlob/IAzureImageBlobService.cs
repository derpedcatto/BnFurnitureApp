using Microsoft.AspNetCore.Http;

namespace BnFurniture.Shared.Utilities.AzureBlob;

public interface IAzureImageBlobService
{
    public Task<string> GetBlobURLAsync(string blobFullName, CancellationToken cancellationToken);
    public Task<IEnumerable<string>> GetBlobsAsync(
        bool includeURLs,
        string pathPrefix,
        CancellationToken cancellationToken = default);
    public Task<IEnumerable<string>> GetBlobsAsync(IEnumerable<string> blobNames, CancellationToken cancellationToken);
    public Task UploadFileBlobAsync(IFormFile file, string pathPrefix, CancellationToken cancellationToken);
    public Task UploadFilesBlobAsync(IEnumerable<IFormFile> files, string pathPrefix, CancellationToken cancellationToken);
    public Task DeleteBlobAsync(string blobName, CancellationToken cancellationToken);
    public Task SwapBlobNamesAsync(int blobIndex1, int blobIndex2, string pathPrefix, CancellationToken cancellationToken);
}

// public Task<BlobInfo> GetBlobAsync(string name, CancellationToken cancellationToken);