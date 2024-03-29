using Group4DesktopApp.DAL;
using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.ViewModel
{
    /// <summary>
    /// The SignUp Window ViewModel
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class SignupViewModel : INotifyPropertyChanged
    {
        private string username;
        private string password;
        //private EmployeeDAL employeeDAL;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignupViewModel"/> class.
        /// </summary>
        public SignupViewModel()
        {
            this.username = String.Empty;
            this.password = String.Empty;
        }

        /// <summary>
        /// Gets or sets the username property.
        /// </summary>
        /// <value>
        /// The username property.
        /// </value>
        public string UsernameProperty
        {
            get { return username; }
            set
            {
                username = value;
                NotifyPropertyChanged(UsernameProperty);
            }
        }
        /// <summary>
        /// Gets or sets the password property.
        /// </summary>
        /// <value>
        /// The password property.
        /// </value>
        public string PasswordProperty
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged(nameof(PasswordProperty));
            }
        }

        /// <summary>
        /// Calls the Data Access Layer to create and return a User with the currently entered username and password, null if failed.
        /// </summary>
        /// <returns>a User with the currently entered username and password, null if failed.</returns>
        public User? CreateAccount()
        {
            bool isAccountCreated = AccountDAL.CreateAccount(UsernameProperty, PasswordProperty);
            if (isAccountCreated)
            {
                int? accID = AccountDAL.GetAccountID(UsernameProperty, PasswordProperty);
                if (accID == null)
                {
                    return null;
                }
                User? user = UserDAL.GetUserByID(accID.Value);
                return user;
            }
            return null;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
