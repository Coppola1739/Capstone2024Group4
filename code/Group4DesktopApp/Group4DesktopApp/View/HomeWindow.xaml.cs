using Group4DesktopApp.Model;
using Group4DesktopApp.ViewModel;
using System;
using System.Collections.Generic;
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

        public HomeWindow(User loggedInUser)
        {
            InitializeComponent();
            this.viewModel = new HomeViewModel();
            this.DataContext = this.viewModel;
            this.loggedInUser = loggedInUser;
            this.lblWelcome.Content = $"Hello, {loggedInUser.UserName}!";
            this.viewModel.PopulateSourcesByID(loggedInUser.UserId);
        }

        private void btnViewSource_Click(object sender, RoutedEventArgs e)
        {
            Source? selectedSource = this.SourcesList.SelectedItem as Source;
            if(selectedSource != null )
            {
                SourcePageWindow sourcePageWindow = new SourcePageWindow(loggedInUser, selectedSource);
                sourcePageWindow.Show();
                this.Close();
            }
        }
    }
}
