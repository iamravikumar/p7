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
    public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
    {
        public RatingRepository(PoseidonContext context) : base(context)
        {
        }

        /// <summary>
        /// Asynchronously gets all Rating entities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Rating>> GetAllAsync() =>
            await base.FindAll()
                .OrderBy(bl => bl.Id)
                .ToListAsync();

        public async Task<Rating> GetByIdAsync(int id) =>
            await base.FindByCondition(x => x.Id == id)
                .FirstOrDefaultAsync();

        public void CreateRating(Rating entity)
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