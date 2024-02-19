using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.Model
{
    public class User
    {
        private int id;
        private string username;

        public int UserId => this.id;
        public string UserName => this.username;

        public User(int id, string username) { 
        
            this.id = id;
            this.username = username;
        }
    }
}
