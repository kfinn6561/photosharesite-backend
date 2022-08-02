using Microsoft.AspNetCore.Mvc;
using Google.Cloud.BigQuery.V2;
using Google.Cloud.Storage.V1;
using System;
using System.IO;
using PhotoBackend.Models;

namespace PhotoBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaFileController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public MediaFileController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("all", Name = "GetAllFiles")]
        public List<MediaFile> GetAllFiles(string userIP)
        {
            //DatabaseConnection dbConnection = new DatabaseConnection();
            //DataTable fileDataTable = dbConnection.ExecuteReader
            //List<MediaFile> output = new List<MediaFile>();
            //// Display the results
            //foreach (BigQueryRow row in client.GetQueryResults(job.Reference))
            //{
            //    output.Add(new MediaFile(
            //        (long)row["id"],
            //        (string)row["url"],
            //        (DateTime)row["uploadDate"],
            //        String.Equals(userIP, (string)row["ipAddress"], StringComparison.OrdinalIgnoreCase)
            //        )
            //        );
            //}
            List < MediaFile > output = new List < MediaFile >();
            return output;
        }
     }
}