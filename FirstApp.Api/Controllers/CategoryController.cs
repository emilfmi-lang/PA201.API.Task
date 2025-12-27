using FirstApp.Api.Data;
using FirstApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstApp.Api.Dtos.Categories;
using AutoMapper;
using FirstApp.Api.Helpers;

namespace FirstApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(AppDbContext appDbContext
                               ,IMapper mapper) : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        var Categories = appDbContext.Categories
            .Include(x => x.Products)
            .ToList();
        List<CategoriesReturnDto> CategoriesReturnDto = new List<CategoriesReturnDto>();
        foreach (var category in Categories)
        {
            CategoriesReturnDto categoryDto = new CategoriesReturnDto
            {
                Id =  category.Id,
                Name = category.Name,
                Description = category.Description,
                CreatedDate = category.CreatedDate,
                UpdatedDate = category.UpdatedDate,
                Products = category.Products.Select(p => new ProductInCategoryDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryId = p.CategoryId
                }).ToList()
            };
            CategoriesReturnDto.Add(categoryDto);
        }
        return Ok(CategoriesReturnDto);
    }
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var category = appDbContext.Categories
           .Include(x => x.Products).
            FirstOrDefault(c => c.Id == id);
        if (category == null)
            return NotFound();
        CategoriesReturnDto categoriesReturnDto = mapper.Map<CategoriesReturnDto>(category);
        return Ok(categoriesReturnDto);
    }
    [HttpPut("{id}")]
    public IActionResult Put(int id,[FromBody] CategoryUpdateDto categoryUpdateDto )
    {
        var existingCategory = appDbContext.Categories.FirstOrDefault(c => c.Id == id);
        if (existingCategory == null)
            return NotFound();
        existingCategory.Name = categoryUpdateDto.Name;
        existingCategory.Description = categoryUpdateDto.Description;
        appDbContext.SaveChanges();
        return NoContent();
    }
    [HttpPost]
    public IActionResult Post([FromForm] CategoryCreateDto categoryCreateDto)
    {
        if (appDbContext.Categories.Any(c => c.Name == categoryCreateDto.Name))
            return Conflict();
        var category = mapper.Map<Category>(categoryCreateDto);
        category.ImageUrl = categoryCreateDto.File.SaveFile("wwwroot/images/");
        appDbContext.Categories.Add(category);
        appDbContext.SaveChanges();
        return StatusCode(StatusCodes.Status201Created);
    }

   [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var category = appDbContext.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
            return NotFound();
        appDbContext.Categories.Remove(category);
        return NoContent();
    }
    //category bulky creation
    [HttpPost("bulky")]
    public IActionResult PostBulky([FromBody] List<Category> categories)
    {
        appDbContext.Categories.AddRange(categories);
        appDbContext.SaveChanges();
        return StatusCode(StatusCodes.Status201Created);
    }
}
