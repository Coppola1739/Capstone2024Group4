using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.Model
{
    public class Notes : INotifyPropertyChanged
    {
        private string content;
        public event PropertyChangedEventHandler? PropertyChanged;

        public int NotesId { get; set; }
        public int SourceId { get; set; }
        public string Content {
            get { return content; }
            set
            {
                content = value;
                NotifyPropertyChanged(nameof(Content));
            }
        }

        public Notes() {
            this.NotesId = -1;
            this.SourceId = -1;
            this.content = string.Empty;
            this.Content = string.Empty;
        }

        public Notes(int notesId, int sourceId, string content)
        {
            this.NotesId = notesId;
            this.SourceId = sourceId;
            this.content = content;
            this.Content = content;
        }

        public override string ToString()
        {
            return this.Content;
        }

        private void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
