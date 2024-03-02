using System;
using System.IO;

namespace Group4DesktopApp.Model
{
    public class Source
    {

        public int SourceId { get; set; }
        public int UserId { get; set; }
        public string SourceName { get; set; }
        public DateTime UploadDate { get; set; }
        public Byte[] Content { get; set; }
        public string SourceType { get; set; }

        public string SourceImage {
            get { return SourceIconPaths.GetIconPath(SourceType); }
        }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Title { get; set; }

        public Source()
        {
            this.SourceId = -1;
            this.UserId = -1;
            this.SourceName = string.Empty;
            this.UploadDate = DateTime.Now;
            this.Content = new byte[1];
            this.SourceType = string.Empty;
            this.AuthorFirstName = string.Empty;
            this.AuthorLastName = string.Empty;
            this.Title = string.Empty;
        }
        public Source(int sourceid, int id, string sourcename, DateTime uploadDate, byte[] content, string sourceType, string authorFirstname, string authorLastname, string title)
        {
            this.SourceId = sourceid;
            this.UserId = id;
            this.SourceName = sourcename;
            this.UploadDate = uploadDate;
            this.Content = content;
            this.SourceType = sourceType;
            this.AuthorFirstName = authorFirstname;
            this.AuthorLastName = authorLastname;
            this.Title = title;

        }

        public override string ToString()
        {
            return $"Type: {this.SourceType} Name: {this.SourceName}";
        }
    }
}
