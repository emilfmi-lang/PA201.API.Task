namespace FirstApp.Api.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public List<Product> Products { get; set; }
    public string ImageUrl { get; set; }
}
