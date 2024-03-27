using System.Diagnostics.CodeAnalysis;
/// <summary>
/// API Models
/// </summary>
namespace WebApp.Server.Models
{
    /// <summary>
    /// User class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class User
    {

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the sources.
        /// </summary>
        /// <value>
        /// The sources.
        /// </value>
        public ICollection<Source> Sources { get; set; }
    }
}
