using apistudy.Models.Detos;

using Ecommerce_Api.Models.Detos;

namespace apistudy.interfaces
{
    public interface IOrderHeder : IRepositoryService<OrderHeaderDTO>
    {


        OrderHeaderDTO Save(OrderHeaderDTO entity);
       OrderHeaderDTO GetorderhederByuserId(int id);
         

    }
}
