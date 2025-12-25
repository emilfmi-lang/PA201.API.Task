
using FirstApp.Api.Data;
using FirstApp.Api.Dtos.Products;
using FirstApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(AppDbContext appDbContext) : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        var products = appDbContext.Products
            .Include(x => x.Category)
            .ToList();
        List<ProductReturnDto> productsReturnDto = new List<ProductReturnDto>();
        foreach (var product in products)
        {
            ProductReturnDto productDto = new ProductReturnDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CreatedDate = product.CreatedDate,
                UpdatedDate = product.UpdatedDate,
                Category = new CategoryInProductReturnDto
                {
                    Name = product.Category.Name,
                    Description = product.Category.Description
                }
            };
            productsReturnDto.Add(productDto);
        }
        return Ok(productsReturnDto);
    }
    [HttpPost]
    public IActionResult Post([FromBody] Product product)
    {
        appDbContext.Products.Add(product);
        appDbContext.SaveChanges();
        return StatusCode(StatusCodes.Status201Created);
    }
    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        var product = appDbContext.Products
            .Include(x => x.Category)
            .FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();
        ProductReturnDto productDto = new ()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            CreatedDate = product.CreatedDate,
            UpdatedDate = product.UpdatedDate,
            Category = new CategoryInProductReturnDto
            {
                Name = product.Category.Name,
                Description = product.Category.Description
            }
        };
        return Ok(productDto);
    }
    [HttpPut("{id}")]
    public IActionResult Put(Product product)
    {
        var existingProduct = appDbContext.Products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct == null)
            return NotFound();
        if(!appDbContext.Categories.Any(c => c.Id == product.CategoryId))
            return BadRequest();
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.CategoryId = product.CategoryId;
        appDbContext.SaveChanges();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = appDbContext.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();
        appDbContext.Products.Remove(product);
        appDbContext.SaveChanges();
        return NoContent();
    }

    //product bulky creation
    [HttpPost("bulk")]
    public IActionResult PostBulky([FromBody] List<Product> products)
    {
        foreach (var product in products)
        {
            if(!appDbContext.Categories.Any(c => c.Id == product.CategoryId))
                return BadRequest();
        }
        appDbContext.Products.AddRange(products);
        appDbContext.SaveChanges();
        return StatusCode(StatusCodes.Status201Created);
    }
}
