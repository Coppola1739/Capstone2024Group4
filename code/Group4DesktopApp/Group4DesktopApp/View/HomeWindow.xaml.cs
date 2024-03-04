using Group4DesktopApp.DAL;
using Group4DesktopApp.Model;
using Group4DesktopApp.Utilities;
using Group4DesktopApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private User loggedInUser;
        private HomeViewModel viewModel;
        private string selectedType;
        private string chosenFilePath;

        public HomeWindow(User loggedInUser)
        {
            InitializeComponent();
            this.viewModel = new HomeViewModel();
            this.DataContext = this.viewModel;
            this.loggedInUser = loggedInUser;
            this.lblWelcome.Content = $"Hello, {loggedInUser.UserName}!";
            this.viewModel.PopulateSourcesByID(loggedInUser.UserId);
            this.selectedType = string.Empty;
            this.chosenFilePath = string.Empty;
        }

        private void btnViewSource_Click(object sender, RoutedEventArgs e)
        {
            Source? selectedSource = this.SourcesList.SelectedItem as Source;
            if (selectedSource != null)
            {
                SourcePageWindow sourcePageWindow = new SourcePageWindow(loggedInUser, selectedSource);
                sourcePageWindow.Show();
                this.Close();
            }
            Debug.WriteLine(this.SourcesList.Items.GetItemAt(0).ToString());
        }

        private void cmbSourceType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.uploadGrid.Visibility = Visibility.Visible;

            if (cmbSourceType.SelectedItem.Equals(SourceType.Enum.PDF.ToString()))
            {
                this.selectedType = SourceType.Enum.PDF.ToString();
                this.clearUploadFields();
                this.stackFileChoose.Visibility = Visibility.Visible;
                
            }
            else if (cmbSourceType.SelectedItem.Equals(SourceType.Enum.YOUTUBE_LINK.ToString()))
            {
                this.selectedType = SourceType.Enum.YOUTUBE_LINK.ToString();
                this.clearUploadFields();
                this.uploadGrid.Visibility = Visibility.Visible;
                this.youtubeGrid.Visibility = Visibility.Visible;
            }
            else
            {
                this.uploadGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void clearUploadFields()
        {
            this.stackFileChoose.Visibility = Visibility.Collapsed;
            this.youtubeGrid.Visibility = Visibility.Collapsed;
            this.btnCancel.Visibility = Visibility.Collapsed;
            this.stackMetaData.Visibility = Visibility.Collapsed;
            this.txtYoutubeUrl.IsEnabled = true;

            this.lblUploadedSource.Content = "No file chosen";
            this.clearMetaDataFields();
        }

        private void btnFileChoose_Click(object sender, RoutedEventArgs e)
        {
            if (this.selectedType == SourceType.Enum.PDF.ToString())
            {
                var dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.DefaultExt = ".pdf";
                dialog.Filter = "PDF Documents (.pdf)|*.pdf";
                bool? result = dialog.ShowDialog();
                if (result == true)
                {
                    this.chosenFilePath = dialog.FileName;
                    this.lblUploadedSource.Content = dialog.SafeFileName;
                    this.stackMetaData.Visibility = Visibility.Visible;

                }
            }
        }

        private void clearMetaDataFields()
        {
            this.chosenFilePath = String.Empty;
            this.txtSourceName.Text = String.Empty;
            this.txtAuthFirstName.Text = String.Empty;
            this.txtAuthLastName.Text = String.Empty;
            this.txtTitle.Text = String.Empty;
            this.lblSrcNameError.Visibility = Visibility.Collapsed;
            this.lblAuthFirstError.Visibility = Visibility.Collapsed;
            this.lblAuthLastError.Visibility = Visibility.Collapsed;
            this.lblTitleError.Visibility = Visibility.Collapsed;
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            if (this.inputValidation())
            {
                if(this.selectedType == SourceType.Enum.PDF.ToString())
                {
                    byte[] content = File.ReadAllBytes(this.chosenFilePath);
                    this.viewModel.InsertNewSource(this.loggedInUser.UserId, "PDF", content);

                    this.lblUploadedSource.Content = "No file chosen";
                    this.clearMetaDataFields();
                    this.stackMetaData.Visibility = Visibility.Collapsed;
                } else if(this.selectedType == SourceType.Enum.YOUTUBE_LINK.ToString())
                {
                    byte[] youtubeURLcontent = System.Text.Encoding.Default.GetBytes(this.txtYoutubeUrl.Text);
                    this.viewModel.InsertNewSource(this.loggedInUser.UserId, "video", youtubeURLcontent);
                    this.clearUploadFields();
                    this.stackMetaData.Visibility = Visibility.Collapsed;
                    this.youtubeGrid.Visibility = Visibility.Visible;
                }
               
            }
        }

        private bool inputValidation()
        {
            bool isInvalid = false;

            if (!isSourceNameValid(this.txtSourceName.Text))
            {
                isInvalid = true;
            }
            if (!isAuthorFirstNameValid(this.txtAuthFirstName.Text))
            {
                isInvalid = true;
            }
            if (!isAuthorLastNameValid(this.txtAuthLastName.Text))
            {
                isInvalid = true;
            }
            if (!isTitleValid(this.txtTitle.Text))
            {
                isInvalid = true;
            }
            if (!isInvalid)
            {
                return true;
            }

            return false;
        }

        private bool isSourceNameValid(string sourceName)
        {
            if (String.IsNullOrWhiteSpace(sourceName))
            {
                this.showInputFieldErrorMessage("Source Name is required", this.lblSrcNameError);
                return false;
            }

            if (sourceName.Length < 3)
            {
                this.showInputFieldErrorMessage($"Source Name must have {3} or more characters", this.lblSrcNameError);
                return false;
            }

            return true;
        }

        private bool isAuthorFirstNameValid(string authorFirst)
        {
            if (String.IsNullOrWhiteSpace(authorFirst))
            {
                this.showInputFieldErrorMessage("Author First Name is required", this.lblAuthFirstError);
                return false;
            }

            if (authorFirst.Length < 3)
            {
                this.showInputFieldErrorMessage($"Author First Name must have {3} or more characters", this.lblAuthFirstError);
                return false;
            }

            return true;
        }

        private bool isAuthorLastNameValid(string authorLast)
        {
            if (String.IsNullOrWhiteSpace(authorLast))
            {
                this.showInputFieldErrorMessage("Author Last Name is required", this.lblAuthLastError);
                return false;
            }

            if (authorLast.Length < 3)
            {
                this.showInputFieldErrorMessage($"Author Last Name must have {3} or more characters", this.lblAuthLastError);
                return false;
            }

            return true;
        }

        private bool isTitleValid(string title)
        {
            if (String.IsNullOrWhiteSpace(title))
            {
                this.showInputFieldErrorMessage("Title is required", this.lblTitleError);
                return false;
            }

            if (title.Length < 3)
            {
                this.showInputFieldErrorMessage($"Title must have {3} or more characters", this.lblTitleError);
                return false;
            }

            return true;
        }

        private void showInputFieldErrorMessage(string message, Label errorLabel)
        {
            errorLabel.Content = message;
            errorLabel.Visibility = Visibility.Visible;
        }

        private void txtSourceName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lblSrcNameError.Visibility = Visibility.Collapsed;
        }

        private void txtAuthFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lblAuthFirstError.Visibility = Visibility.Collapsed;
        }

        private void txtAuthLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lblAuthLastError.Visibility = Visibility.Collapsed;
        }

        private void txtTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lblTitleError.Visibility = Visibility.Collapsed;
        }

        private void SourcesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox? lb = sender as ListBox;
            if (e.OriginalSource == lb && lb.SelectedItem != null)
            {
                lb.ScrollIntoView(lb.SelectedItem);
                this.btnDeleteSource.Visibility = Visibility.Visible;
            }
            else
            {
                this.btnDeleteSource.Visibility = Visibility.Collapsed;
            }
        }

        private void btnDeleteSource_Click(object sender, RoutedEventArgs e)
        {
            var selectedSource = this.SourcesList.SelectedItem as Source;
            if (selectedSource != null)
            {
                MessageBoxResult confirmBox = AlertDialog.DeleteSourceConfirm();
                if (confirmBox == MessageBoxResult.Yes)
                {
                    this.viewModel.DeleteSource(selectedSource);
                    this.SourcesList.SelectedItem = null;
                }
            }
        }

        private void btnYoutubeUpload_Click(object sender, RoutedEventArgs e)
        {
            if(LinkParser.IsYoutubeLink(this.txtYoutubeUrl.Text)) {
                this.txtYoutubeUrl.IsEnabled = false;
                this.stackMetaData.Visibility = Visibility.Visible;
                this.btnCancel.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBoxResult confirmBox = AlertDialog.InvalidYoutubeLinkErrorBox();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.txtYoutubeUrl.IsEnabled = true;
            this.stackMetaData.Visibility = Visibility.Collapsed;
            this.btnCancel.Visibility = Visibility.Collapsed;
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
    }
}
