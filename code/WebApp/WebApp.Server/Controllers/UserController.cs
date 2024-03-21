using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Server.Data;
using WebApp.Server.Models;
/// <summary>
/// API Controllers
/// </summary>
namespace WebApp.Server.Controllers
{
    /// <summary>
    /// User API Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class UserController(CapstoneDbContext context) : ControllerBase
    {
        private readonly CapstoneDbContext _context = context;

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>List of all users</returns>
        [HttpGet ("getallusers")]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        /// <summary>
        /// Gets the user id by login information
        /// </summary>
        /// <param name="user">The username.</param>
        /// <param name="pass">The password.</param>
        /// <returns>NotFound if user doesnt exist, list of users that match, Bad if context/network errors</returns>
        [HttpGet("getUserIdByLogin")]
        public async Task<IActionResult> getUserIdByLogin(string? user, string? pass)
        {
            try
            {
                if (user == null || pass == null)
                {
                    return NotFound();
                }
                var foundUser = await _context.Users
                    .Where(s => s.Username == user).Where(s => s.Password == pass)
                    .ToListAsync();

                if (foundUser == null)
                {
                    return NotFound();
                }
                if (foundUser != null && foundUser.Count <= 0)
                {
                    return NotFound();
                }
                return Ok(foundUser[0].UserId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        /// <summary>
        /// Creates the account of the user
        /// </summary>
        /// <param name="model">The UserModel</param>
        /// <returns>BadRequest if invalid credentials, Conflict if username already exists, Ok if created, Bad if network/context errors</returns>
        [HttpPost("createAccount")]
        public async Task<IActionResult> CreateAccount([FromForm] UserModel model)
        {
            try
            {
                if (model == null || string.IsNullOrWhiteSpace(model.userName) || string.IsNullOrWhiteSpace(model.passWord))
                {
                    return BadRequest(new { Message = "Invalid credentials" });
                }

                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.userName);
                if (existingUser != null)
                {
                    return Conflict(new { Message = "Username already exists" });
                }

                var user = new User
                {
                    Username = model.userName,
                    Password = model.passWord,
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Account successfully created!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

    }

    /// <summary>
    /// UserModel class
    /// </summary>
    public class UserModel
    {
        public string userName { get; set; }
        public string passWord { get; set; }
    }
}
