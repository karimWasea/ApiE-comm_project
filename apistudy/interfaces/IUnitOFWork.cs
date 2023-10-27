using apistudy.interfaces;

namespace Ecommerce_Api.interfaces
{
    public interface IUnitOFWork :IDisposable
    {
        



        public ICategories Categories { get; }
        public IProduct Product { get; }
        public IShopingCart shopingCart { get; }
        public IOrderHeder orderHeder { get; }    
        public IorderDetails  orderDetails { get; }

    }
}
