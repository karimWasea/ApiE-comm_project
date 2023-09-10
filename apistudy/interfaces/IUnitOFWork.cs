using apistudy.Models.Entityies;

namespace apistudy.interfaces
{
    public interface IUnitOFWork : IDisposable
    {

        IProduct Product { get; }
        ICategories Categories { get; }
    }
}
