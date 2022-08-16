using PhotoBackend.Models;
using System.Data;

namespace PhotoBackend.Data
{
    public class DatabaseController
    {
        public DatabaseController()
        { }

        public List<MediaFile> GetAllFiles(string userIP)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            DataTable files = dbConnection.ExecuteReader("SelectFiles");
            dbConnection.Close();

            List<MediaFile> output = new List<MediaFile>();

            foreach (DataRow row in files.Rows)
            {
                output.Add(new MediaFile(
                    (int)row["FileID"],
                    (string)row["url"],
                    (DateTime)row["uploadDate"],
                    String.Equals(userIP, (string)row["ipAddress"], StringComparison.OrdinalIgnoreCase),
                    (string)row["fileName"]
                    )
                    );
            }
            return output;
        }

        public void InsertFile(int userID, string url, string fileName)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"url", (object)url },
                {"ownerID", userID},
                {"filename", fileName}
            };
            dbConnection.ExecuteNonQuery("InsertFile", parameters);
            dbConnection.Close();
        }

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

        public int GetUserID(string IPAddress)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"NewIPAddress", (object)IPAddress }
            };

            DataTable result = dbConnection.ExecuteReader("InsertOrSelectUser", parameters);
            dbConnection.Close();

            return (int)result.Rows[0]["ID"];
        }

        public void DeleteFile(int fileID)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"FileIDToDelete", (object)fileID }
            };

            dbConnection.ExecuteNonQuery("DeleteFile", parameters);
            dbConnection.Close();
        }

        public MediaFile GetFile(int fileID, string userIP)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"FileID", (object)fileID }
            };

            var result = dbConnection.ExecuteReader("GetFile", parameters);
            dbConnection.Close();

            var row = result.Rows[0];

            var output = new MediaFile(
                    (int)row["FileID"],
                    (string)row["url"],
                    (DateTime)row["uploadDate"],
                    String.Equals(userIP, (string)row["ipAddress"], StringComparison.OrdinalIgnoreCase),
                    (string)row["fileName"]
                    );
            return output;

        }

    }

    }
