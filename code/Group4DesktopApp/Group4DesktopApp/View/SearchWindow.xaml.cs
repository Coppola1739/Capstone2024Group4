﻿using Group4DesktopApp.DAL;
using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Group4DesktopApp.View
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        private User loggedInUser;
        private List<string> searchedTags;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchWindow"/> class.
        /// </summary>
        /// <param name="loggedInUser">The logged in user.</param>
        public SearchWindow(User loggedInUser)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;
            this.autoComplete.FilterMode = AutoCompleteFilterMode.Contains;
            List<string> tagsUnderUser = NoteTagsDAL.GetAllTagsByUserId(loggedInUser.UserId).Select(o => o.TagName).ToList();
            this.autoComplete.ItemsSource = tagsUnderUser;
            this.searchedTags = new List<string>();
            this.lstSearchResult.PreviewMouseRightButtonDown += ListView_PreviewMouseRightButtonDown;
        }

        private void ListView_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
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
            if (this.lstSearchedTags.Items.Count <= 0 || string.IsNullOrWhiteSpace(this.autoComplete.Text) && this.lstSearchedTags.Items.Count <= 0)
            {
                this.lstSearchResult.Visibility = Visibility.Collapsed;
                this.lstSearchedTags.Visibility = Visibility.Collapsed;
            }
        }

        private void updateSearchResult()
        {
            this.lstSearchResult.Visibility = Visibility.Visible;
            this.lstSearchedTags.Visibility = Visibility.Visible;
            this.lstSearchResult.Items.Clear();
            var list = NoteTagsDAL.GetAllTagsByUserId(this.loggedInUser.UserId);
            var notes = NotesDAL.GetAllNotesByUserId(this.loggedInUser.UserId);

            IEnumerable<Notes> foundNotes = new Collection<Notes>();

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

        private void btnAddFilter_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.autoComplete.Text) && !this.searchedTags.Contains(this.autoComplete.Text, StringComparer.OrdinalIgnoreCase))
            {
                this.lstSearchedTags.Items.Add(new Tags(this.autoComplete.Text));
                this.searchedTags.Add(this.autoComplete.Text);
                this.lstSearchedTags.Visibility = Visibility.Visible;
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
        }

        private void btnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            this.autoComplete.Text = string.Empty;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            if (this.GetType() != typeof(HomeWindow))
            {
                HomeWindow homeWindow = new HomeWindow(this.loggedInUser);
                homeWindow.Show();
                this.Close();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (this.GetType() != typeof(SearchWindow))
            {
                SearchWindow searchWindow = new SearchWindow(loggedInUser);
                searchWindow.Show();
                this.Close();
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmBox = AlertDialog.LogoutConfirm();
            if (confirmBox == MessageBoxResult.Yes)
            {

                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        private void btnTagSearch_Click(object sender, RoutedEventArgs e)
        {
            this.updateSearchResult();
        }
    }
}
