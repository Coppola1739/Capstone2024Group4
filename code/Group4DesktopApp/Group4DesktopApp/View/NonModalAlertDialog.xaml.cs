using System.Windows;

namespace Group4DesktopApp.View
{
    /// <summary>
    /// Interaction logic for NonModalAlertDialog.xaml
    /// </summary>
    public partial class NonModalAlertDialog : Window
    {
        public string AlertMessage { get; set; }

        public NonModalAlertDialog(string message)
        {
            InitializeComponent();
            AlertMessage = message;
            DataContext = this;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
