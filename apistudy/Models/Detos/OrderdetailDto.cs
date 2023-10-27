using apistudy.Models.Entityies;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ecommerce_Api.Models.Detos
{
    public class OrderdetailDto
    {
        public int OrderHeaderId { get; set; }
    


        public int ProductId { get; set; }
        public string  UserId { get; set; }
        [JsonIgnore]
        public string  UserName { get; set; }
        public int Id { get; set; }

        public double TotalPriceoFcart { get; set; }
        public int TotalQantity { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public static OrderDetail ConvertDtoToCreatedObject(OrderdetailDto entity)
        {
            return new OrderDetail
            {
                OrderHeaderId = entity.OrderHeaderId,
                applicstionuserid = entity.UserId,
                Id = entity.Id,
                 Count = entity.Count,
                Price = entity.Price,
                Productid = entity.ProductId,
            };
        }
    }
}

