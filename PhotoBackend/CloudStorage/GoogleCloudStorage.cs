using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;


namespace PhotoBackend.CloudStorage
{
    public class GoogleCloudStorage : ICloudStorage
    {
        private readonly GoogleCredential googleCredential;
        private readonly StorageClient storageClient;
        private readonly string bucketName;
        private readonly UrlSigner urlSigner;
        private readonly int URLExpiryTimeHours;


        public GoogleCloudStorage (IConfiguration configuration)
        {
            var gcpfilenme=configuration.GetValue<string>("GCPCredentialFile");
            googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("GCPCredentialFile"));
            storageClient = StorageClient.Create(googleCredential);
            bucketName = configuration.GetValue<string>("GCPBucketName");
            urlSigner = UrlSigner.FromServiceAccountCredential(googleCredential.UnderlyingCredential as ServiceAccountCredential);
            URLExpiryTimeHours = configuration.GetValue<int>("URLExpiryTimeHours");
        }

        public async Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                var dataObject = await storageClient.UploadObjectAsync(bucketName, fileNameForStorage, null, memoryStream);
                return dataObject.MediaLink;
            }
        }

        public async Task DeleteFileAsync(string fileName)
        {
            await storageClient.DeleteObjectAsync(bucketName, fileName);
        }

        public string GetDownloadUrl(string fileName)
        {
            var signUrl = urlSigner.Sign(
                bucketName,
                fileName,
                TimeSpan.FromHours(3),
                HttpMethod.Get
            );
            return signUrl;
        }

    }
}
