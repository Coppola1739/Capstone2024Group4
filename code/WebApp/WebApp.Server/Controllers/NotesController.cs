using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApp.Server.Data;
using WebApp.Server.Models;

namespace WebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly CapstoneDbContext _context;

        public NotesController(CapstoneDbContext context)
        {
            _context = context;
        }

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

                return Ok(new { Message = "Note added successfully", NoteId = note.NotesId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }


    }

    public class AddNoteModel
    {
        public int SourceId { get; set; }
        public string Content { get; set; }
    }
}
