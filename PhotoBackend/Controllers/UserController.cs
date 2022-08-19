using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using PhotoBackend.Models;
using PhotoBackend.Data;

namespace PhotoBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IDatabaseController _databaseController;

        public UserController(ILogger<UserController> logger, IDatabaseController databaseController)
        {
            _logger = logger;
            _databaseController = databaseController;
        }

        [HttpPost("saveuser", Name = "saveuser")]
        public UInt64 SaveUser(string ipAddress)
        {
            return _databaseController.SaveUser(ipAddress);
        }

        [HttpGet("getusers", Name = "getusers")]
        public List<User> GetUsers()
        {
            return _databaseController.GetUsers();

        }

        [HttpGet("getuserid", Name = "getuserid")]
        public int GetUserID(string IPAddress)
        {
            return _databaseController.GetUserID(IPAddress);
        }

    }
}
