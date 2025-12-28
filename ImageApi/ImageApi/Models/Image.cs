namespace ImageApi.Models;

public class Image
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string OriginalName { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
