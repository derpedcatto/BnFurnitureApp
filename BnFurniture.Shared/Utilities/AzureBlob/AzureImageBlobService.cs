using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BnFurniture.Shared.Utilities.AzureBlob;

public class AzureImageBlobService : IAzureImageBlobService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _azureContainerName;
    private readonly string _azureAccessKey;
    private readonly string _azureStorageAccountName;
    private readonly string _azureBlobServiceUri;

    public AzureImageBlobService(IConfiguration configuration, BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
        _azureContainerName = configuration.GetValue<string>("AzureBlobStorage:ImageContainerName")!;
        _azureAccessKey = configuration.GetValue<string>("AzureBlobStorage:AccessKey")!;
        _azureStorageAccountName = configuration.GetValue<string>("AzureBlobStorage:StorageAccountName")!;
        _azureBlobServiceUri = configuration.GetValue<string>("AzureBlobStorage:BlobServiceUri")!;
    }

    public Task<string> GetBlobURLAsync(string blobFullName, CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_azureContainerName);
        var blobClient = containerClient.GetBlobClient(blobFullName);

        var sasTokenUrl = GenerateSasTokenForBlob(blobClient);

        return Task.FromResult(sasTokenUrl);
        // return Task.FromResult(blobClient.Uri.ToString());
    }

    public async Task<IEnumerable<string>> GetBlobsAsync(
        bool includeURLs,
        string pathPrefix,
        CancellationToken cancellationToken = default)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_azureContainerName);
        var resultList = new List<string>();

        var blobItems = containerClient.GetBlobsAsync(prefix: pathPrefix, cancellationToken: cancellationToken);
        await foreach (var blobItem in blobItems)
        {
            if (includeURLs)
            {
                var blobClient = containerClient.GetBlobClient($"{blobItem.Name}");
                var sasTokenUrl = GenerateSasTokenForBlob(blobClient);
                resultList.Add(sasTokenUrl);
                // resultList.Add(containerClient.GetBlobClient(blobItem.Name).Uri.ToString());
            }
            else
            {
                resultList.Add(blobItem.Name);
            }
        }

        return resultList;
    }

    public Task<IEnumerable<string>> GetBlobsAsync(IEnumerable<string> blobNames, CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_azureContainerName);
        var resultList = new List<string>();

        foreach (var name in blobNames)
        {
            resultList.Add(containerClient.GetBlobClient(name).Uri.ToString());
        }

        return Task.FromResult<IEnumerable<string>>(resultList);
    }

    public async Task UploadFileBlobAsync(IFormFile file, string pathPrefix, CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_azureContainerName);

        var blobs = containerClient.GetBlobsByHierarchyAsync(prefix: pathPrefix, cancellationToken: cancellationToken);
        BlobHierarchyItem? lastBlob = null;

        await foreach (var blob in blobs)
        {
            lastBlob = blob;
        }
        var lastBlobValue = lastBlob == null
            ? 0
            : int.Parse(Path.GetFileNameWithoutExtension(lastBlob.Blob.Name));
        var newBlobName = (lastBlobValue + 1) + Path.GetExtension(file.FileName);

        var newFileName = $"{pathPrefix}/{newBlobName}";

        var blobClient = containerClient.GetBlobClient(newFileName);
        var blobHttpHeaders = new BlobHttpHeaders
        {
            ContentType = file.ContentType
        };

        using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, new BlobUploadOptions
        {
            HttpHeaders = blobHttpHeaders
        }, cancellationToken);
    }

    public async Task UploadFilesBlobAsync(IEnumerable<IFormFile> files, string pathPrefix, CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_azureContainerName);

        var blobs = containerClient.GetBlobsByHierarchyAsync(prefix: pathPrefix, cancellationToken: cancellationToken);
        BlobHierarchyItem? lastBlob = null;

        await foreach (var blob in blobs)
        {
            lastBlob = blob;
        }

        var lastBlobValue = lastBlob == null
            ? 0
            : int.Parse(Path.GetFileNameWithoutExtension(lastBlob.Blob.Name));

        foreach (var file in files)
        {
            var newBlobName = (++lastBlobValue) + Path.GetExtension(file.FileName);
            var newFileName = $"{pathPrefix}/{newBlobName}";
            
            var blobClient = containerClient.GetBlobClient(newFileName);
            var blobHttpHeaders = new BlobHttpHeaders
            {
                ContentType = file.ContentType
            };

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, new BlobUploadOptions
            {
                HttpHeaders = blobHttpHeaders
            }, cancellationToken);
        }
    }

    public async Task DeleteBlobAsync(string blobName, CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_azureContainerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    public async Task SwapBlobNamesAsync(int blobIndex1, int blobIndex2, string pathPrefix, CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_azureContainerName);
        var blobFullName1 = $"{pathPrefix}/{blobIndex1}";
        var blobFullName2 = $"{pathPrefix}/{blobIndex2}";
        var blobClient1 = containerClient.GetBlobClient(blobFullName1);
        var blobClient2 = containerClient.GetBlobClient(blobFullName2);

        // Temporary blob names for the swap
        var tempBlobName1 = $"temp-{blobIndex1}";
        var tempBlobFullName1 = $"{pathPrefix}/{tempBlobName1}";
        var tempBlobClient1 = containerClient.GetBlobClient(tempBlobFullName1);

        // Check if both blobs exist
        if ( ! await blobClient1.ExistsAsync(cancellationToken) || !await blobClient2.ExistsAsync(cancellationToken))
            throw new InvalidOperationException("One or both of the blobs do not exist.");

        // Copy first blob to a temporary blob
        await tempBlobClient1.StartCopyFromUriAsync(blobClient1.Uri, cancellationToken: cancellationToken);
        await blobClient1.DeleteIfExistsAsync(cancellationToken: cancellationToken);

        // Copy second blob to first blob's original name
        await blobClient1.StartCopyFromUriAsync(blobClient2.Uri, cancellationToken: cancellationToken);
        await blobClient2.DeleteIfExistsAsync(cancellationToken: cancellationToken);

        // Copy temporary blob to second blob's original name
        await blobClient2.StartCopyFromUriAsync(tempBlobClient1.Uri, cancellationToken: cancellationToken);
        await tempBlobClient1.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    private string GenerateSasTokenForBlob(BlobClient blobClient, int durationMinutes = 5)
    {
        var sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
            BlobName = blobClient.Name,
            Resource = "b",
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(durationMinutes)
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        var blobService = new BlobServiceClient(new Uri(_azureBlobServiceUri), new StorageSharedKeyCredential(_azureStorageAccountName, _azureAccessKey));
        return blobClient.GenerateSasUri(sasBuilder).ToString();
    }
}



/* Implementation for renaming all the blobs after deleted blob
 * to maintain hierarchy (if image 3 of 6 is deleted, images '4, 5, 6' are renamed to '3, 4, 5')
    public async Task DeleteBlobAsync(string blobName, CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);

        var blobs = containerClient.GetBlobsByHierarchyAsync(prefix: Path.GetDirectoryName(blobName), cancellationToken: cancellationToken);
        var blobsList = new List<BlobItem>();
        await foreach (var blob in blobs) 
        {
            blobsList.Add(blob.Blob);
        }

        int i = 1;
        foreach (var blob in blobsList)
        {
            string currentName = Path.GetFileNameWithoutExtension(blob.Name);
            string extension = Path.GetExtension(blob.Name);

            if (currentName != i.ToString())
            {
                // Rename the blob
                string newBlobName = $"{i}{extension}";
                var newBlobClient = containerClient.GetBlobClient(Path.Combine(Path.GetDirectoryName(blob.Name), newBlobName));

                // Copy the old blob to the new blob
                await newBlobClient.StartCopyFromUriAsync(blobClient.Uri, cancellationToken: cancellationToken);

                // Wait for the copy operation to complete
                var newBlobProperties = await newBlobClient.GetPropertiesAsync(cancellationToken: cancellationToken);
                while (newBlobProperties.Value.CopyStatus == CopyStatus.Pending)
                {
                    await Task.Delay(500, cancellationToken);
                    newBlobProperties = await newBlobClient.GetPropertiesAsync(cancellationToken: cancellationToken);
                }

                if (newBlobProperties.Value.CopyStatus == CopyStatus.Success)
                {
                    // Delete the old blob
                    await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
                }
            }
            i++;
        }
    }
*/

/*
public class BlobInfo
{
    public BinaryData Content { get; set; }
    public string ContentType { get; set; }

    public BlobInfo(BinaryData Content, string ContentType)
    {
        this.Content = Content;
        this.ContentType = ContentType;
    }
}

public async Task<BlobInfo> GetBlobAsync(string name, CancellationToken cancellationToken)
{
    var containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
    var blobClient = containerClient.GetBlobClient(name);
    var blobDownloadInfo = await blobClient.DownloadContentAsync(cancellationToken);

    return new BlobInfo(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.Details.ContentType);
}
*/