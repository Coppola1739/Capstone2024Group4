using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.Utilities
{
    /// <summary>
    /// The Character Checker Utilities class.
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public static class CharChecker
    {
        /// <summary>
        /// Determines whether the character is a special character.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <returns>
        ///   <c>true</c> if the character is a special character; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSpecialCharacter(char c)
        {
            return !(char.IsLetterOrDigit(c) || char.IsWhiteSpace(c));
        }
    }
}
