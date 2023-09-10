using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using apistudy.Models.Detos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using apistudy.Models;
using apistudy.Models.Entityies;
using apistudy.Servesess;
using apistudy.interfaces;

namespace apistudy.Controllers
{

    

    namespace YourProjectName.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class CategoriesController : ControllerBase
        {
            //private readonly AppIdentityDbContext _context;
            public readonly Unitofwork _Unitofwork;
            public CategoriesController( Unitofwork Unitofwork)
            {
                _Unitofwork = Unitofwork;   
                //_context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
            {
                //var categories = await _context.Categories
                //    .Include(c => c.Products)
                //    .Select(category => new CategoryDto
                //    {
                //        Id = category.Id,
                //        Title = category.Title,
                //        Description = category.Description,
                //        ProductNames = category.Products.Select(p => p.Title).ToList(),
                //        ProductTitles = category.Products.Select(p => p.Description).ToList()
                //    })
                //    .ToListAsync();
                var categories = _Unitofwork.Categories.GetAllAsync();
                return Ok(categories);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<CategoryDto>> GetCategory(int id)
            {
                //var category = await _context.Categories
                //    .Include(c => c.Products)
                //    .Where(c => c.Id == id)
                //    .Select(category => new CategoryDto
                //    {
                //        Id = category.Id,
                //        Title = category.Title,
                //        Description = category.Description,
                //        ProductNames = category.Products.Select(p => p.Title).ToList(),
                //        ProductTitles = category.Products.Select(p => p.Description).ToList()
                //    })
                //    .FirstOrDefaultAsync();
                var category =   _Unitofwork.Categories.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }

            [HttpPost]
            public async Task<ActionResult<CategoryDto>> CreateCategory( [FromBody] CategoryDto category)
            {
                _Unitofwork.Categories.Save(category);

                return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
            }

            [HttpPut("{id}")] 
            public async Task<ActionResult<CategoryDto>> UpdateCategory( CategoryDto updatedCategory)
            {
                //var existingCategory = await _context.Categories.FindAsync(id);

                //if (existingCategory == null)
                //{
                //    return NotFound();
                //}

                //UpdateCategoryProperties(existingCategory, updatedCategory);
                //await _context.SaveChangesAsync();
                var existingCategory = _Unitofwork.Categories.Save(updatedCategory);
                return Ok(existingCategory);
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult<CategoryDto>> DeleteCategory(int id)
            {
                //var category = await _context.Categories.FindAsync(id);

                //if (category == null)
                //{
                //    return NotFound();
                //}

                //_context.Categories.Remove(category);
                //await _context.SaveChangesAsync();
                var category =   _Unitofwork.Categories.DeleteAsync(id);


                return Ok(category);
            }

           
        }
    }

}


