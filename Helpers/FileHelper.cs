using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Music_Api.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> UploadImage(IFormFile file)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=mussicstorageaccount;AccountKey=Utx6U7jildKpRrldOSEbJKN3c+8wGSUTE9bptdlrBOom+NjTPHOYEnnZfIxvcq+4ywHTP21/CLVShSSkMX6NwQ==;EndpointSuffix=core.windows.net";
            string containerName = "songscover";

            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream);
            return blobClient.Uri.AbsoluteUri;
        }

        public static async Task<string> UploadFile(IFormFile file)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=mussicstorageaccount;AccountKey=Utx6U7jildKpRrldOSEbJKN3c+8wGSUTE9bptdlrBOom+NjTPHOYEnnZfIxvcq+4ywHTP21/CLVShSSkMX6NwQ==;EndpointSuffix=core.windows.net";
            string containerName = "audiofiles";

            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream);
            return blobClient.Uri.AbsoluteUri;
        }
    }
}
