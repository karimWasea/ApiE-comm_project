using apistudy.Models.Detos;

using Ecommerce_Api.interfaces;

namespace apistudy.interfaces
{
    public interface IProduct :  IRepositoryService<ProductDto> , IPaginationHelper<ProductDto>
    {


        ProductCreateDto Save(ProductCreateDto entity);


    }
}
