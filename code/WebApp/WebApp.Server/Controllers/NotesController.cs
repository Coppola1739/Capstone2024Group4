using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            Debug.WriteLine("MY SOURCE ID IS = " + sourceId);
            Debug.WriteLine(_context.Notes);
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
    }
}