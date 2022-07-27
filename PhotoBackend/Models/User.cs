namespace PhotoBackend.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string IPAddress { get; set; }

        public User(int userID, string iPAddress)
        {
            UserID = userID;
            IPAddress = iPAddress;
        }
    }
}
