using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;


namespace PhotoBackend.CloudStorage
{
    public interface ICloudStorage
    {
        Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage);
        Task DeleteFileAsync(string fileNameForStorage);
        string GetDownloadUrl(string fileName);
    }
}
