using apistudy.Models.Detos;

namespace apistudy.interfaces
{
    public interface ICategories : IRepositoryService<CategoryDto>
    {


        CategoryDto Save(CategoryDto entity);


    }
}
