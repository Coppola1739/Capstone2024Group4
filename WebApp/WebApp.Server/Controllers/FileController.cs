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
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly CapstoneDbContext _context;

        public FileController(CapstoneDbContext context)
        {
            _context = context;
        }
        [HttpPost("uploadpdf")]
        public async Task<IActionResult> UploadPdf(IFormFile pdfFile)
        {
            try
            {
                if (pdfFile == null || pdfFile.Length == 0)
                {
                    return BadRequest(new { Message = "Invalid file" });
                }

                // Get authenticated user ID (replace this with your authentication logic)
                int userId = 1; // Replace with your logic to get the authenticated user ID

                // Read the PDF content into a byte array
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await pdfFile.CopyToAsync(memoryStream);
                    var pdfContent = memoryStream.ToArray();

                    // Create a new UserFile entity
                    var userFile = new UserFile
                    {
                        UserId = userId,
                        FileName = pdfFile.FileName,
                        UploadDate = DateTime.UtcNow,
                        // Save the PDF content as a byte array
                        FileContent = pdfContent,
                    };

                    // Save the UserFile entity to the database
                    _context.UserFiles.Add(userFile);
                    await _context.SaveChangesAsync();

                    return Ok(new { Message = "PDF uploaded successfully" });
                }
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

    }
}
