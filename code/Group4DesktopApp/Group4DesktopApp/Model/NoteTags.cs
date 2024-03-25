using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.Model
{
    public class NoteTags
    {
        public string TagName { get; set; }
        public int NotesId { get; set; }

        public NoteTags() { 
            this.TagName = string.Empty;
            this.NotesId = -1;
        }

        public NoteTags(string tagName, int notesId)
        {
            TagName = tagName;
            NotesId = notesId;
        }
    }
}
