using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Identity;
using TatooMarket.Domain.Repositories.Azure;

namespace TatooMarket.Infrastructure.Azure
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly BlobServiceClient _blobClient;

        public AzureStorageService(BlobServiceClient blobClient) => _blobClient = blobClient;

        public async Task DeleteContainer(string containerName)
        {
            var container = _blobClient.GetBlobContainerClient(containerName);

            await container.DeleteIfExistsAsync();
        }

        public async Task<string> GetImage(string containerName, string fileName)
        {
            var container = _blobClient.GetBlobContainerClient(containerName);
            var exists = await container.ExistsAsync();

            if(!exists.Value || string.IsNullOrEmpty(fileName))
                return "";

            var blobClient = container.GetBlobClient(fileName);

            exists = await blobClient.ExistsAsync();

            if(!exists.Value)
                return "";

            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = containerName,
                BlobName = fileName,
                ExpiresOn = DateTime.UtcNow.AddMinutes(40),
                Resource = "b"
            };

            sasBuilder.SetPermissions(BlobAccountSasPermissions.Read);

            return blobClient.GenerateSasUri(sasBuilder).ToString();
        }

        public async Task UploadUser(UserEntity user, Stream file, string fileName)
        {
            var container = _blobClient.GetBlobContainerClient(user.UserIdentifier.ToString());
            await container.CreateIfNotExistsAsync();

            var client = container.GetBlobClient(fileName);
            await client.UploadAsync(file, overwrite: true);
        }
    }
}
