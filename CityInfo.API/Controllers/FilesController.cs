using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider fileExtensionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            this.fileExtensionContentTypeProvider = fileExtensionContentTypeProvider ?? throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider));
        }

        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            // look up the actual file, depending on the fileId.
            // demo code.
            var pathToFile = "test.pdf";

            // check if the file exists
            if (!System.IO.File.Exists(pathToFile))
                return NotFound();

            if (!fileExtensionContentTypeProvider.TryGetContentType(pathToFile, out var contentType))
                contentType = "application/octet-stream";

            var bytes = System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, contentType, Path.GetFileName(pathToFile));
        }

        [HttpPost]
        public async Task<ActionResult> CreateFile(IFormFile file)
        {
            // Validation
            if (file.Length == 0 || file.Length > 20971520 || file.ContentType != "application/pdf")
                return BadRequest("No file or an invalid one has been inputted.");

            // Usually saved not in the same directory with the project. It is stored in a directory without execute privileges (*.exe files).
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"uploaded_file_{Guid.NewGuid()}.pdf");

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("Your file has been uploaded successfully");
        }
    }
}
