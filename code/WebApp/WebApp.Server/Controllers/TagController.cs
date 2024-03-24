using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Server.Data;
using WebApp.Server.Models;

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

        [HttpPost("AddTag")]
        public async Task<IActionResult> AddTag([FromBody] NoteTags model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.TagName))
                {
                    return BadRequest(new { Message = "Invalid tag data" });
                }

                var existingTag = await _context.NoteTags.FirstOrDefaultAsync(nt => nt.NotesId == model.NotesId && nt.TagName == model.TagName);
                if (existingTag != null)
                {
                    return BadRequest(new { Message = "Tag already exists for the note" });
                }

                var noteTag = new NoteTags()
                {
                    NotesId = model.NotesId,
                    TagName = model.TagName
                };

                _context.NoteTags.Add(noteTag);
                await _context.SaveChangesAsync();


                var existingTagName = await _context.Tags.FirstOrDefaultAsync(tg => tg.TagName == model.TagName);
                if (existingTagName == null)
                {
                    var tag = new Tag()
                    {
                        TagName = model.TagName,
                    };
                    _context.Tags.Add(tag);
                    await _context.SaveChangesAsync();
                }
               

                return Ok(new { Message = "Tag added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        [HttpDelete("RemoveTag")]
        public async Task<IActionResult> RemoveTag([FromBody] string tagName, int notesId)
        {
            try
            {
                if (string.IsNullOrEmpty(tagName))
                {
                    return BadRequest(new { Message = "Invalid tag name" });
                }

                var tagToRemove = await _context.NoteTags.FirstOrDefaultAsync(nt => nt.NotesId == notesId && nt.TagName == tagName);
                if (tagToRemove == null)
                {
                    return NotFound(new { Message = "Tag not found for the note" });
                }

                _context.NoteTags.Remove(tagToRemove);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Tag removed successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

    }
}
