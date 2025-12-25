using FirstApp.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly List<Category> Categories = new()
    {
        new Models.Category { Id = 1, Name = "Electronics", Description = "Devices and gadgets" },
        new Models.Category { Id = 2, Name = "Books", Description = "Printed and digital books" },
        new Models.Category { Id = 3, Name = "Clothing", Description = "Apparel and accessories" }
    };
    [HttpGet]
    public ActionResult<IEnumerable<Category>> Get()
    {
        return Ok(Categories);
    }
    [HttpGet("{id}")]
    public ActionResult<Category> GetById(int id)
    {
        var category = Categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
            return NotFound();
        return Ok(category);
    }
    [HttpPut("{id}")]
    public IActionResult Put([FromBody] Category category)
    {
        var existingCategory = Categories.FirstOrDefault(c => c.Id == category.Id);
        if (existingCategory == null)
            return NotFound();
        existingCategory.Name = category.Name;
        existingCategory.Description = category.Description;
        return NoContent();
    }
    [HttpPost]
    public IActionResult Post([FromBody] Category category)
    {
        category.Id = Categories.Max(c => c.Id) + 1;
        Categories.Add(category);
        return StatusCode(StatusCodes.Status201Created);
    }
   [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var category = Categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
            return NotFound();
        Categories.Remove(category);
        return NoContent();
    }
}
