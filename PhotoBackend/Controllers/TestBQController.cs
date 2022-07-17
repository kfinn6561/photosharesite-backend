using Microsoft.AspNetCore.Mvc;
using Google.Cloud.BigQuery.V2;
using System;

namespace PhotoBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestBQController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public TestBQController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTestData")]
        public List<string> TestQuery(
        string projectId = "photosharesite"
    )
        {
            BigQueryClient client = BigQueryClient.Create(projectId);
            string query = @"
            SELECT name FROM `bigquery-public-data.usa_names.usa_1910_2013`
            WHERE state = 'TX'
            LIMIT 100";
            BigQueryJob job = client.CreateQueryJob(
                sql: query,
                parameters: null,
                options: new QueryOptions { UseQueryCache = false });
            // Wait for the job to complete.
            job.PollUntilCompleted();
            List<string> output = new List<string>();
            // Display the results
            foreach (BigQueryRow row in client.GetQueryResults(job.Reference))
            {
                output.Add($"{row["name"]}");
            }
            return output;
        }
    }
}