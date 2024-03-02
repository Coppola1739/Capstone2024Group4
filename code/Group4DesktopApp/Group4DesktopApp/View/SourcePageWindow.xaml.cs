using Group4DesktopApp.Model;
using Group4DesktopApp.ViewModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;


namespace Group4DesktopApp.View
{
    /// <summary>
    /// Interaction logic for SourcePageWindow.xaml
    /// </summary>
    public partial class SourcePageWindow : Window
    {
        private SourcePageViewModel viewModel;
        private User loggedInUser;
        private Source source;

        public SourcePageWindow(User loggedInUser, Source source)
        {
            InitializeComponent();
            this.viewModel = new SourcePageViewModel();
            this.DataContext = this.viewModel;
            this.loggedInUser = loggedInUser;
            this.source = source;
            this.lblSourceTitle.Content = source.Title;

            this.viewModel.PopulateNotesByID(source.SourceId);

            string extension = "pdf";

            string filename = System.IO.Path.GetTempFileName() + "." + extension;

            File.WriteAllBytes(filename, source.Content);

            this.pdfViewer.Navigate(filename);
        }

        private void btnBackHome_Click(object sender, RoutedEventArgs e)
        {
            Window HomeWindow = new HomeWindow(this.loggedInUser);
            HomeWindow.Show();
            this.Close();
        }

        private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(this.txtNoteBox.Text)) {
                this.viewModel.InsertNewNote(this.source.SourceId);
                this.txtNoteBox.Text = string.Empty;
            } else
            {
                MessageBoxResult errorBox = System.Windows.MessageBox.Show("Note must not be empty", "Note Add Failed", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
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
                    this.txtNoteBox.Text = notes.Content;
                    this.setModifyNoteButtonsVisibility(true);
                }
            }
            else
            {
                this.setModifyNoteButtonsVisibility(false);
                this.txtNoteBox.Text = string.Empty;
            }
        }
        private void setModifyNoteButtonsVisibility(bool state)
        {
            if (state)
            {
                this.btnAddNote.Visibility = Visibility.Collapsed;
                this.NoteModifyGrid.Visibility = Visibility.Visible;
            }
            else
            {
                this.btnAddNote.Visibility = Visibility.Visible;
                this.NoteModifyGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void btnCancelModify_Click(object sender, RoutedEventArgs e)
        {
            this.lstNotes.SelectedItem = null;
            this.setModifyNoteButtonsVisibility(false);
        }
    }
}
