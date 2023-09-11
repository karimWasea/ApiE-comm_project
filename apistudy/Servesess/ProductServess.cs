using apistudy.interfaces;
using apistudy.Models;
using apistudy.Models.Detos;
using apistudy.Models.Entityies;
using apistudy.Utillites;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using NuGet.Packaging;

using ServiceStack;

namespace apistudy.Servesess
{
    public class ProductServess : IProduct
    {
        public FileUploadService _fileUploadService;

        public readonly AppIdentityDbContext _dbContext;
        public ProductServess(AppIdentityDbContext _dbContext, FileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
            this._dbContext = _dbContext;
        }
        public ProductDto DeleteAsync(int id)
        {
            var Product =
                _dbContext.Products.Find(id);
            var listofimges = _dbContext.ProductImages.Where(p => p.ProductId == id).ToList();
            foreach (var item in listofimges)
            {
                if (!string.IsNullOrEmpty(item.ImageUrl))
                {
                    _fileUploadService.DeleteImage(item.ImageUrl);
                }
            }

            _dbContext.ProductImages.RemoveRange(listofimges);
            _dbContext.SaveChanges();

            var entity =
                _dbContext.Products.Remove(Product);
            _dbContext.SaveChangesAsync();

            return GetByIdAsync(entity.Entity.Id);
        }

        public IEnumerable<ProductDto> GetAllAsync()
        {
            var categories = _dbContext.Products
                     .Include(c => c.ProductImages)
                     .Select(product => new ProductDto
                     {

                         Id = product.Id,
                         Title = product.Title,
                         Description = product.Description,
                         Price = product.Price,
                         Images = product.ProductImages.Select(i => i.ImageUrl).ToList(),
                         CategoryId = product.CategoryId,
                         Quantity = product.Quantity,
                         Offer = product.Offer,
                         CategoryName = product.Category.Title,
                         productimgidId=product.ProductImages.Select(p=>p.ProductId).ToList(),


                     })
                     .ToList().DefaultIfEmpty();
            return categories;
        }

        public ProductDto GetByIdAsync(int id)
        {

            var product = _dbContext.Products
                .Include(c => c.ProductImages)
                .Where(c => c.Id == id)
                .Select(product => new ProductDto
                {

                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price,
                    //Image = product.Image,
                    CategoryId = product.CategoryId,
                    Quantity = product.Quantity,
                    Offer = product.Offer,
                    CategoryName = product.Category.Title,
                    Images = product.ProductImages.Select(r => r.ImageUrl).ToList(),

                    productimgidId = product.ProductImages.Select(p => p.ProductId).ToList(),

                })
                .FirstOrDefault();
            return product;
        }

        public ProductCreateDto Save(ProductCreateDto entity)
        {
            var savedmodel = ProductCreateDto.ConvertdetoTceatedobject(entity);
            if (entity.Id > 0)
            {
                var Entity = _dbContext.Products.Update(savedmodel);



                _dbContext.SaveChanges();
                if (entity.IFormFileCollectionImageUrls != null)
                {
                    var imges = _fileUploadService.UploadFiles(entity.IFormFileCollectionImageUrls);


                    foreach (var im in imges)
                    {
                        var imgess = new ProductImage() { ImageUrl = im, Id = entity.productimgidId, ProductId = Entity.Entity.Id };
                        _dbContext.ProductImages.Update(imgess);

                        _dbContext.SaveChanges();
                    }

                }


                //var returnobj=   GetByIdAsync(Entity.Entity.Id); ;
                return entity;

            }
            else
            {

                var Entity = _dbContext.Products.Add(savedmodel);
                _dbContext.SaveChanges();


                if (entity.IFormFileCollectionImageUrls != null)
                {
                    var imges = _fileUploadService.UploadFiles(entity.IFormFileCollectionImageUrls);
                    foreach (var im in imges)
                    {
                        var imgess = new ProductImage() { ImageUrl = im, Id = entity.productimgidId, ProductId = Entity.Entity.Id };
                        _dbContext.ProductImages.Add(imgess);

                        _dbContext.SaveChanges();


                    }
                }
                return entity;

            }
        }
    }
}
