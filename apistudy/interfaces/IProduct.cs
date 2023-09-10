using apistudy.Models.Detos;

namespace apistudy.interfaces
{
    public interface IProduct : IRepositoryService<ProductDto>
    {


        ProductCreateDto Save(ProductCreateDto entity);


    }
}
