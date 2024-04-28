using Group4DesktopApp.DAL;
using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.ViewModel
{
    /// <summary>
    /// The Login Window ViewModel
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string username;
        private string password;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        public LoginViewModel()
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
        /// Gets the User with the currently entered username and password
        /// </summary>
        /// <returns>the User with the currently entered username and password, null if not found.</returns>
        public User? getUser()
        {
            int? accID = AccountDAL.GetAccountID(UsernameProperty, PasswordProperty);
            if (accID == null)
            {
                Debug.WriteLine(PasswordProperty);
                return null;
            }
            User? user = UserDAL.GetUserByID(accID.Value);
            return user;
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
