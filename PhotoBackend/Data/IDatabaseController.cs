using PhotoBackend.Models;

namespace PhotoBackend.Data
{
    public interface IDatabaseController
    {
        void DeleteFile(int fileID);
        List<MediaFile> GetAllFiles(string userIP);
        MediaFile GetFile(int fileID, string userIP);
        int GetUserID(string IPAddress);
        List<User> GetUsers();
        void InsertFile(int userID, string url, string fileName);
        ulong SaveUser(string ipAddress);
    }
}