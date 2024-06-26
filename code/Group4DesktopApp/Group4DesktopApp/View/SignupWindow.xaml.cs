﻿using Group4DesktopApp.Model;
using Group4DesktopApp.Utilities;
using Group4DesktopApp.ViewModel;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Group4DesktopApp.View
{
    /// <summary>
    /// Interaction logic for SignupWindow.xaml
    /// </summary>
    public partial class SignupWindow : Window
    {
        Regex VALIDUSERNAMEREGEX = new Regex("^[a-zA-Z0-9 ]*$");
        Regex VALID_SPECIAL_CHAR = new Regex("[!@$%^&*()]+");
        const int MIN_INPUT_CHARS = 6;
        private SignupViewModel viewModel = new SignupViewModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="SignupWindow"/> class.
        /// </summary>
        public SignupWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            this.lblUserError.Content = string.Empty;
            this.lblUserError.Content += "Username is required\n";
            this.lblUserError.Content += "Username cannot contain whitespace\n";
            this.lblUserError.Content += $"Username must have {MIN_INPUT_CHARS} or more characters\n";
            this.lblUserError.Content += "Username cannot contain specical characters";

            this.lblPassError.Content = string.Empty;
            this.lblPassError.Content += "Password is required\n";
            this.lblPassError.Content += "Password cannot contain whitespace\n";
            this.lblPassError.Content += $"Password must have {MIN_INPUT_CHARS} or more characters\n";
            this.lblPassError.Content += "Password must have at least one uppercase letter\n";
            this.lblPassError.Content += "Password must have at least one of the following symbols: !@$%^&*()";

            this.lblPassCopyError.Content = string.Empty;
            this.lblPassCopyError.Visibility = Visibility.Visible;
            this.lblPassCopyError.Content += "Passwords must match";


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
            if (!isPassCopyValid(this.txtPassword.Text,this.txtCopyPassword.Text))
            {
                isInvalid = true;
            }
            if (!isInvalid)
            {
                return true;
            }

            return false;
        }

        private bool isUserValid(string username)
        {
            string errors = string.Empty;
            bool isInvalid = false;
            if (String.IsNullOrWhiteSpace(username))
            {
                isInvalid = true;
                errors += "Username is required\n";
            }
            if (username.Any(Char.IsWhiteSpace))
            {
                isInvalid = true;
                errors += "Username cannot contain whitespace\n";
            }
            if (username.Length < MIN_INPUT_CHARS)
            {
                isInvalid = true;
                errors += $"Username must have {MIN_INPUT_CHARS} or more characters\n";
            }
            if (!VALIDUSERNAMEREGEX.IsMatch(username))
            {
                isInvalid = true;
                errors += "Username cannot contain specical characters";
            }
            if (isInvalid)
            {
                this.showInputFieldErrorMessage(errors, this.lblUserError);
                return false;
            }
            return true;
        }

        private bool isPassValid(string pass)
        {
            string errors = string.Empty;
            bool isInvalid = false;

            if (String.IsNullOrWhiteSpace(pass))
            {
                isInvalid = true;
                errors += "Password is required\n";
            }
            if (pass.Any(Char.IsWhiteSpace))
            {
                isInvalid = true;
                errors += "Password cannot contain whitespace\n";
            }
            if (pass.Length < MIN_INPUT_CHARS)
            {
                isInvalid = true;
                errors += $"Password must have {MIN_INPUT_CHARS} or more characters\n";
            }
            if (!pass.Any(char.IsUpper))
            {
                isInvalid = true;
                errors += "Password must have at least one uppercase letter\n";
            }
            if (!isTextContainingValidSpecialChar(pass))
            {
                isInvalid = true;
                errors += "Password must have at least one of the following symbols: !@$%^&*()";
            }
            if (isInvalid)
            {
                this.showInputFieldErrorMessage(errors, this.lblPassError);
                return false;
            }
            return true;
        }

        private bool isTextContainingValidSpecialChar(string text)
        {
            if(VALID_SPECIAL_CHAR.IsMatch(text))
            {
                return true;
            }
            return false;
        }

        private bool isPassCopyValid(string original, string copy)
        {
            string errors = string.Empty;
            bool isInvalid = false;

            if (String.IsNullOrWhiteSpace(copy))
            {
                isInvalid = true;
                errors += "Password Copy is required\n";
            }
            if (!original.Equals(copy))
            {
                isInvalid = true;
                errors += "Passwords do not match\n";
            }
            if (isInvalid)
            {
                this.showInputFieldErrorMessage(errors, this.lblPassCopyError);
                return false;
            }
            return true;
        }

        private void showInputFieldErrorMessage(string message, Label errorLabel)
        {
            errorLabel.Content = message;
            errorLabel.Visibility = Visibility.Visible;
        }

        private void showErrorMessage(string message)
        {
            this.lblError.Content = message;
            this.lblError.Visibility = Visibility.Visible;
        }

        private void txtLoginLink_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Window LoginWindow = new LoginWindow();
            LoginWindow.Show();
            this.Close();
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

        private void chboxCopyShowPassword_Click(object sender, RoutedEventArgs e)
        {
            CheckBox? cb = sender as CheckBox;
            if (e.OriginalSource == cb && cb.IsChecked == true)
            {
                this.pboxCopyPassword.Visibility = Visibility.Collapsed;
                this.txtCopyPassword.Visibility = Visibility.Visible;

            }
            else if (e.OriginalSource == cb && cb.IsChecked == false)
            {
                this.pboxCopyPassword.Visibility = Visibility.Visible;
                this.txtCopyPassword.Visibility = Visibility.Collapsed;
            }
        }

        private void pboxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.txtPassword.Text = this.pboxPassword.Password;
        }

        private void pboxCopyPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.txtCopyPassword.Text = this.pboxCopyPassword.Password;
        }
    }
}
