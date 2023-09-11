namespace Ecommerce_Api.interfaces
{
    using Braintree;
    // Services/IPaginationService.cs
    using System.Collections.Generic;
    using System.Threading.Tasks;

  

    public interface IPaginationService<TEntity> where TEntity : class
    {
        Task<PaginatedResult<TEntity>> GetPaginatedResultsAsync(int page, int pageSize);
    }



}
