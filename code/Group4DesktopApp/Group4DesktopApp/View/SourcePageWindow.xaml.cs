using Group4DesktopApp.DAL;
using Group4DesktopApp.Model;
using Group4DesktopApp.Resources;
using Group4DesktopApp.Utilities;
using Group4DesktopApp.ViewModel;
using Microsoft.Web.WebView2.Core;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;


namespace Group4DesktopApp.View
{
    /// <summary>
    /// Interaction logic for SourcePageWindow.xaml
    /// </summary>
    public partial class SourcePageWindow : Window
    {
        private const double YOUTUBE_PLAYER_WIDTH_OFFSET = 20.0;
        private const double YOUTUBE_PLAYER_HEIGHT_OFFSET = 20.0;
        Regex youtubeRegex = new Regex("youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");

        private enum NoteState
        {
            ADDING,
            MODIFYING,
            NONE
        }

        private SourcePageViewModel viewModel;
        private User loggedInUser;
        private Source source;
        private NoteState noteEditState = NoteState.NONE;
        private Notes? previousSelectedNote;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcePageWindow"/> class.
        /// </summary>
        /// <param name="loggedInUser">The logged in user.</param>
        /// <param name="source">The source.</param>
        public SourcePageWindow(User loggedInUser, Source source)
        {
            InitializeComponent();
            this.viewModel = new SourcePageViewModel();
            this.DataContext = this.viewModel;
            this.loggedInUser = loggedInUser;
            this.source = source;
            this.lblSourceTitle.Content = source.SourceName;
            this.noteEditState = NoteState.ADDING;
            this.previousSelectedNote = null;

            this.viewModel.PopulateNotesByID(source.SourceId);

            this.handleLoadingSource(source);

        }

        private void handleLoadingSource(Source source)
        {
            string sourceType = source.SourceType.ToUpper();
            switch (sourceType)
            {
                case "PDF":
                    string? tmpPDFDirectoryPath = SourceResources.ResourceDirectories.GetValueOrDefault("PDFDocuments");
                    if (tmpPDFDirectoryPath != null)
                    {
                        var tmpPDFDirectory = Directory.CreateDirectory(tmpPDFDirectoryPath);
                        string extension = "pdf";

                        string filename = System.IO.Path.Combine(tmpPDFDirectory.FullName, System.IO.Path.GetRandomFileName() + "." + extension);

                        File.WriteAllBytes(filename, source.Content);
                        this.pdfViewer.Visibility = Visibility.Visible;

                        this.pdfViewer.Navigate(filename);

                    }
                    break;

                case "VIDEO":
                    var youtubeLink = System.Text.Encoding.Default.GetString(source.Content);
                    string? youtubeId = LinkParser.ExtractYoutubeLinkID(youtubeLink);
                    if (youtubeId != null)
                    {
                        this.pdfViewer.Visibility = Visibility.Collapsed;
                        this.youtubePlayer.Visibility = Visibility.Visible;
                        this.loadYoutubeHTMLContent(youtubeId);
                    }
                    break;

                default:
                    Debug.WriteLine("Could not handle source type");
                    break;
            }

        }

        private async void loadYoutubeHTMLContent(string youtubeID)
        {
            var webEnv = await CoreWebView2Environment.CreateAsync(null, System.IO.Path.GetTempPath());
            await this.youtubePlayer.EnsureCoreWebView2Async(webEnv);

            string htmlContent = @"
            <html>
            <head>
            <!-- Include the YouTube iframe API script using HTTPS -->
            <script src='https://www.youtube.com/iframe_api'></script>
            </head>
            <body>
            <!-- Embed the YouTube video with enablejsapi parameter over HTTPS -->
            <iframe id='player' type='text/html'";
            htmlContent += $"width='{this.youtubePlayer.ActualWidth - YOUTUBE_PLAYER_WIDTH_OFFSET}' " +
                $"height='{this.youtubePlayer.ActualHeight - YOUTUBE_PLAYER_HEIGHT_OFFSET}'";
            htmlContent += "src ='";
            htmlContent += $"https://www.youtube.com/embed/{youtubeID}?enablejsapi=1";
            htmlContent += @"'
                frameborder='0' allow='fullscreen';></iframe>

            <!-- JavaScript code to handle fullscreen changes -->
            <script>
             // Initialize the YouTube iframe API when the script is loaded
            function onYouTubeIframeAPIReady() {
            player = new YT.Player('player', {
            events: {
                    'onReady': onPlayerReady,
                    'onStateChange': onPlayerStateChange
                    }
                });
            }

            function onPlayerReady(event) {
            // Player is ready
            // You can control playback and perform other actions here
            }

            function getCurrentTime() {
                return player.getCurrentTime();
            }

            function adjustPlayerSize(width, height) {
                player.setSize(width, height);
            }

            function onPlayerStateChange(event) {
            // Player state has changed (e.g., video started, paused, etc.)
            // Check if the player is in fullscreen mode
                var isFullscreen = document.fullscreenElement === player.getIframe();

                if (isFullscreen) {
            // Trigger the player's native fullscreen mode
                external.RequestFullscreen();
                } else {
            // Exit fullscreen
                external.ExitFullscreen();
             }
            }

            document.addEventListener('fullscreenchange', function () {
            console.log('Fullscreen change event triggered');
            window.chrome.webview.postMessage('fullscreenchange');
            });
            </script>
            </body>
            </html>
        ";
            this.youtubePlayer.NavigateToString(htmlContent);
        }

        private void btnBackHome_Click(object sender, RoutedEventArgs e)
        {
            Window HomeWindow = new HomeWindow(this.loggedInUser);
            HomeWindow.Show();
            if (this.youtubePlayer.Visibility == Visibility.Visible)
            {
                this.youtubePlayer.NavigateToString("");
            }
            this.Close();
        }

        private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.txtNoteBox.Text))
            {
                if (this.viewModel.InsertNewNote(this.source.SourceId))
                {
                    this.txtNoteBox.Text = string.Empty;
                }
            }
            else
            {
                MessageBoxResult errorBox = AlertDialog.AddNoteErrorBox();
            }

        }

        private void lstNotes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ListBox? lb = sender as ListBox;
            if (e.OriginalSource == lb && lb.SelectedItem != null)
            {
                lb.ScrollIntoView(lb.SelectedItem);

                if (lb.SelectedItem is Notes notes)
                {
                    var selectedItem = lb.SelectedItem;
                    switch (this.noteEditState)
                    {
                        case NoteState.ADDING:
                            this.handleAddState(lb, notes);
                            break;
                        case NoteState.MODIFYING:
                            this.handleModifyState(lb, notes);
                            break;

                        default:
                            return;
                    }
                }
            }
        }

        private void handleModifyState(ListBox lb, Notes notes)
        {
            if (this.previousSelectedNote != null &&
                this.previousSelectedNote != lb.SelectedItem &&
                !this.txtNoteBox.Text.Equals(this.previousSelectedNote.Content))
            {
                MessageBoxResult confirmBox = AlertDialog.EditNewNoteWithoutSavingConfirm();
                if (confirmBox == MessageBoxResult.Yes)
                {
                    this.previousSelectedNote = notes;
                    this.txtNoteBox.Text = notes.Content;
                    this.setModifyNoteButtonsVisibility(true);
                    this.noteEditState = NoteState.MODIFYING;
                }
                else
                {
                    lb.SelectedItem = this.previousSelectedNote;
                }
            }
            else if (this.previousSelectedNote != null &&
                this.previousSelectedNote != lb.SelectedItem &&
                this.txtNoteBox.Text.Equals(this.previousSelectedNote.Content))
            {
                this.previousSelectedNote = notes;
                this.txtNoteBox.Text = notes.Content;
                this.setModifyNoteButtonsVisibility(true);
                this.noteEditState = NoteState.MODIFYING;
            }
        }

        private void handleAddState(ListBox lb, Notes notes)
        {
            if (!string.IsNullOrWhiteSpace(this.txtNoteBox.Text))
            {
                MessageBoxResult confirmBox = AlertDialog.EditNewNoteWithoutSavingConfirm();
                if (confirmBox == MessageBoxResult.Yes)
                {
                    this.previousSelectedNote = notes;
                    this.txtNoteBox.Text = notes.Content;
                    this.setModifyNoteButtonsVisibility(true);
                    this.noteEditState = NoteState.MODIFYING;
                }
                else
                {
                    lb.SelectedItem = null;
                    this.setModifyNoteButtonsVisibility(false);
                }
            }
            else if (string.IsNullOrWhiteSpace(this.txtNoteBox.Text))
            {
                this.previousSelectedNote = notes;
                this.txtNoteBox.Text = notes.Content;
                this.setModifyNoteButtonsVisibility(true);
                this.noteEditState = NoteState.MODIFYING;
            }
        }

        private void setModifyNoteButtonsVisibility(bool state)
        {
            if (state)
            {
                this.btnAddNote.Visibility = Visibility.Collapsed;
                this.NoteModifyGrid.Visibility = Visibility.Visible;
                this.TagGrid.Visibility = Visibility.Visible;
                this.viewModel.PopulateTagsBySelectedNote();
            }
            else
            {
                this.btnAddNote.Visibility = Visibility.Visible;
                this.NoteModifyGrid.Visibility = Visibility.Collapsed;
                this.TagGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void resetNoteInputDisplay()
        {
            this.noteEditState = NoteState.ADDING;
            this.lstNotes.SelectedItem = null;
            this.setModifyNoteButtonsVisibility(false);
            this.txtNoteBox.Text = string.Empty;
        }

        private void btnCancelModify_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmBox = AlertDialog.QuitModifyingNoteConfirm();
            if (confirmBox == MessageBoxResult.Yes)
            {
                this.resetNoteInputDisplay();
            }
            else
            {
                this.lstNotes.SelectedItem = this.previousSelectedNote;
            }
        }

        private void btnUpdateNote_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtNoteBox.Text))
            {
                MessageBoxResult errorBox = AlertDialog.UpdateNoteWithEmptyTextErrorBox();
                return;
            }
            var selectedNote = this.lstNotes.SelectedItem as Notes;
            if (selectedNote != null)
            {
                MessageBoxResult confirmBox = AlertDialog.UpdateNoteConfirm();
                if (confirmBox == MessageBoxResult.Yes)
                {
                    this.viewModel.UpdateNoteContent(selectedNote, this.txtNoteBox.Text);
                }
            }
        }

        private void btnDeleteNote_Click(object sender, RoutedEventArgs e)
        {
            var selectedNote = this.lstNotes.SelectedItem as Notes;
            if (selectedNote != null)
            {
                MessageBoxResult confirmBox = AlertDialog.DeleteNoteConfirm();
                if (confirmBox == MessageBoxResult.Yes)
                {
                    this.viewModel.DeleteNote(selectedNote);
                    this.resetNoteInputDisplay();
                }
            }
        }

        private void youtubePlayer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.youtubePlayer.ExecuteScriptAsync($"adjustPlayerSize({this.youtubePlayer.ActualWidth - YOUTUBE_PLAYER_WIDTH_OFFSET},{this.youtubePlayer.ActualHeight - YOUTUBE_PLAYER_HEIGHT_OFFSET})");
        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtNewTag.Text))
            {
                MessageBoxResult errorBox = AlertDialog.TagEmptyErrorBox();
                return;
            }
            var selectedNote = this.viewModel.SelectedNoteProperty;
            if (selectedNote != null && NoteTagsDAL.isTagExistingUnderNote(this.txtNewTag.Text, selectedNote.NotesId))
            {
                MessageBoxResult errorBox = AlertDialog.TagUnderNoteAlreadyExists();
                return;
            }
            this.viewModel.InsertNewTag(this.txtNewTag.Text);
        }

        private void btnRemoveTag_Click(object sender, RoutedEventArgs e)
        {
            var selectedTag = sender as Control;
            if (selectedTag == null)
            {
                return;
            }
            var parent = selectedTag.Parent as StackPanel;
            if (parent == null)
            {
                return;
            }
            var tagObject = parent.FindName("btnTag") as Button;
            if (tagObject == null)
            {
                return;
            }
            this.viewModel.DeleteTagFromNote((string)tagObject.Content);
        }

        private void btnTags_Click(object sender, RoutedEventArgs e)
        {
            this.TagGrid.Visibility = Visibility.Visible;
        }
    }

}
