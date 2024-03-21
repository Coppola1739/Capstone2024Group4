using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Server.Data;
using WebApp.Server.Models;
using System.Text;

/// <summary>
/// 
/// </summary>
namespace WebApp.Server.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly CapstoneDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public FileController(CapstoneDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Uploads the video.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost("uploadvideo")]
        public async Task<IActionResult> UploadVideo([FromForm] VideoUploadModel model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.VideoLink))
                {
                    return BadRequest(new { Message = "Invalid video link" });
                }

                byte[] videoBytes = Encoding.UTF8.GetBytes(model.VideoLink);

                var source = new Source
                {
                    UserId = model.UserId,
                    SourceName = model.SourceName,
                    UploadDate = DateTime.UtcNow,
                    Content = videoBytes,
                    AuthorFirstName = model.AuthorFirstName,
                    AuthorLastName = model.AuthorLastName,
                    Title = model.Title,
                    SourceType = "video"
                };

                _context.Source.Add(source);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Video link uploaded successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }


        /// <summary>
        /// Uploads the PDF.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost("uploadpdf")]
        public async Task<IActionResult> UploadPdf([FromForm] FileUploadModel model)
        {
            try
            {
                if (model == null || model.PdfFile == null || model.PdfFile.Length == 0)
                {
                    return BadRequest(new { Message = "Invalid file" });
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await model.PdfFile.CopyToAsync(memoryStream);
                    var pdfContent = memoryStream.ToArray();

                    var source = new Source
                    {
                        UserId = model.UserId,
                        SourceName = model.SourceName,
                        UploadDate = DateTime.UtcNow,
                        Content = pdfContent,
                        AuthorFirstName = model.AuthorFirstName,
                        AuthorLastName = model.AuthorLastName,
                        Title = model.Title,
                        SourceType = model.SourceType
                    };

                    _context.Source.Add(source);
                    await _context.SaveChangesAsync();

                    return Ok(new { Message = "PDF uploaded successfully" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        /// <summary>
        /// Gets the users sources.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [HttpGet("GetUsersSources")]
        public async Task<IActionResult> GetUsersSources([FromQuery] int userId)
        {
            try
            {
                var userSources = await _context.Source
                    .Where(s => s.UserId == userId)
                    .ToListAsync();

                return Ok(new { Sources = userSources });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        /// <summary>
        /// Gets the source by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("GetSourceById")]
        public async Task<IActionResult> GetSourceById(int id)
        {
            try
            {
                var source = await _context.Source.FindAsync(id);

                if (source == null)
                {
                    return NotFound();
                }

                return Ok(source);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the source.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("DeleteSource/{id}")]
        public async Task<IActionResult> DeleteSource(int id)
        {
            try
            {
                var source = await _context.Source.FindAsync(id);
                if (source == null)
                {
                    return NotFound();
                }
                var notes = await _context.Notes.Where(n => n.SourceId == id).ToListAsync();
                _context.Notes.RemoveRange(notes);

                _context.Source.Remove(source);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Source and associated notes deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class FileUploadModel
    {
        public int UserId { get; set; }
        public IFormFile PdfFile { get; set; }
        public string SourceName { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Title { get; set; }
        public string SourceType { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoUploadModel
    {
        public int UserId { get; set; }
        public string VideoLink { get; set; }
        public string SourceName { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Title { get; set; }
        public string SourceType { get; set; }
    }
}
