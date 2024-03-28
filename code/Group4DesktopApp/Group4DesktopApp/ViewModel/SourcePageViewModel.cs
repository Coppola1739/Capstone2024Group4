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
    /// <summary>
    /// The Source Page Window ViewModel
    /// </summary>
    public class SourcePageViewModel
    {
        private ObservableCollection<Notes> notes;
        private ObservableCollection<NoteTags> tags;
        private string noteInputText;
        private Notes? selectedNote;

        /// <summary>
        /// Gets the update button command.
        /// </summary>
        /// <value>
        /// The update button command.
        /// </value>
        public RelayCommand UpdateButtonCommand => 
            new RelayCommand(execute => defaultRelayExecute(), 
                canExecute => IsInputTextModified);

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcePageViewModel"/> class.
        /// </summary>
        public SourcePageViewModel()
        {
            this.notes = new ObservableCollection<Notes>();
            this.tags = new ObservableCollection<NoteTags>();
            this.noteInputText = string.Empty;
            this.selectedNote = null;
        }

        /// <summary>
        /// Gets a value indicating whether the note input field has been modified from its original content.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the note input field has been modified from its original content; otherwise, <c>false</c>.
        /// </value>
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
        /// <summary>
        /// Gets or sets the notes data property.
        /// </summary>
        /// <value>
        /// The notes data property.
        /// </value>
        public ObservableCollection<Notes> NotesDataProperty
        {
            get { return notes; }
            set
            {
                notes = value;
                NotifyPropertyChanged(nameof(NotesDataProperty));

            }
        }
        /// <summary>
        /// Gets or sets the tag data property.
        /// </summary>
        /// <value>
        /// The tag data property.
        /// </value>
        public ObservableCollection<NoteTags> TagDataProperty
        {
            get { return tags; }
            set
            {
                tags = value;
                NotifyPropertyChanged(nameof(TagDataProperty));

            }
        }
        /// <summary>
        /// Gets or sets the note input property.
        /// </summary>
        /// <value>
        /// The note input property.
        /// </value>
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
        /// <summary>
        /// Gets or sets the selected note property.
        /// </summary>
        /// <value>
        /// The selected note property.
        /// </value>
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
        /// <summary>
        /// Occurs when a property changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Populates the notes list by source ID.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        public void PopulateNotesByID(int sourceId)
        {
            this.notes = NotesDAL.GetAllNotesBySourceId(sourceId);
        }
        /// <summary>
        /// Populates the tags by selected note.
        /// </summary>
        public void PopulateTagsBySelectedNote()
        {
            if(SelectedNoteProperty != null)
            {
                this.tags.Clear();
                var noteTags = NoteTagsDAL.GetAllTagsByNoteId(SelectedNoteProperty.NotesId);
                noteTags.ToList().ForEach(this.tags.Add);
            }
        }

        /// <summary>
        /// Calls the Data Access Layer to insert the new note into the database.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        /// <returns>True if successfully added note to database, false otherwise.</returns>
        public bool InsertNewNote(int sourceId)
        {
            bool success = NotesDAL.AddNoteToSource(sourceId, NoteInputProperty);
            if(success)
            {
                this.updateList(sourceId);
            }
            return success;
        }

        /// <summary>
        /// Calls the Data Access Layer to insert the new Tag to the selected note into the database.
        /// </summary>
        /// <param name="tagName">The tag name.</param>
        /// <returns>True if successfully added tag to the database, false otherwise.</returns>
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
        /// <summary>
        /// Calls the Data Access Layer to Update the specified note content, if note exists.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <param name="newContent">The new content.</param>
        /// <returns>True if note is successfully updated, false otherwise.</returns>
        public bool UpdateNoteContent(Notes? note, string newContent)
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
        /// <summary>
        /// Calls the Data Access Layer to delete the note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns>True if note is deleted from the database, false otherwise</returns>
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
        /// <summary>
        /// Calls the Data Access Layer to Delete the tag from the selected note.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns>True if tag is removed from selected note, false otherwise.</returns>
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
