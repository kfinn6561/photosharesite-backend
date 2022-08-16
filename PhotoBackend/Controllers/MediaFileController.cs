using Microsoft.AspNetCore.Mvc;
using Google.Cloud.BigQuery.V2;
using Google.Cloud.Storage.V1;
using System;
using System.IO;
using PhotoBackend.Models;
using System.Data;
using System.Web;
using PhotoBackend.CloudStorage;
using PhotoBackend.Data;

namespace PhotoBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaFileController : ControllerBase
    {

        private readonly ILogger<MediaFileController> _logger;
        private readonly ICloudStorage _coudstorage;

        public MediaFileController(ILogger<MediaFileController> logger, ICloudStorage cloudStorage)
        {
            _logger = logger;
            _coudstorage = cloudStorage;
        }

        [HttpGet("all", Name = "GetAllFiles")]
        public List<MediaFile> GetAllFiles(string userIP)
        {
            var dbController = new DatabaseController();
            return dbController.GetAllFiles(userIP);
        }

        [HttpPost("insert", Name = "InsertFile")]
        public void InsertFile(int userID, string url, string fileName)
        {
            var dbController = new DatabaseController();
            dbController.InsertFile(userID, url, fileName);
        }
        
        [HttpPost("upload", Name = "UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file, string IPAdress)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            Guid uuid = Guid.NewGuid();
            string fileName = $"{uuid.ToString()}{fileExtension}";

            string URL = await _coudstorage.UploadFileAsync(file, fileName);

            var dbController = new DatabaseController();

            int ownerID = dbController.GetUserID(IPAdress);

            dbController.InsertFile(ownerID, URL, fileName);

            return Ok();

        }


        [HttpDelete("delete", Name = "DeleteFile")]
        public async Task<IActionResult> DeleteFile(int fileID, string IPAddress)
        {
            var dbController = new DatabaseController();

            var file = dbController.GetFile(fileID, IPAddress);

            if (!file.IsModifyable) {
                return Unauthorized();
            }

            await _coudstorage.DeleteFileAsync(file.FileName);
            dbController.DeleteFile(fileID);

            return Ok();
        }




    }
}