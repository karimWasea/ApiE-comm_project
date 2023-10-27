using apistudy.interfaces;
using apistudy.Models;
using apistudy.Models.Detos;
using apistudy.Models.Entityies;
using apistudy.Utillites;

using Ecommerce_Api.interfaces;
using Ecommerce_Api.Models.Detos;

using Humanizer;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using NuGet.Packaging;

using ServiceStack;

using System;

namespace apistudy.Servesess
{
    public class OrdeRDetailsSErvess : PaginationHelper<OrderdetailDto>, IorderDetails
    {
        UserManager<ApplicationUser> _UserManager;

        public readonly AppIdentityDbContext _dbContext;
        public OrdeRDetailsSErvess(AppIdentityDbContext _dbContext, UserManager<ApplicationUser> UserManager)
        { 
            _UserManager = UserManager; 
            this._dbContext = _dbContext;
        }
        public OrderdetailDto DeleteAsync(int id)
        {
            var OrderHeaders =
                _dbContext.OrderDetails.Find(id);



            var entity =
                _dbContext.OrderDetails.Remove(OrderHeaders);
            _dbContext.SaveChangesAsync();

            return GetByIdAsync(entity.Entity.Id);
        }

        public IEnumerable<OrderdetailDto> GetAllAsync()
        {
            var categories = _dbContext.OrderDetails
                     .Include(c => c.OrderHeader).Include(p => p.applicationUser)
                    .Select(entity => new OrderdetailDto
                    {

                        OrderHeaderId = entity.OrderHeaderId,
                        UserId = entity.applicstionuserid,
                        Id = entity.Id,
                        Count = entity.Count,
                        Price = entity.Price,
                        ProductId = entity.Productid,
                        UserName = _UserManager.Users.Where(i => i.Id == entity.applicstionuserid).Select(i => i.UserName).FirstOrDefault(),

                    })
                     .ToList().DefaultIfEmpty();
            return categories;
        }

        public OrderdetailDto GetByIdAsync(int id)
        {



            var OrderdetailDto = _dbContext.OrderDetails.Where(i=>i.Id==id)
                     .Include(c => c.OrderHeader).Include(p => p.applicationUser)
                    .Select(entity => new OrderdetailDto
                    {

                        OrderHeaderId = entity.OrderHeaderId,
                        UserId = entity.applicstionuserid,
                        Id = entity.Id,
                        Count = entity.Count,
                        Price = entity.Price,
                        ProductId = entity.Productid,
                        UserName = _UserManager.Users.Where(i => i.Id == entity.applicstionuserid).Select(i => i.UserName).FirstOrDefault(),

                    })
                .FirstOrDefault();
            return OrderdetailDto;
        }

        public OrderdetailDto GetorderhederByuserId(string usrid)
        {
            var totalcount = _dbContext.OrderDetails.Where(p => p.applicstionuserid == usrid).Select(p => p.Count).Sum();
            double totalpriccart = _dbContext.OrderDetails.Where(p => p.applicstionuserid == usrid).Select(p => p.Price).Sum();

            var categories = _dbContext.OrderDetails.Where(i => i.applicstionuserid == usrid)
                              .Include(c => c.applicationUser).Include(c => c.Product)
                              .Select(product => new OrderdetailDto
                              {

                                  UserId = product.applicstionuserid,
                                  TotalPriceoFcart= totalpriccart,
                                  TotalQantity = totalcount,
                                  UserName = _UserManager.Users.FirstOrDefault(p => p.Id == usrid).UserName,




                              })
                              .FirstOrDefault();
            return categories;
        }

        public OrderdetailDto Save(OrderdetailDto entity)
        {
            var savedmodel = OrderdetailDto.ConvertDtoToCreatedObject(entity);
            if (entity.Id > 0)
            {
                var Entity = _dbContext.OrderDetails.Update(savedmodel);



                _dbContext.SaveChanges();




                return entity;

            }
            else
            {

                var Entity = _dbContext.OrderDetails.Add(savedmodel);
                _dbContext.SaveChanges();


                return entity;

            }
        }
    }
}
