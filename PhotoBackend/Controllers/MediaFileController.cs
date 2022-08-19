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
        private readonly IDatabaseController _databaseController;

        public MediaFileController(ILogger<MediaFileController> logger, ICloudStorage cloudStorage, IDatabaseController databaseController)
        {
            _logger = logger;
            _coudstorage = cloudStorage;
            _databaseController = databaseController;
        }

        [HttpGet("all", Name = "GetAllFiles")]
        public List<MediaFile> GetAllFiles(string userIP)
        {
            return _databaseController.GetAllFiles(userIP);
        }

        [HttpPost("insert", Name = "InsertFile")]
        public void InsertFile(int userID, string url, string fileName)
        {
            _databaseController.InsertFile(userID, url, fileName);
        }
        
        [HttpPost("upload", Name = "UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file, string IPAdress)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            Guid uuid = Guid.NewGuid();
            string fileName = $"{uuid.ToString()}{fileExtension}";

            string URL = await _coudstorage.UploadFileAsync(file, fileName);

            int ownerID = _databaseController.GetUserID(IPAdress);

            _databaseController.InsertFile(ownerID, URL, fileName);

            return Ok();

        }


        [HttpDelete("delete", Name = "DeleteFile")]
        public async Task<IActionResult> DeleteFile(int fileID, string IPAddress)
        {
            var file = _databaseController.GetFile(fileID, IPAddress);

            if (!file.IsModifyable) {
                return Unauthorized();
            }

            await _coudstorage.DeleteFileAsync(file.FileName);
            _databaseController.DeleteFile(fileID);

            return Ok();
        }

        [HttpGet("download", Name = "DownloadFile")]
        public ActionResult DownloadFile(int fileID)
        {
            var file = _databaseController.GetFile(fileID, "");

            var downloadURL = _coudstorage.GetDownloadUrl(file.FileName);

            Response.ContentType = "application/octet-stream";

            return Redirect(downloadURL);
        }


    }
}