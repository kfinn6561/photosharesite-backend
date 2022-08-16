using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using PhotoBackend.Models;
using PhotoBackend.Data;

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

        [HttpGet("testsaveuser", Name = "testsaveuser")]
        public void SaveUser()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"IPAddress", (object)"127.0.0.1" }
            };
            dbConnection.ExecuteNonQuery("InsertTestUser", parameters);
            dbConnection.Close();
        }

        [HttpGet("testgetusers",Name = "testgetusers")]
        public List<User> GetUsers()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            DataTable users = dbConnection.ExecuteReader("SelectUsers");
            dbConnection.Close();

            List<User> usersList = new List<User>();
            foreach(DataRow row in users.Rows)
            {
                usersList.Add(new Models.User((int)row["UserID"], (string)row["IPAddress"]));
            }

            return usersList;

        }

    }
}