using ImageApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ImageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ImageService imageService;
        public ImagesController(ImageService imageService)
        {
            this.imageService = imageService;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            var imageId = await imageService.SaveImageAsync(file);
            return Ok(new { ImageId = imageId });   
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var image = await imageService.GetImageAsync(id);
            if (image == null)
                return NotFound();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", image.Name);
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(bytes, "image/jpeg");
        }
    }
}
