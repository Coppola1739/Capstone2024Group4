
/// <summary>
/// 
/// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        [ApiController]
        [Route("[controller]")]
        public class TagController : ControllerBase
        {
            private readonly CapstoneDbContext _context;

            /// <summary>
            /// Initializes a new instance of the <see cref="TagController" /> class.
            /// </summary>
            /// <param name="context">The context.</param>
            public TagController(CapstoneDbContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Gets the tag by notes identifier.
            /// </summary>
            /// <param name="notesId">The notes identifier.</param>
            /// <returns></returns>
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
