using Group4DesktopApp.Model;
using Group4DesktopApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for SignupWindow.xaml
    /// </summary>
    public partial class SignupWindow : Window
    {
        Regex VALIDUSERNAMEREGEX = new Regex("^[a-zA-Z0-9 ]*$");
        const int MIN_INPUT_CHARS = 3;
        private SignupViewModel viewModel = new SignupViewModel();

        public SignupWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            if (this.inputValidation())
            {
                User? user = this.viewModel.CreateAccount();
                if (user != null)
                {
                    Window HomeWindow = new HomeWindow(user);
                    HomeWindow.Show();
                    this.Close();
                }
                else
                {
                    this.showErrorMessage("Username already exists");
                }
            }
        }

        private bool inputValidation()
        {
            bool isInvalid = false;

            if (!isUserValid(this.txtUsername.Text))
            {
                isInvalid = true;
            }
            if (!isPassValid(this.txtPassword.Text))
            {
                isInvalid = true;
            }
            if(!isInvalid)
            {
                return true;
            }

            return false;
        }

        private bool isUserValid(string username)
        {
            if (String.IsNullOrWhiteSpace(username))
            {
                this.showUserInputErrorMessage("Username is required");
                return false;
            }
            if (username.Any(Char.IsWhiteSpace))
            {
                this.showUserInputErrorMessage("Username cannot contain whitespace");
                return false;
            }
            if (username.Length < MIN_INPUT_CHARS)
            {
                this.showUserInputErrorMessage($"Username must have {MIN_INPUT_CHARS} or more characters");
                return false;
            }
            if (!VALIDUSERNAMEREGEX.IsMatch(username))
            {
                this.showUserInputErrorMessage("Username cannot contain specical characters");
                return false;
            }
            return true;
        }

        private bool isPassValid(string pass)
        {
            if (String.IsNullOrWhiteSpace(pass))
            {
                this.showPassInputErrorMessage("Password is required");
                return false;
            }
            if (pass.Any(Char.IsWhiteSpace))
            {
                this.showPassInputErrorMessage("Password cannot contain whitespace");
                return false;
            }
            if (pass.Length < MIN_INPUT_CHARS)
            {
                this.showPassInputErrorMessage($"Password must have {MIN_INPUT_CHARS} or more characters");
                return false;
            }
            return true;
        }

        private void showUserInputErrorMessage(string message)
        {
            this.lblUserError.Content = message;
            this.lblUserError.Visibility = Visibility.Visible;
        }

        private void showPassInputErrorMessage(string message)
        {
            this.lblPassError.Content = message;
            this.lblPassError.Visibility = Visibility.Visible;
        }

        private void showErrorMessage(string message)
        {
            this.lblError.Content = message;
            this.lblError.Visibility = Visibility.Visible;
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lblUserError.Visibility= Visibility.Hidden;
            this.lblError.Visibility = Visibility.Hidden;
        }

        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lblPassError.Visibility = Visibility.Hidden;
            this.lblError.Visibility = Visibility.Hidden;
        }

        private void txtLoginLink_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Window LoginWindow = new LoginWindow();
            LoginWindow.Show();
            this.Close();
        }
    }
}
