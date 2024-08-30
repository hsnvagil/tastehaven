#region

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

#endregion

namespace Taste_Haven_API.Services;

public interface IBlobService
{
    Task<string> GetBlob(string blobName, string containerName);
    Task<bool> DeleteBlob(string blobName, string containerName);
    Task<string> UploadBlob(string blobName, string containerName, IFormFile file);
}

public class BlobService(BlobServiceClient client) : IBlobService
{
    public async Task<bool> DeleteBlob(string blobName, string containerName)
    {
        var blobContainerClient = client.GetBlobContainerClient(containerName);
        var blobClient = blobContainerClient.GetBlobClient(blobName);

        return await blobClient.DeleteIfExistsAsync();
    }

    public async Task<string> GetBlob(string blobName, string containerName)
    {
        var blobContainerClient = client.GetBlobContainerClient(containerName);
        var blobClient = blobContainerClient.GetBlobClient(blobName);

        return blobClient.Uri.AbsoluteUri;
    }

    public async Task<string> UploadBlob(string blobName, string containerName, IFormFile file)
    {
        var blobContainerClient = client.GetBlobContainerClient(containerName);
        var blobClient = blobContainerClient.GetBlobClient(blobName);

        var httpHeaders = new BlobHttpHeaders
        {
            ContentType = file.ContentType
        };

        var result = await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);
        if (result != null) return await GetBlob(blobName, containerName);
        return "";
    }
}