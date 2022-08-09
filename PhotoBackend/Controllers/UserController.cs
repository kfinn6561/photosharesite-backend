using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using PhotoBackend.Models;


namespace PhotoBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost("saveuser", Name = "saveuser")]
        public UInt64 SaveUser(string ipAddress)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"IPAddress", (object)ipAddress }
            };
            DataTable userIDTable = dbConnection.ExecuteReader("InsertUser", parameters);
            dbConnection.Close();

            return (UInt64)userIDTable.Rows[0]["ID"];
        }

        [HttpGet("getusers", Name = "getusers")]
        public List<User> GetUsers()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            DataTable users = dbConnection.ExecuteReader("SelectUsers");
            dbConnection.Close();

            List<User> usersList = new List<User>();
            foreach (DataRow row in users.Rows)
            {
                usersList.Add(new Models.User((int)row["UserID"], (string)row["IPAddress"]));
            }

            return usersList;

        }

    }
}
