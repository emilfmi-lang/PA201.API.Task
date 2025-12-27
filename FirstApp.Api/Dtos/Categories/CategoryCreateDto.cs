namespace FirstApp.Api.Dtos.Categories;

public class CategoryCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile File { get; set; }
}
