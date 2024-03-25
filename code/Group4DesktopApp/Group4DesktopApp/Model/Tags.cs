using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.Model
{
    public class Tags
    {
        private string tagName;
        public string TagName
        {
            get { return tagName; }
            set
            {
                tagName = value;
            }
        }
        public Tags()
        {
            this.tagName = string.Empty;
        }
        public Tags(string tagName)
        {
            this.tagName = tagName;
        }
    }
}
