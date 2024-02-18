using System.Diagnostics.CodeAnalysis;

namespace WebApp.Server.Models
{
    [ExcludeFromCodeCoverage]
    public class User
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; }

        public ICollection<Source> Sources { get; set; }
    }
}
