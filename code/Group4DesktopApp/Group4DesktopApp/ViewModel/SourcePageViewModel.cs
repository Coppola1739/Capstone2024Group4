using Group4DesktopApp.DAL;
using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.ViewModel
{
    public class SourcePageViewModel
    {
        private ObservableCollection<Notes> notes;
        private string noteInputText;

        public SourcePageViewModel()
        {
            this.notes = new ObservableCollection<Notes>();
            this.noteInputText = string.Empty;
        }

        public ObservableCollection<Notes> NotesDataProperty
        {
            get { return notes; }
            set
            {
                notes = value;
                NotifyPropertyChanged(nameof(NotesDataProperty));

            }
        }

        public string NoteInputProperty
        {
            get { return noteInputText; }
            set
            {
                noteInputText = value;
                NotifyPropertyChanged(NoteInputProperty);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void PopulateNotesByID(int sourceId)
        {
            this.notes = NotesDAL.GetAllNotesBySourceId(sourceId);
        }

        public bool InsertNewNote(int sourceId)
        {
            bool success = NotesDAL.AddNoteToSource(sourceId, NoteInputProperty);
            if(success)
            {
                this.PopulateNotesByID(sourceId);
            }
            return success;
        }

        private void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
