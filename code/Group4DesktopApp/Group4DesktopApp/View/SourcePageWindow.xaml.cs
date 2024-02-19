using Group4DesktopApp.Model;
using Group4DesktopApp.ViewModel;
using System;
using System.Collections.Generic;
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
    }
}
