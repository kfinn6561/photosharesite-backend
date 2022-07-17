namespace PhotoBackend
{
    public class MediaFile
    {
        public int ID { get; set; }

        public string URL { get; set; }

        public DateTime CreationTime { get; set; }

        private string OwnerIP;

        public MediaFile(int iD, string uRL, DateTime creationTime, string ownerIP)
        {
            ID = iD;
            URL = uRL;
            CreationTime = creationTime;
            OwnerIP = ownerIP;
        }

        public bool CanModify(string IPAddress)
        {
            return IPAddress.Equals(OwnerIP, StringComparison.OrdinalIgnoreCase);
        }
    }
}