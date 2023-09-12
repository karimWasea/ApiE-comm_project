using apistudy.Models.Entityies;

namespace apistudy.interfaces
{
    public interface IUnitOFWork : IDisposable
    {
        IShopingCart shopingCart { get; }
        IProduct Product { get; }
        ICategories Categories { get; }
    }
}
