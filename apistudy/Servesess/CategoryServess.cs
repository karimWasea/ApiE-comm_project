using apistudy.interfaces;
using apistudy.Models;
using apistudy.Models.Detos;

using Microsoft.EntityFrameworkCore;

namespace apistudy.Servesess
{
    public class CategoryServess : ICategories
    {
        public readonly AppIdentityDbContext _dbContext;
        public CategoryServess(AppIdentityDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public CategoryDto DeleteAsync(int id)
        {
            var category =
                _dbContext.Categories.Find(id);



            var entity =
                   _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return GetByIdAsync(entity.Entity.Id);
        }

        public IEnumerable<CategoryDto> GetAllAsync()
        {
            var categories = _dbContext.Categories
                     .Include(c => c.Products)
                     .Select(category => new CategoryDto
                     {
                         Id = category.Id,
                         Title = category.Title,
                         Description = category.Description,
                         //ProductNames = category.Products.Select(p => p.Title).ToList(),
                         //ProductTitles = category.Products.Select(p => p.Description).ToList()
                     })
                     .ToList();
            return categories;
        }

        public CategoryDto GetByIdAsync(int id)
        {

            var category = _dbContext.Categories
                .Include(c => c.Products)
                .Where(c => c.Id == id)
                .Select(category => new CategoryDto
                {
                    Id = category.Id,
                    Title = category.Title,
                    Description = category.Description,
                    //ProductNames = category.Products.Select(p => p.Title).ToList(),
                    //ProductTitles = category.Products.Select(p => p.Description).ToList()
                })
                .FirstOrDefault();
            return category;
        }

        public
            CategoryDto Save(CategoryDto entity)
        {
            var savedmodel = CategoryDto.ConvertdetoTceatedobject(entity);
            if (entity.Id > 0)
            {
                var Entity = _dbContext.Categories.Update(savedmodel);
                _dbContext.SaveChanges();
                return GetByIdAsync(Entity.Entity.Id);

            }
            else
            {

                var Entity = _dbContext.Categories.Add(savedmodel);
                _dbContext.SaveChanges();
                return
                    GetByIdAsync(Entity.Entity.Id);


            }
        }
    }
}
