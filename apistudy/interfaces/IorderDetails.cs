using apistudy.Models.Detos;
using apistudy.Models.Entityies;

using Ecommerce_Api.interfaces;
using Ecommerce_Api.Models.Detos;

namespace apistudy.interfaces
{
    public interface IorderDetails : IRepositoryService<OrderdetailDto> , IPaginationHelper<OrderdetailDto>
    {


        OrderdetailDto Save(OrderdetailDto entity);
        OrderdetailDto GetorderhederByuserId(string id);
         

    }
}
