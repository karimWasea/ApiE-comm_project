﻿using apistudy.interfaces;
using apistudy.Models;

namespace apistudy.Servesess
{
    public class Unitofwork : IUnitOFWork



    {
        public readonly AppIdentityDbContext _context;

        public Unitofwork(AppIdentityDbContext context, CategoryServess categoryServess  , ProductServess productServess)
        {
             Product =productServess;
            Categories = categoryServess;

            _context = context;

        }




        #region// Implement the Dispose method to release resources
        private bool disposed = false;

        public ICategories Categories { get; }
        public IProduct Product { get; }

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
