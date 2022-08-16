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

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost("saveuser", Name = "saveuser")]
        public UInt64 SaveUser(string ipAddress)
        {
            var dbController = new DatabaseController();
            return dbController.SaveUser(ipAddress);
        }

        [HttpGet("getusers", Name = "getusers")]
        public List<User> GetUsers()
        {
            var dbController = new DatabaseController();
            return dbController.GetUsers();

        }

        [HttpGet("getuserid", Name = "getuserid")]
        public int GetUserID(string IPAddress)
        {
            var dbController = new DatabaseController();
            return dbController.GetUserID(IPAddress);
        }

    }
}
