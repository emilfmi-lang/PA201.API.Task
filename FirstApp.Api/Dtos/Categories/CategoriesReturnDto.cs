namespace FirstApp.Api.Dtos.Categories;

public class CategoriesReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public List<ProductInCategoryDto> Products { get; set; }
    public int PCount { get; set; }

}
public class ProductInCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; } 
}
