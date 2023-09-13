using apistudy.interfaces;
using apistudy.Models;
using apistudy.Models.Detos;
using apistudy.Models.Entityies;
using apistudy.Utillites;

using Ecommerce_Api.interfaces;
using Ecommerce_Api.Models.Detos;

using Humanizer;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using NuGet.Packaging;

using ServiceStack;

namespace apistudy.Servesess
{
    public class OrderHederServess : PaginationHelper<OrderHeaderDTO>, IOrderHeder
    {

        public readonly AppIdentityDbContext _dbContext;
        public OrderHederServess(AppIdentityDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public OrderHeaderDTO DeleteAsync(int id)
        {
            var OrderHeaders =
                _dbContext.OrderHeaders.Find(id);
        
         

            var entity =
                _dbContext.OrderHeaders.Remove(OrderHeaders);
            _dbContext.SaveChangesAsync();

            return GetByIdAsync(entity.Entity.Id);
        }

        public IEnumerable<OrderHeaderDTO> GetAllAsync()
        {
            var categories = _dbContext.OrderHeaders
                     .Include(c => c.ApplicationUser)
                    .Select(dto => new OrderHeaderDTO
                    {

                        ApplicationUserId = dto.ApplicationUserId,
                        OrderDate = dto.OrderDate,
                        ShippingDate = dto.ShippingDate,
                        OrderTotal = dto.OrderTotal,
                        OrderStatus = dto.OrderStatus,
                        PaymentStatus = dto.PaymentStatus,
                        TrackingNumber = dto.TrackingNumber,
                        Carrier = dto.Carrier,
                        PaymentDate = dto.PaymentDate,
                        PaymentDueDate = dto.PaymentDueDate,
                        SessionId = dto.SessionId,
                        PaymentIntentId = dto.PaymentIntentId,
                        PhoneNumber = dto.PhoneNumber,
                        StreetAddress = dto.StreetAddress,
                        City = dto.City,
                        State = dto.State,
                        PostalCode = dto.PostalCode,
                        Name = dto.Name

                    })
                     .ToList().DefaultIfEmpty();
            return categories;
        }

        public OrderHeaderDTO GetByIdAsync(int id)
        {

            var product = _dbContext.OrderHeaders
               .Include(c => c.ApplicationUser)
 .Where(c => c.Id == id)
                .Select(dto => new OrderHeaderDTO
                {

                    ApplicationUserId = dto.ApplicationUserId,
                    OrderDate = dto.OrderDate,
                    ShippingDate = dto.ShippingDate,
                    OrderTotal = dto.OrderTotal,
                    OrderStatus = dto.OrderStatus,
                    PaymentStatus = dto.PaymentStatus,
                    TrackingNumber = dto.TrackingNumber,
                    Carrier = dto.Carrier,
                    PaymentDate = dto.PaymentDate,
                    PaymentDueDate = dto.PaymentDueDate,
                    SessionId = dto.SessionId,
                    PaymentIntentId = dto.PaymentIntentId,
                    PhoneNumber = dto.PhoneNumber,
                    StreetAddress = dto.StreetAddress,
                    City = dto.City,
                    State = dto.State,
                    PostalCode = dto.PostalCode,
                    Name = dto.Name

                })
                .FirstOrDefault();
            return product;
        }

        public OrderHeaderDTO GetorderhederByuserId(string id)
        {
            var product = _dbContext.OrderHeaders
                .Include(c => c.ApplicationUser)
  .Where(c => c.ApplicationUserId == id)
                 .Select(dto => new OrderHeaderDTO
                 {

                     ApplicationUserId = dto.ApplicationUserId,
                     OrderDate = dto.OrderDate,
                     ShippingDate = dto.ShippingDate,
                     OrderTotal = dto.OrderTotal,
                     OrderStatus = dto.OrderStatus,
                     PaymentStatus = dto.PaymentStatus,
                     TrackingNumber = dto.TrackingNumber,
                     Carrier = dto.Carrier,
                     PaymentDate = dto.PaymentDate,
                     PaymentDueDate = dto.PaymentDueDate,
                     SessionId = dto.SessionId,
                     PaymentIntentId = dto.PaymentIntentId,
                     PhoneNumber = dto.PhoneNumber,
                     StreetAddress = dto.StreetAddress,
                     City = dto.City,
                     State = dto.State,
                     PostalCode = dto.PostalCode,
                     Name = dto.Name

                 })
                 .FirstOrDefault();
            return product;
        }

        public OrderHeaderDTO Save(OrderHeaderDTO entity)
        {
            var savedmodel = OrderHeaderDTO.ConvertDtoToCreatedObject(entity);
            if (entity.Id > 0)
            {
                var Entity = _dbContext.OrderHeaders.Update(savedmodel);



                _dbContext.SaveChanges();
               

                

                 return entity;

            }
            else
            {

                var Entity = _dbContext.OrderHeaders.Add(savedmodel);
                _dbContext.SaveChanges();


                return entity;

            }
        }
    }
}
