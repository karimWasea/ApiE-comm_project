using apistudy.Models.Entityies;

namespace Ecommerce_Api.Models.Detos
{
    
        public class OrderHeaderDTO
        {public int Id { get; set; }
            public string ApplicationUserId { get; set; }
            public string ApplicationUserName { get; set; }
            public DateTime OrderDate { get; set; }
            public DateTime ShippingDate { get; set; }
            public double OrderTotal { get; set; }
            public string OrderStatus { get; set; }
            public string PaymentStatus { get; set; }
            public string TrackingNumber { get; set; }
            public string Carrier { get; set; }
            public DateTime PaymentDate { get; set; }
            public DateTime PaymentDueDate { get; set; }
            public string SessionId { get; set; }
            public string PaymentIntentId { get; set; }
            public string PhoneNumber { get; set; }
            public string StreetAddress { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
            public string Name { get; set; }
       
            // ... other properties

            public static OrderHeader ConvertDtoToCreatedObject(OrderHeaderDTO dto)
            {
                return new OrderHeader
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
                };
            
        }


    }


}
