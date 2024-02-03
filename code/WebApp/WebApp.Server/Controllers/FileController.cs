using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Server.Data;
using WebApp.Server.Models;

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

        [HttpPost("uploadpdf")]
        public async Task<IActionResult> UploadPdf([FromForm] FileUploadModel model)
        {
            try
            {
                if (model == null || model.PdfFile == null || model.PdfFile.Length == 0)
                {
                    return BadRequest(new { Message = "Invalid file" });
                }

                int userId = 1; // Assuming you get the userId from the request or authentication

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
                        SourceType = model.SourceType // Set the sourceType from model
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
    }

    public class FileUploadModel
    {
        public IFormFile PdfFile { get; set; }
        public string SourceName { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Title { get; set; }
        public string SourceType { get; set; } // Add SourceType property
    }
}
