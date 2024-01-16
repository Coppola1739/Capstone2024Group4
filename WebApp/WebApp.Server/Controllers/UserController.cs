using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Server.Data;
using WebApp.Server.Models;

namespace WebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(CapstoneDbContext context) : ControllerBase
    {
        private readonly CapstoneDbContext _context = context;

        [HttpGet (Name = "GetUsers")]
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
    }
}
