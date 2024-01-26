namespace WebApp.Server.Models
{
    public class UserFile
    {
        public int UserFileId { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public byte[] FileContent { get; set; } // New property for storing PDF content

        public User User { get; set; }
    }
}
