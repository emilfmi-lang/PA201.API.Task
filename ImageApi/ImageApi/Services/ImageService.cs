using ImageApi.Data;
using ImageApi.Interface;
using ImageApi.Models;

namespace ImageApi.Services;

public class ImageService : IImageService
{
    private readonly AppDbContext _db;
    private readonly string _uploadFolder;
    public ImageService(AppDbContext db)
    {
        _db = db;
        _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        if (!Directory.Exists(_uploadFolder))
            Directory.CreateDirectory(_uploadFolder);
    }
    public async Task<Image> GetImageAsync(string id)
    {
        var file = await _db.Images.FindAsync(id);
        if (file == null)
            return null;
        return file;
    }

    public async Task<string> SaveImageAsync(IFormFile file)
    {
        if(file == null || file.Length == 0)
            throw new ArgumentException("File is null or empty", nameof(file));
        var id = Guid.NewGuid().ToString();
        var extension = Path.GetExtension(file.FileName).ToLower();
        var fileName = $"{id}{extension}";
        var filePath = Path.Combine(_uploadFolder, fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        var image = new Image
        {
            Id = id,
            Name = fileName,
            OriginalName = file.FileName
        };
        _db.Images.Add(image);
        await _db.SaveChangesAsync();
        return id;
    }
}
