﻿using Group4DesktopApp.DAL;
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
        private string noteInputText;
        private Notes? selectedNote;

        public RelayCommand UpdateButtonCommand => 
            new RelayCommand(execute => defaultRelayExecute(), 
                canExecute => IsInputTextModified);

        public SourcePageViewModel()
        {
            this.notes = new ObservableCollection<Notes>();
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

        private void updateList(int sourceId)
        {
            var updatedList = NotesDAL.GetAllNotesBySourceId(sourceId);
            var result = updatedList.Where(p => !this.notes.Any(p2 => p2.NotesId == p.NotesId));
            foreach (var note in result)
            {
                this.notes.Add(note);
            }
        }

        public Notes? InsertNewNote(int sourceId)
        {
            bool success = NotesDAL.AddNoteToSource(sourceId, NoteInputProperty);
            if(success)
            {
                //this.PopulateNotesByID(sourceId);
                //Notes fake = new Notes(18,sourceId,"This is a dummy string to test my theory");
                this.updateList(sourceId);
            }
            return null;
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
