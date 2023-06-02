using Backend.ImageUploadModule;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
namespace Backend.MockImplementations;

public class ImageUploadService : IImageStorageService
{
private readonly IConfiguration _config;

    public ImageUploadService(IConfiguration config)
    {
        _config = config;
    }

    string containerName = "tomrybjagodno1container";
    string blobName = "blobContainer";

    public async Task<string> UploadImageAsync(Guid imageId, Stream imageStream)
    {

        BlobServiceClient blobServiceClient = new BlobServiceClient(_config.GetConnectionString("storage"));
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);

        BlobClient blobClient = containerClient.GetBlobClient(imageId.ToString());
        var response = await blobClient.UploadAsync(imageStream);

        return blobClient.Uri.AbsolutePath;
    }
}
