using apistudy.Models.Detos;

using Ecommerce_Api.interfaces;
using Ecommerce_Api.Models.Detos;

namespace apistudy.interfaces
{
    public interface IOrderHeder : IRepositoryService<OrderHeaderDTO> , IPaginationHelper<ProductDto>
    {


        OrderHeaderDTO Save(OrderHeaderDTO entity);
       OrderHeaderDTO GetorderhederByuserId(string id);
         

    }
}
