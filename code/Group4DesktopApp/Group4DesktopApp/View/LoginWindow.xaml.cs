using Group4DesktopApp.Model;
using Group4DesktopApp.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Group4DesktopApp.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoginViewModel viewModel;

        public LoginWindow()
        {
            InitializeComponent();
            this.viewModel = new LoginViewModel();
            this.DataContext = viewModel;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (this.inputValidation())
            {
                User? user = this.viewModel.getUser();
                if (user != null)
                {
                    Window HomeWindow = new HomeWindow(user);
                    HomeWindow.Show();
                    this.Close();
                } else
                {
                    this.showErrorMessage("Incorrect username or password");
                }
            }
        }


        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Window signupWindow = new SignupWindow();
            signupWindow.Show();
            this.Close();
        }

        private bool inputValidation()
        {
            if (String.IsNullOrWhiteSpace(this.txtUsername.Text) || String.IsNullOrWhiteSpace(this.txtPassword.Text))
            {
                this.showErrorMessage("Username and Password required");
                return false;
            }
            return true;
        }

        private void showErrorMessage(string message)
        {
            this.lblError.Content = message;
            this.lblError.Visibility = Visibility.Visible;
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lblError.Visibility = Visibility.Hidden;
        }

        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lblError.Visibility = Visibility.Hidden;
        }
    }
}
