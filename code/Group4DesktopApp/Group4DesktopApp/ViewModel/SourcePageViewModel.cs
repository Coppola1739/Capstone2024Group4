using Group4DesktopApp.DAL;
using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Group4DesktopApp.ViewModel
{
    public class SourcePageViewModel
    {
        private ObservableCollection<Notes> notes;
        private ObservableCollection<NoteTags> tags;
        private string noteInputText;
        private Notes? selectedNote;

        public RelayCommand UpdateButtonCommand => 
            new RelayCommand(execute => defaultRelayExecute(), 
                canExecute => IsInputTextModified);

        public SourcePageViewModel()
        {
            this.notes = new ObservableCollection<Notes>();
            this.tags = new ObservableCollection<NoteTags>();
            this.noteInputText = string.Empty;
            this.selectedNote = null;
        }

        public bool IsInputTextModified
        {
            get
            {
                if (SelectedNoteProperty != null && !NoteInputProperty.Equals(SelectedNoteProperty.Content))
                {
                    return true;
                }
                return false;
            }
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

        public ObservableCollection<NoteTags> TagDataProperty
        {
            get { return tags; }
            set
            {
                tags = value;
                NotifyPropertyChanged(nameof(TagDataProperty));

            }
        }

        public string NoteInputProperty
        {
            get { return noteInputText; }
            set
            {
                noteInputText = value;
                NotifyPropertyChanged(nameof(NoteInputProperty));
                NotifyPropertyChanged(nameof(IsInputTextModified));
            }
        }

        public Notes? SelectedNoteProperty
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                NotifyPropertyChanged(nameof(SelectedNoteProperty));
                NotifyPropertyChanged(nameof(IsInputTextModified));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void PopulateNotesByID(int sourceId)
        {
            this.notes = NotesDAL.GetAllNotesBySourceId(sourceId);
        }

        public void PopulateTagsBySelectedNote()
        {
            if(SelectedNoteProperty != null)
            {
                this.tags.Clear();
                var noteTags = NoteTagsDAL.GetAllTagsByNoteId(SelectedNoteProperty.NotesId);
                noteTags.ToList().ForEach(this.tags.Add);
            }
        }

        private void updateList(int sourceId)
        {
            var updatedList = NotesDAL.GetAllNotesBySourceId(sourceId);
            var result = updatedList.Where(p => !this.notes.Any(p2 => p2.NotesId == p.NotesId));
            foreach (var note in result)
            {
                this.notes.Add(note);
            }
        }

        private void updateTags()
        {
            if (SelectedNoteProperty != null)
            {
                var updatedList = NoteTagsDAL.GetAllTagsByNoteId(SelectedNoteProperty.NotesId);
                var result = updatedList.Where(p => !this.tags.Any(p2 => p2.TagName == p.TagName));
                foreach (var tag in result)
                {
                    this.tags.Add(tag);
                }
            }
        }

        public bool InsertNewNote(int sourceId)
        {
            bool success = NotesDAL.AddNoteToSource(sourceId, NoteInputProperty);
            if(success)
            {
                this.updateList(sourceId);
            }
            return success;
        }

        public bool InsertNewTag(string tagName)
        {
            if(SelectedNoteProperty != null)
            {
                bool success = NoteTagsDAL.AddTagToNote(tagName, SelectedNoteProperty.NotesId);
                if (success)
                    {
                        this.updateTags();
                    }
                return success;
            }
            return false;
        }

        public bool UpdateExistingNote(Notes? note, string newContent)
        {
            if (note == null) return false;
            bool success = NotesDAL.UpdateNoteContent(note.NotesId, newContent);
            if (success)
            {
                int noteIndex = this.notes.IndexOf(note);
                this.notes[noteIndex].Content = newContent;
            }
            return success;
        }

        public bool DeleteNote(Notes? note)
        {
            if (note == null) return false;
            bool success = NotesDAL.DeleteNoteById(note.NotesId);
            if (success)
            {
                this.notes.Remove(note);
            }
            return success;
        }

        public bool DeleteTagFromNote(string tagName)
        {
           NoteTags? tag = this.tags.FirstOrDefault(x => x.TagName == tagName);
            if (SelectedNoteProperty != null && tag != null)
            {
                bool success = NoteTagsDAL.DeleteTagFromNote(tag);
                if (success)
                {
                    this.tags.Remove(tag);
                }
                return success;
            }
            return false;
        }

        private void defaultRelayExecute()
        {
            return;
        }

        private void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
