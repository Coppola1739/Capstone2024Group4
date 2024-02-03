namespace WebApp.Server.Models
{
    public class Source
    {
        public int SourceId { get; set; }
        public int UserId { get; set; }
        public string SourceName { get; set; }
        public DateTime UploadDate { get; set; }
        public byte[] Content { get; set; } // New property for storing PDF content
        public string SourceType { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public string Title { get; set; }

        public User User { get; set; }

        public ICollection<Notes> Notes { get; set; }

    }
}
