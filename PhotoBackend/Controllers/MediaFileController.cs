using Microsoft.AspNetCore.Mvc;
using Google.Cloud.BigQuery.V2;
using Google.Cloud.Storage.V1;
using System;
using System.IO;

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
            string projectId = "photosharesite";
            string dataset = "photosharesit_dataset";
            string proc = "selectfiles";

            BigQueryClient client = BigQueryClient.Create(projectId);
            string query = $"call {projectId}.{dataset}.{proc}()";
            BigQueryJob job = client.CreateQueryJob(
                sql: query,
                parameters: null,
                options: new QueryOptions { UseQueryCache = false });
            // Wait for the job to complete.
            job.PollUntilCompleted();
            List<MediaFile> output = new List<MediaFile>();
            // Display the results
            foreach (BigQueryRow row in client.GetQueryResults(job.Reference))
            {
                output.Add(new MediaFile(
                    (long)row["id"],
                    (string)row["url"],
                    (DateTime)row["uploadDate"],
                    String.Equals(userIP, (string)row["ipAddress"], StringComparison.OrdinalIgnoreCase)
                    )
                    );
            }
            return output;
        }
     }
}