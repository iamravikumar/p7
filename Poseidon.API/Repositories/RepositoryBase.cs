using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Poseidon.API.Data;

namespace Poseidon.API.Repositories
{
    /// <summary>
    /// Provides generic base repository functionality.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly PoseidonContext _context;

        public RepositoryBase(PoseidonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await _context
                .Set<T>()
                .Where(expression)
                .AsNoTracking()
                .ToListAsync();
        }

        public void Create(T entity)
        {
            _context
                .Set<T>()
                .Add(entity);
        }

        public void Update(T entity)
        {
            _context
                .Set<T>()
                .Update(entity);
        }

        public void Delete(T entity)
        {
            _context
                .Set<T>()
                .Remove(entity);
        }
    }
}