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

        public SourcePageWindow(User loggedInUser, Source source)
        {
            InitializeComponent();
            this.viewModel = new SourcePageViewModel();
            this.DataContext = this.viewModel;
            this.loggedInUser = loggedInUser;
            this.source = source;
            this.lblSourceTitle.Content = source.Title;
            this.noteEditState = NoteState.ADDING;
            this.previousSelectedNote = null;

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
            if (!string.IsNullOrWhiteSpace(this.txtNoteBox.Text))
            {
                this.viewModel.InsertNewNote(this.source.SourceId);
                this.txtNoteBox.Text = string.Empty;
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
                            this.handleModifyState(lb,notes);
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
            }
            else
            {
                this.btnAddNote.Visibility = Visibility.Visible;
                this.NoteModifyGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void btnCancelModify_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmBox = AlertDialog.QuitModifyingNoteConfirm();
            if (confirmBox == MessageBoxResult.Yes)
            {
                this.noteEditState = NoteState.ADDING;
                this.lstNotes.SelectedItem = null;
                this.setModifyNoteButtonsVisibility(false);
                this.txtNoteBox.Text = string.Empty;
            }
            else
            {
                this.lstNotes.SelectedItem = this.previousSelectedNote;
            }
        }
    }

}
