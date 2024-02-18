using Group4DesktopApp.Model;
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

        public HomeWindow(User loggedInUser)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;
            this.lblWelcome.Content = $"Hello, {loggedInUser.UserName}!";
        }
    }
}
