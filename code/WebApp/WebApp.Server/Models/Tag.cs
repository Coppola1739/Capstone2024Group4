using System.Diagnostics.CodeAnalysis;
/// <summary>
/// API Models
/// </summary>
namespace WebApp.Server.Models
{
    /// <summary>
    /// Tag Class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Tag
    {

        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        /// <value>
        /// The name of the tag.
        /// </value>
        public string TagName { get; set; }
    }
}
