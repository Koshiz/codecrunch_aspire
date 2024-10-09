using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CV_test01.AppHost
{

    [ApiController]
    [Route("api/[controller]")]
    public class CVSummarizerController : ControllerBase
    {
        private readonly IOpenAiService _openAiService;
        private readonly CvContext _context;

        public CVSummarizerController(IOpenAiService openAiService, CvContext context)
        {
            _openAiService = openAiService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SummarizeCV([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            // Store the file locally
            var filePath = Path.Combine("UploadedFiles", file.FileName);
            if (!Directory.Exists("UploadedFiles"))
            {
                Directory.CreateDirectory("UploadedFiles");
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Extract the text from the file (for simplicity, reading as plain text)
            string cvText = await ExtractTextFromFile(filePath);

            if (string.IsNullOrWhiteSpace(cvText))
            {
                return BadRequest("Unable to extract text from the file.");
            }

            // Call OpenAI API to summarize the CV text
            var summary = await _openAiService.SummarizeCV(cvText);

            // Save the summarized CV in the database
            var cv = new CVModel
            {
                FileName = file.FileName,
                Summary = summary,
                UploadDate = DateTime.UtcNow
            };

            _context.CVs.Add(cv);
            await _context.SaveChangesAsync();

            return Ok(summary);
        }

        private async Task<string> ExtractTextFromFile(string filePath)
        {
            // Placeholder for file text extraction (e.g., reading text or parsing PDFs)
            return await System.IO.File.ReadAllTextAsync(filePath);
        }
    }


}
