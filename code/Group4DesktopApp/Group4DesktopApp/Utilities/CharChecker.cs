using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.Utilities
{
    public static class CharChecker
    {
        public static bool IsSpecialCharacter(char c)
        {
            return !(char.IsLetterOrDigit(c) || char.IsWhiteSpace(c));
        }
    }
}
