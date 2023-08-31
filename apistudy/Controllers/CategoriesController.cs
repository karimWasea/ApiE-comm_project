using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using apistudy.Models.Detos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using apistudy.Models;
using apistudy.Models.Entityies;

namespace apistudy.Controllers
{

    

    namespace YourProjectName.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class CategoriesController : ControllerBase
        {
            private readonly AppIdentityDbContext _context;

            public CategoriesController(AppIdentityDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
            {
                var categories = await _context.Categories
                    .Include(c => c.Products)
                    .Select(category => new CategoryDto
                    {
                        Id = category.Id,
                        Title = category.Title,
                        Description = category.Description,
                        ProductNames = category.Products.Select(p => p.Title).ToList(),
                        ProductTitles = category.Products.Select(p => p.Description).ToList()
                    })
                    .ToListAsync();

                return Ok(categories);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<CategoryDto>> GetCategory(int id)
            {
                var category = await _context.Categories
                    .Include(c => c.Products)
                    .Where(c => c.Id == id)
                    .Select(category => new CategoryDto
                    {
                        Id = category.Id,
                        Title = category.Title,
                        Description = category.Description,
                        ProductNames = category.Products.Select(p => p.Title).ToList(),
                        ProductTitles = category.Products.Select(p => p.Description).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }

            [HttpPost]
            public async Task<ActionResult<Category>> CreateCategory(Category category)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
            }

            [HttpPut("{id}")] 
            public async Task<IActionResult> UpdateCategory(int id, CategoryDto updatedCategory)
            {
                var existingCategory = await _context.Categories.FindAsync(id);

                if (existingCategory == null)
                {
                    return NotFound();
                }

                UpdateCategoryProperties(existingCategory, updatedCategory);
                await _context.SaveChangesAsync();

                return Ok(existingCategory);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteCategory(int id)
            {
                var category = await _context.Categories.FindAsync(id);

                if (category == null)
                {
                    return NotFound();
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private void UpdateCategoryProperties(Category existingCategory, CategoryDto updatedCategory)
            {
                existingCategory.Title = updatedCategory.Title;
                existingCategory.Description = updatedCategory.Description;
                // You can add more property updates here if needed
            }
        }
    }

}


