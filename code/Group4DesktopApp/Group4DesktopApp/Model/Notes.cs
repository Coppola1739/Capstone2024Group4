using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.Model
{
    public class Notes
    {
        public int NotesId { get; set; }
        public int SourceId { get; set; }
        public string Content { get; set; }

        public Notes() {
            this.NotesId = -1;
            this.SourceId = -1;
            this.Content = string.Empty;
        }

        public Notes(int notesId, int sourceId, string content)
        {
            this.NotesId = notesId;
            this.SourceId = sourceId;
            this.Content = content;
        }

        public override string ToString()
        {
            return this.Content;
        }
    }
}
