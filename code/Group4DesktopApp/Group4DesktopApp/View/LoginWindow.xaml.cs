using Group4DesktopApp.DAL;
using Group4DesktopApp.Model;
using Group4DesktopApp.ViewModel;
using System;
using System.Diagnostics;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginWindow"/> class.
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();
            this.viewModel = new LoginViewModel();
            this.DataContext = viewModel;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            SourceDAL.GetAllSourcesByUserId(1);
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

        private void chboxShowPassword_Click(object sender, RoutedEventArgs e)
        {
            CheckBox? cb = sender as CheckBox;
            if (e.OriginalSource == cb && cb.IsChecked == true)
            {
                this.pboxPassword.Visibility = Visibility.Collapsed;
                this.txtPassword.Visibility = Visibility.Visible;

            }
            else if (e.OriginalSource == cb && cb.IsChecked == false)
            {
                this.pboxPassword.Visibility = Visibility.Visible;
                this.txtPassword.Visibility = Visibility.Collapsed;
            }
        }

        private void pboxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.txtPassword.Text = this.pboxPassword.Password;
        }
    }
}
