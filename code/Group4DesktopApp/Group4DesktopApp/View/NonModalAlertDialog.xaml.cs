using System.Windows;

namespace Group4DesktopApp.View
{
    /// <summary>
    /// Interaction logic for NonModalAlertDialog.xaml
    /// </summary>
    public partial class NonModalAlertDialog : Window
    {
        /// <summary>
        /// Gets or sets the alert message.
        /// </summary>
        /// <value>
        /// The alert message.
        /// </value>
        public string AlertMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonModalAlertDialog"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public NonModalAlertDialog(string message)
        {
            InitializeComponent();
            AlertMessage = message;
            DataContext = this;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
