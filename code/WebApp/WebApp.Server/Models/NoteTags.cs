using System.Diagnostics.CodeAnalysis;

namespace WebApp.Server.Models
{
    [ExcludeFromCodeCoverage]
    public class NoteTags
    {
        public string TagName { get; set; }

        public int NotesId { get; set; }

    }
}
