namespace apistudy.interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepositoryService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAllAsync();
        TEntity GetByIdAsync(int id);
        //Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity DeleteAsync(int id);
    }
}
