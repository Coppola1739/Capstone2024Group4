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

namespace WebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly CapstoneDbContext _context;

        public FileController(CapstoneDbContext context)
        {
            _context = context;
        }

        [HttpPost("uploadvideo")]
        public async Task<IActionResult> UploadVideo([FromForm] VideoUploadModel model)
        {
            Console.WriteLine(model.VideoLink);
            try
            {
                if (model == null || string.IsNullOrEmpty(model.VideoLink))
                {
                    return BadRequest(new { Message = "Invalid video link" });
                }

                byte[] videoBytes = Encoding.UTF8.GetBytes(model.VideoLink);
                int userId = 1;

                var source = new Source
                {
                    UserId = userId,
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


        [HttpPost("uploadpdf")]
        public async Task<IActionResult> UploadPdf([FromForm] FileUploadModel model)
        {
            try
            {
                if (model == null || model.PdfFile == null || model.PdfFile.Length == 0)
                {
                    return BadRequest(new { Message = "Invalid file" });
                }

                int userId = 1;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await model.PdfFile.CopyToAsync(memoryStream);
                    var pdfContent = memoryStream.ToArray();

                    var source = new Source
                    {
                        UserId = userId,
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

        [HttpGet("GetUsersSources")]
        public async Task<IActionResult> GetUsersSources()
        {
            Debug.WriteLine("Im in file");
            try
            {
                // Get user id from authentication or request
                int userId = 1;

                // Retrieve sources belonging to the user
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
    }

    public class FileUploadModel
    {
        public IFormFile PdfFile { get; set; }
        public string SourceName { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Title { get; set; }
        public string SourceType { get; set; }
    }

    public class VideoUploadModel
    {
        //public int UserId { get; set; }
        public string VideoLink { get; set; }
        public string SourceName { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Title { get; set; }
        public string SourceType { get; set; }
    }
}
