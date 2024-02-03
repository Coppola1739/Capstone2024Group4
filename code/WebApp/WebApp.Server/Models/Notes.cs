namespace WebApp.Server.Models
{
    public class Notes
    {
        public int NotesId { get; set; }

        public int SourceId { get; set; }

        public string Content { get; set; }

        public Source Source { get; set; }
    }
}
