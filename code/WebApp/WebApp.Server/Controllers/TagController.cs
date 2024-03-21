namespace WebApp.Server.Controllers
{
    using global::WebApp.Server.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    namespace WebApp.Server.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class TagController : ControllerBase
        {
            private readonly CapstoneDbContext _context;

            public TagController(CapstoneDbContext context)
            {
                _context = context;
            }

            [HttpGet("GetTagByNotesID/{notesId:int}")]
            public async Task<IActionResult> GetTagByNotesID(int notesId)
            {
                try
                {
                    var tags = await _context.NoteTags
                        .Where(nt => nt.NotesId == notesId)
                        .Select(nt => nt.TagName)
                        .ToListAsync();

                    return Ok(tags);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
                }
            }
        }
    }

}
