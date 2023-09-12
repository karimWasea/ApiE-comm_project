using apistudy.Models.Detos;
using apistudy.Models.Entityies;

using Ecommerce_Api.interfaces;

namespace apistudy.interfaces
{
    public interface IShopingCart :  IRepositoryService<ShopingCartDto> , IPaginationHelper<ShopingCartDto>
    {


        CreatedShopingCartDto Save(CreatedShopingCartDto entity);

        IEnumerable<ShopingCartDto> GetAllByUserId( string usrid);
        ShopingCartDto GetAllByQantityandCount(string usrid);

    }
}
