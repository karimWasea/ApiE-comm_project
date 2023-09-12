using apistudy.interfaces;
using apistudy.Models;
using apistudy.Models.Detos;
using apistudy.Models.Entityies;
using apistudy.Utillites;

using Ecommerce_Api.interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using NuGet.Packaging;

using ServiceStack;

namespace apistudy.Servesess
{
    public class ShopingCartServess : PaginationHelper<ShopingCartDto>, IShopingCart
    {//b0e51957-70a4-4fd9-87d6-b0e9882a7160
        public FileUploadService _fileUploadService;
        IProduct _Product;
        UserManager<ApplicationUser> _UserManager;

        public readonly AppIdentityDbContext _dbContext;
        public ShopingCartServess(AppIdentityDbContext _dbContext, FileUploadService fileUploadService , ProductServess Product, UserManager<ApplicationUser> UserManager
)
        {
            _UserManager = UserManager;
            _Product = Product;
            _fileUploadService = fileUploadService;
            this._dbContext = _dbContext;
        }
        public ShopingCartDto DeleteAsync(int id)
        {
            var Product =
                _dbContext.ShoppingCarts.Find(id);

            var model = _dbContext.ShoppingCarts.Remove(Product);


            _dbContext.SaveChanges();
            var productquntity = _dbContext.Products.FirstOrDefault(i => i.Id == model.Entity.ProductId);
            productquntity.Quantity += model.Entity.Count;
            _dbContext.Products.Update(productquntity);
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

        //public CreatedShopingCartDto Save(CreatedShopingCartDto entity)
        //{
        //     var price= _dbContext.Products.FirstOrDefault(p => p.Id==entity.ProductId).Price;

        //    entity.Price = entity.Count * price;
        //    var Countbeforupdate = _dbContext.ShoppingCarts.FirstOrDefault(i => i.Id == entity.Id).Count;
        //    var savedmodel = CreatedShopingCartDto.ConvertdetoTceatedobject(entity);
        //    if (entity.Id > 0)
        //    {




        //        var Entity = _dbContext.ShoppingCarts.Update(savedmodel);
        //        var productquntity = _dbContext.Products.FirstOrDefault(i => i.Id == Entity.Entity.ProductId);
        //        _dbContext.SaveChanges();

        //        //var CountofthiscartAfterupdate = Entity.Entity.Count;
        //        if (entity.Count > Countbeforupdate)
        //        {
        //            productquntity.Quantity -= entity.Count;

        //        }
        //        else
        //        {


        //            productquntity.Quantity += entity.Count;

        //        }

        //        _dbContext.Products.Update(productquntity);
        //        _dbContext.SaveChanges();



        //        return entity;

        //    }
        public CreatedShopingCartDto Save(CreatedShopingCartDto entity)
        {
            // Find the price of the product with the given ProductId
            var price = _dbContext.Products.FirstOrDefault(p => p.Id == entity.ProductId)?.Price;

            if (price == null)
            {
                // Handle the case where the product with the given ProductId doesn't exist.
                // You may want to return an error or take appropriate action.
                return null; // or throw an exception
            }

            // Calculate the total price based on Count and Price per unit
            entity.Price = entity.Count * price.Value;

            if (entity.Id > 0)
            {
                // Update an existing ShoppingCart
                var existingCart = _dbContext.ShoppingCarts.FirstOrDefault(i => i.Id == entity.Id);

                if (existingCart != null)
                {
                    var CountBeforeUpdate = existingCart.Count;

                    // Update the properties of the existingCart with values from savedmodel
                    existingCart.Count = entity.Count;
                    existingCart.Price = entity.Price;
                    existingCart.Id = entity.Id;
                    // Save changes to the database
                    _dbContext.SaveChanges();

                    // Calculate the difference in Count and update Product Quantity accordingly
                    var countDifference = entity.Count - CountBeforeUpdate;
                    var product = _dbContext.Products.FirstOrDefault(i => i.Id == entity.ProductId);

                    if (product != null)
                    {
                        if (entity.Count > CountBeforeUpdate) { 
                            product.Quantity -= countDifference;
                            _dbContext.Update(product);
                        // Save changes to update Product Quantity
                        _dbContext.SaveChanges();
                    }
                    else
                    {

                        product.Quantity -= countDifference;
                            _dbContext.Update(product);

                            _dbContext.SaveChanges();
                    }
                    }
                  
                
                    return entity;
                }
                else
                {
                    // Handle the case where the ShoppingCart with the given Id doesn't exist.
                    // You may want to return an error or take appropriate action.
                    return null; // or throw an exception
                }
            }
            else
            {
                // Create a new ShoppingCart
                var newCart = new ShoppingCart
                {
                    // Set properties based on the CreatedShopingCartDto
                    ProductId = entity.ProductId,
                    Count = entity.Count,
                    Price = entity.Price
                };

                // Add the new ShoppingCart to the context
                _dbContext.ShoppingCarts.Add(newCart);

                // Update Product Quantity
                var product = _dbContext.Products.FirstOrDefault(i => i.Id == entity.ProductId);
                if (product != null)
                {
                    product.Quantity -= entity.Count;
                    _dbContext.Update(product);

                    // Save changes to update Product Quantity
                    _dbContext.SaveChanges();
                }

                // Save changes to create the new ShoppingCart and update Product Quantity
                _dbContext.SaveChanges();

                // Return the newly created entity
                return entity;
            }
        }
        

        //    else
        //    {

        //        var Entity = _dbContext.ShoppingCarts.Add(savedmodel);
        //        _dbContext.SaveChanges();
        //        var productquntity = _dbContext.Products.FirstOrDefault(i => i.Id == Entity.Entity.ProductId);
        //        productquntity.Quantity -= Entity.Entity.Count;
        //        _dbContext.Products.Update(productquntity);
          

        //        _dbContext.SaveChanges();

        //        return entity;

        //    }

        //}

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

            var categories = _dbContext.ShoppingCarts.Where(i => i.ApplicationUserId == usrid)
                              .Include(c => c.applicationUser).Include(c => c.product)
                              .Select(product => new ShopingCartDto
                              {

                                  ApplicationUserId = product.ApplicationUserId,
                                  TotalPriceoFcart = totalcount,
                                  TotalQantity = totalpriccart,
                                  UserName = _UserManager.Users.FirstOrDefault(p => p.Id == usrid).UserName,




                              })
                              .FirstOrDefault();
            return categories;

        }

    }
}
