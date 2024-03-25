using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.Model
{
    /// <summary>
    /// The User class.
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public class User
    {
        private int id;
        private string username;

        /// <summary>
        /// Gets the user ID.
        /// </summary>
        /// <value>
        /// The user ID.
        /// </value>
        public int UserId => this.id;
        /// <summary>
        /// Gets the user name.
        /// </summary>
        /// <value>
        /// The user name.
        /// </value>
        public string UserName => this.username;

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="username">The username.</param>
        public User(int id, string username) { 
        
            this.id = id;
            this.username = username;
        }
    }
}
