using Microsoft.AspNetCore.Mvc;
using Google.Cloud.BigQuery.V2;
using System;

namespace PhotoBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestSQLController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public TestSQLController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "saveuser")]
        public void SaveUser()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"IPAddress", (object)"127.0.0.1" }
            };
            dbConnection.ExecuteNonQuery("SaveUser", parameters);
        }

    }
}