using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Poseidon.API.Data;
using Poseidon.API.Models;

namespace Poseidon.API.Repositories
{
    /// <summary>
    /// Provides additional entity-specific repository functionality.
    /// </summary>
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(PoseidonContext context) : base(context)
        {
        }

        /// <summary>
        /// Asynchronously gets all User entities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllAsync() =>
            await base.FindAll()
                .OrderBy(bl => bl.Id)
                .ToListAsync();

        public async Task<User> GetByIdAsync(int id) =>
            await base.FindByCondition(x => x.Id == id)
                .FirstOrDefaultAsync();

        public void CreateUser(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            base.Insert(entity);
        }

        public bool Exists(int id) =>
            base.Exists(x => x.Id == id);
    }
}