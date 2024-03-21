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


    }

    /// <summary>
    /// AddNoteModel
    /// </summary>
    public class AddNoteModel
    {
        public int SourceId { get; set; }
        public string Content { get; set; }
    }
}
