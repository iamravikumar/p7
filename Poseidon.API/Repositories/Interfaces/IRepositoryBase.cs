using System;
using System.Linq;
using System.Linq.Expressions;

namespace Poseidon.API.Repositories
{
    /// <summary>
    /// Provides generic base repository functionality.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Exists(Expression<Func<T, bool>> expression);
    }
}