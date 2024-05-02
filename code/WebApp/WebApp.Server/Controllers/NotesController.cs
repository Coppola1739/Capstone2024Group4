using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApp.Server.Data;
using WebApp.Server.Models;

/// <summary>
/// API Controllers
/// </summary>
namespace WebApp.Server.Controllers
{
    /// <summary>
    /// Notes API Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly CapstoneDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="context">The DB context.</param>
        public NotesController(CapstoneDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the notes by source id.
        /// </summary>
        /// <param name="sourceId">The source id.</param>
        /// <returns>A list of notes if exist. Empty list if they dont</returns>
        [HttpGet("GetNotesBySourceId/{sourceId:int}")]
        public async Task<IActionResult> GetNotesBySourceId(int sourceId)
        {
            try
            {
                var notes = await _context.Notes
                    .Where(n => n.SourceId == sourceId)
                    .ToListAsync();

                return Ok(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        /// <summary>
        /// Updates the note
        /// </summary>
        /// <param name="noteId">The note Id.</param>
        /// <param name="updatedContent">Content of the updated note.</param>
        /// <returns>Ok if note updated, NotFound if note doesnt exist, Bad if network/context issues</returns>
        [HttpPost("UpdateNote/{noteId:int}")]
        public async Task<IActionResult> UpdateNote(int noteId, [FromBody] string updatedContent)
        {
            try
            {
                var note = await _context.Notes.FindAsync(noteId);
                if (note == null)
                {
                    return NotFound();
                }

                note.Content = updatedContent;
                _context.Entry(note).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Note updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteId">The note Id.</param>
        /// <returns>Ok if note deleted, NotFound if note doesnt exist, Bad if network/Context errors</returns>
        [HttpDelete("DeleteNote/{noteId:int}")]
        public async Task<IActionResult> DeleteNote(int noteId)
        {
            try
            {
                var note = await _context.Notes.FindAsync(noteId);
                if (note == null)
                {
                    return NotFound();
                }

                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Note deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        /// <summary>
        /// Adds the note.
        /// </summary>
        /// <param name="model">The AddNoteModel.</param>
        /// <returns>BadRequest if model is null, Ok if note added, Bad if network/Context errors</returns>
        [HttpPost("AddNote")]
        public async Task<IActionResult> AddNote([FromForm] AddNoteModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { Message = "Invalid data" });
                }

                var note = new Notes
                {
                    SourceId = model.SourceId,
                    Content = model.Content
                };

                _context.Notes.Add(note);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Note added successfully"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }


        /// <summary>
        /// Gets the notes by tag.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns></returns>
        [HttpGet("GetNotesByTag/{tagName}")]
        public async Task<IActionResult> GetNotesByTag(string tagName)
        {
            try
            {
                tagName = tagName.ToLower();

                var noteIds = await _context.NoteTags
                    .Where(nt => nt.TagName.ToLower() == tagName)
                    .Select(nt => nt.NotesId)
                    .ToListAsync();

                var notes = await _context.Notes
                    .Where(n => noteIds.Contains(n.NotesId))
                    .ToListAsync();

                return Ok(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        /// <summary>
        /// Gets notes by applied filters under the specified userId.
        /// </summary>
        /// <param name="appliedFilters">List of tags to filter notes.</param>
        /// <param name="userId">The userId to search for the notes under.</param>
        /// <returns>List of notes that match all applied filters.</returns>
        [HttpPost("GetNotesByTags")]
        public async Task<IActionResult> GetNotesByTags([FromBody] List<string> appliedFilters, string userId)
        {
            try
            {
                appliedFilters = appliedFilters.Select(filter => filter.ToLower()).ToList();

                int user = int.Parse(userId);

                var sources = await _context.Source
                    .Where(s => s.UserId == user)
                    .Select(s => s.SourceId)
                    .ToListAsync();

                var noteIds = await _context.NoteTags
                    .Where(nt => appliedFilters.Contains(nt.TagName.ToLower()))
                    .GroupBy(nt => nt.NotesId)
                    .Where(group => group.Count() == appliedFilters.Count)
                    .Select(group => group.Key)
                    .ToListAsync();

                var notes = await _context.Notes
                    .Where(note => noteIds.Contains(note.NotesId))
                    .Where(note => sources.Contains(note.SourceId))
                    .ToListAsync();

                return Ok(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        /// <summary>
        /// Gets the source by note ID.
        /// </summary>
        /// <param name="noteId">The note ID.</param>
        /// <returns>The source associated with the note.</returns>
        [HttpGet("GetSourceByNoteId/{noteId:int}")]
        public async Task<IActionResult> GetSourceByNoteId(int noteId)
        {
            try
            {
                var source = await _context.Source
                    .Where(s => s.SourceId == _context.Notes.FirstOrDefault(n => n.NotesId == noteId).SourceId)
                    .FirstOrDefaultAsync();

                if (source == null)
                {
                    return NotFound(new { Message = "Source not found for the provided note ID" });
                }

                return Ok(source);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
    }

}


    /// <summary>
    /// AddNoteModel
    /// </summary>
    public class AddNoteModel
    {
        public int SourceId { get; set; }
        public string Content { get; set; }
    }