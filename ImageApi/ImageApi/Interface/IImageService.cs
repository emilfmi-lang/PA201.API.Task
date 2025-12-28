using ImageApi.Models;

namespace ImageApi.Interface;

public interface IImageService
{
    Task<string> SaveImageAsync(IFormFile file);
    Task<Image> GetImageAsync(string id);
}
