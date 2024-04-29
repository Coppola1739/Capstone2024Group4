using Group4DesktopApp.DAL;
using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Group4DesktopApp.View
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        private User loggedInUser;
        private List<string> searchedTags;
        public SearchWindow(User loggedInUser)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;
            this.autoComplete.FilterMode = AutoCompleteFilterMode.Contains;
            List<string> tagsUnderUser = NoteTagsDAL.GetAllTagsByUserId(loggedInUser.UserId).Select(o => o.TagName).ToList();
            this.autoComplete.ItemsSource = tagsUnderUser;
            this.searchedTags = new List<string>();
        }

        private void btnRemoveTag_Click(object sender, RoutedEventArgs e)
        {
            var removeButton = sender as Control;
            if (removeButton == null)
            {
                return;
            }

            Tags? selectedTag = removeButton.DataContext as Tags;
            if (selectedTag != null)
            {
                this.lstSearchedTags.Items.Remove(selectedTag);
                this.searchedTags.Remove(selectedTag.TagName);
                this.handleSearching();
            }
        }

        private void handleSearching()
        {
            if (string.IsNullOrWhiteSpace(this.autoComplete.Text) && this.lstSearchedTags.Items.Count <= 0)
            {
                this.lstSearchResult.Visibility = Visibility.Collapsed;
                this.lstSearchedTags.Visibility = Visibility.Collapsed;
            }   
            else
            {

                this.updateSearchResult();
            }
        }

        private void updateSearchResult()
        {
            this.lstSearchResult.Visibility = Visibility.Visible;
            this.lstSearchedTags.Visibility = Visibility.Visible;
            this.lstSearchResult.Items.Clear();
            var list = NoteTagsDAL.GetAllTagsByUserId(this.loggedInUser.UserId);
            var notes = NotesDAL.GetAllNotesByUserId(this.loggedInUser.UserId);

            var foundNotes = notes.Where(obj => list.Any(aObj =>
                aObj.TagName.IndexOf(this.autoComplete.Text, StringComparison.OrdinalIgnoreCase) >= 0 &&
                aObj.NotesId == obj.NotesId) && !string.IsNullOrWhiteSpace(this.autoComplete.Text));

            foreach (var note in this.searchedTags)
            {
                var foundNotes2 = notes.Where(obj => list.Any(aObj =>
                aObj.TagName.IndexOf(note, StringComparison.OrdinalIgnoreCase) >= 0 &&
                aObj.NotesId == obj.NotesId));
                foundNotes = foundNotes.Union(foundNotes2);
            }

            var sortedListA = foundNotes.OrderByDescending(a =>
                list.Count(b => b.NotesId == a.NotesId && this.searchedTags.Any(c => string.Equals(c, b.TagName, StringComparison.OrdinalIgnoreCase))));


            if (foundNotes.Any())
            {
                this.lstSearchResult.Items.Clear();
                foreach (var note in sortedListA)
                {
                    this.lstSearchResult.Items.Add(note);
                }
            }
        }

        private void autoComplete_TextChanged(object sender, RoutedEventArgs e)
        {
            this.handleSearching();
        }

        private void btnAddFilter_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.autoComplete.Text) && !this.searchedTags.Contains(this.autoComplete.Text, StringComparer.OrdinalIgnoreCase))
            {
                this.lstSearchedTags.Items.Add(new Tags(this.autoComplete.Text));
                this.searchedTags.Add(this.autoComplete.Text);
            }
        }

        private void lstSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Notes? selectedNote = this.lstSearchResult.SelectedItem as Notes;
            if (selectedNote == null)
            {
                return;
            }

            Source selectedSource = SourceDAL.GetSourceById(selectedNote.SourceId);
            if (selectedSource != null)
            {
                SourcePageWindow sourcePageWindow = new SourcePageWindow(loggedInUser, selectedSource);
                sourcePageWindow.Show();
                this.Close();
            }
            Debug.WriteLine(this.lstSearchResult.Items.GetItemAt(0).ToString());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Window HomeWindow = new HomeWindow(this.loggedInUser);
            HomeWindow.Show();
            this.Close();
        }

        private void btnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            this.autoComplete.Text = string.Empty;
        }
    }
}
