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

        [HttpGet("simple", Name = "GetSimpleTestData")]
        public List<string> TestSimpleQuery(
        string projectId = "photosharesite"
    )
        {
            BigQueryClient client = BigQueryClient.Create(projectId);
            BigQueryTable table = client.GetTable("bigquery-public-data", "samples", "shakespeare");

            string sql = $"SELECT corpus AS title, COUNT(word) AS unique_words FROM {table} GROUP BY title ORDER BY unique_words DESC LIMIT 10";
            BigQueryParameter[] parameters = null;
            BigQueryResults results = client.ExecuteQuery(sql, parameters);
            List<string> output = new List<string>();

            foreach (BigQueryRow row in results)
            {
                output.Add($"{row["title"]}: {row["unique_words"]}");
            }
            return output;
        }

        [HttpGet("parametrize", Name = "GetParametrizedTestData")]
        public List<string> TestParametrizedQuery(
        string projectId = "photosharesite"
    )
        {
            BigQueryClient client = BigQueryClient.Create(projectId);
            BigQueryTable table = client.GetTable("bigquery-public-data", "usa_names", "usa_1910_2013");

            string sql = $"SELECT name FROM {table} WHERE state = @state LIMIT 100";

            List<string> output = new List<string>();
            BigQueryParameter[] parameters = new[]
            {
                new BigQueryParameter("state", BigQueryDbType.String, "NY"),
            };
            BigQueryResults results = client.ExecuteQuery(sql, parameters);
            

            foreach (BigQueryRow row in results)
            {
                output.Add(Convert.ToString(row["name"]));
            }
            return output;
        }
    }
}