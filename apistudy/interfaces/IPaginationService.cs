namespace Ecommerce_Api.interfaces
{
    using Braintree;

    using PagedList;
    // Services/IPaginationService.cs
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface IPaginationHelper<T>
    {
        IPagedList<T> GetPagedData<T>(IEnumerable<T> data, int pagenumber);
    }


}
