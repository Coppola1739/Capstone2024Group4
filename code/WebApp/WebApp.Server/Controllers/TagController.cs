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
        /// <summary>
        /// Gets the tag by notes id
        /// </summary>
        /// <param name="notesId">the notes id</param>
        /// <returns>a list of tags that belong to a note, 500 if network/db error</returns>
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
        /// <summary>
        /// Adds the tag to the note and also adds the tag to the db if it doesnt already exist
        /// </summary>
        /// <param name="model">The NoteTags model</param>
        /// <returns>Ok if tag was added, BadRequest if NoteTags was null, empty or tag already exists for note</returns>
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
        /// <summary>
        /// Removes the tag based on the tag name and the id of the note
        /// </summary>
        /// <param name="tagName">The AddNoteModel.</param>
        /// <param name="notesId">The noteId.</param>
        /// <returns>BadRequest if tagname is null or empty, NotFound if the note doesnt exist, OK if tag is removed</returns>
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
        [HttpGet("SearchTags")]
        public async Task<IActionResult> SearchTags(string query)
        {
            try
            {
                if (string.IsNullOrEmpty(query))
                {
                    return BadRequest(new { Message = "Invalid search query" });
                }

                // Search for tags that contain the query string (case-insensitive)
                var tags = await _context.Tags
                    .Where(t => EF.Functions.Like(t.TagName, $"%{query}%"))
                    .Select(t => t.TagName)
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
