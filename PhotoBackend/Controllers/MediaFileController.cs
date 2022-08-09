using Microsoft.AspNetCore.Mvc;
using Google.Cloud.BigQuery.V2;
using Google.Cloud.Storage.V1;
using System;
using System.IO;
using PhotoBackend.Models;
using System.Data;

namespace PhotoBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaFileController : ControllerBase
    {

        private readonly ILogger<MediaFileController> _logger;

        public MediaFileController(ILogger<MediaFileController> logger)
        {
            _logger = logger;
        }

        [HttpGet("all", Name = "GetAllFiles")]
        public List<MediaFile> GetAllFiles(string userIP)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            DataTable files = dbConnection.ExecuteReader("SelectFiles");
            dbConnection.Close();

            List<MediaFile> output = new List<MediaFile>();

            foreach (DataRow row in files.Rows)
            {
                output.Add(new MediaFile(
                    (int)row["FileID"],
                    (string)row["url"],
                    (DateTime)row["uploadDate"],
                    String.Equals(userIP, (string)row["ipAddress"], StringComparison.OrdinalIgnoreCase)
                    )
                    );
            }
            return output;
        }

        [HttpPost("insert", Name = "InsertFile")]
        public void InsertFile(int userID, string url)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"url", (object)url },
                {"ownerID", userID}
            };
            dbConnection.ExecuteNonQuery("InsertFile", parameters);
            dbConnection.Close();
        }
    }
}