using apistudy.interfaces;
using apistudy.Models;
using apistudy.Models.Detos;
using apistudy.Models.Entityies;
using apistudy.Utillites;

using Ecommerce_Api.interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using NuGet.Packaging;

using ServiceStack;

namespace apistudy.Servesess
{
    public class ShopingCartServess : PaginationHelper<ShopingCartDto>, IShopingCart
    {
        public FileUploadService _fileUploadService;

        public readonly AppIdentityDbContext _dbContext;
        public ShopingCartServess(AppIdentityDbContext _dbContext, FileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
            this._dbContext = _dbContext;
        }
        public ShopingCartDto DeleteAsync(int id)
        {
            var Product =
                _dbContext.ShoppingCarts.Find(id);

            var model = _dbContext.ShoppingCarts.Remove(Product);


            _dbContext.SaveChanges();
            var productquntity = _dbContext.Products.FirstOrDefault(i => i.Id == model.Entity.ProductId).Quantity;
            productquntity += model.Entity.Count;
            _dbContext.SaveChanges();



            return GetByIdAsync(model.Entity.Id);
        }

        public IEnumerable<ShopingCartDto> GetAllAsync()
        {
            var categories = _dbContext.ShoppingCarts
                     .Include(c => c.applicationUser)
                     .Select(product => new ShopingCartDto
                     {

                         Id = product.Id,
                          Count = product.Count,
                             ProductId
                             = product.Id,
                                 Price = product.Price,
                                  ApplicationUserId = product.ApplicationUserId,

                       


                     })
                     .ToList().DefaultIfEmpty();
            return categories;
        }

        public ShopingCartDto GetByIdAsync(int id)
        {
            var categories = _dbContext.ShoppingCarts
                                .Include(c => c.applicationUser)
                                .Select(product => new ShopingCartDto
                                {

                                    Id = product.Id,
                                    Count = product.Count,
                                    ProductId
                                        = product.Id,
                                    Price = product.Price,
                                    ApplicationUserId = product.ApplicationUserId,

                                


                                })
                                .FirstOrDefault();
            return categories;
        }

        public CreatedShopingCartDto Save(CreatedShopingCartDto entity)
        {
            var savedmodel = CreatedShopingCartDto.ConvertdetoTceatedobject(entity);
            if (entity.Id > 0)
            {

                var Countbeforupdate = _dbContext.ShoppingCarts.FirstOrDefault(i => i.Equals(savedmodel.Id)).Count;
                var Entity = _dbContext.ShoppingCarts.Update(savedmodel);
                var productquntity = _dbContext.Products.FirstOrDefault(i => i.Id == Entity.Entity.ProductId).Quantity;
                _dbContext.SaveChanges();

                //var CountofthiscartAfterupdate = Entity.Entity.Count;
                if (entity.Count > Countbeforupdate)
                {
                    productquntity -= entity.Count;


                }
                else
                {


                    productquntity += entity.Count;
                }


                _dbContext.SaveChanges();



                return entity;

            }
            else
            {

                var Entity = _dbContext.ShoppingCarts.Add(savedmodel);
                _dbContext.SaveChanges();
                var productquntity = _dbContext.Products.FirstOrDefault(i => i.Id == Entity.Entity.ProductId).Quantity;
                productquntity -= Entity.Entity.Count;
                _dbContext.SaveChanges();

                return entity;

            }

        }

        public IEnumerable<ShopingCartDto> GetAllByUserId(string usrid)
        {
            
            var categories = _dbContext.ShoppingCarts
                   .Where(i => i.ApplicationUserId == usrid).Include(c => c.applicationUser)
                    .Select(product => new ShopingCartDto
                    {

                        Id = product.Id,
                        Count = product.Count,
                        ProductId
                            = product.Id,
                        Price = product.Price,
                     




                    })
                    .ToList().DefaultIfEmpty();
            return categories;
        }





        public ShopingCartDto GetAllByQantityandCount(string usrid)
        {
            var totalcount = _dbContext.ShoppingCarts.Where(p => p.ApplicationUserId==usrid).Select(p => p.Count).Sum();
            var totalpriccart = _dbContext.ShoppingCarts.Where(p => p.ApplicationUserId== usrid).Select(p => p.Price).Sum();

            var categories = _dbContext.ShoppingCarts.Where(i=>i.ApplicationUserId== usrid)
                              .Include(c => c.applicationUser).Include(c => c.product)
                              .Select(product => new ShopingCartDto
                              {

                                  ApplicationUserId = product.ApplicationUserId,
                                  TotalPriceoFcart = totalcount,
                                  TotalQantity = totalpriccart,




                              })
                              .FirstOrDefault();
            return categories;

        }

    }
}
