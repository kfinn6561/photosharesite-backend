namespace PhotoBackend.Models
{
    public class MediaFile
    {
        public long ID { get; set; }
        public string URL { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsModifyable { get; set; }
        public string FileName { get; set; }


        public MediaFile(long iD, string uRL, DateTime creationTime, bool isModifyable, string fileName)
        {
            ID = iD;
            URL = uRL;
            CreationTime = creationTime;
            IsModifyable = isModifyable;
            FileName = fileName;
        }

    }
}