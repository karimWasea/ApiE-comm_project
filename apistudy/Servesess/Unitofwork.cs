using apistudy.interfaces;
using apistudy.Models;

using Ecommerce_Api.interfaces;

namespace apistudy.Servesess
{
    public class Unitofwork : IUnitOFWork



    {
        public readonly AppIdentityDbContext _context;

        public Unitofwork(AppIdentityDbContext context, CategoryServess categoryServess  , ProductServess productServess , ShopingCartServess shopingCartServess , OrderHederServess orderHederServess , OrdeRDetailsSErvess ordeRDetailsSErvess)
        {
            shopingCart =  shopingCartServess ;
            Product =productServess;
            Categories = categoryServess;

            _context = context;
            orderHeder=orderHederServess;
            orderDetails = ordeRDetailsSErvess;
        }




        #region// Implement the Dispose method to release resources
        private bool disposed = false;

        public ICategories Categories { get; }
        public IProduct Product { get; }
        public IShopingCart shopingCart { get; }
        public IOrderHeder orderHeder { get; }
        public IorderDetails orderDetails { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        // Implement the finalizer to release unmanaged resources
        ~Unitofwork()
        {
            Dispose(false);
        }
        #endregion





    }
}
