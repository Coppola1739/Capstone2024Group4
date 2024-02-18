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
    public  class SignupViewModel : INotifyPropertyChanged
    {
        private string username;
        private string password;
        //private EmployeeDAL employeeDAL;

        public SignupViewModel()
        {
            this.username = String.Empty;
            this.password = String.Empty;
        }

        public string UsernameProperty
        {
            get { return username; }
            set
            {
                username = value;
                NotifyPropertyChanged(UsernameProperty);
            }
        }

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
        /// Gets the employee with the currently entered username and password
        /// </summary>
        /// <returns></returns>
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

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
