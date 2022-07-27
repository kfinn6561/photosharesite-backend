using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

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

        [HttpGet("saveuser", Name = "saveuser")]
        public void SaveUser()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"IPAddress", (object)"127.0.0.1" }
            };
            dbConnection.ExecuteNonQuery("InsertTestUser", parameters);
        }

        [HttpGet("getusers",Name = "getusers")]
        public string GetUsers()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            DataTable users = dbConnection.ExecuteReader("SelectUsers", new Dictionary<string, object> { });

            return JsonConvert.SerializeObject(users);
        }

    }
}