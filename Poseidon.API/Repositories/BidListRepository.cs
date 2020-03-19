using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Poseidon.API.Data;
using Poseidon.API.Models;
// ReSharper disable RedundantBaseQualifier

namespace Poseidon.API.Repositories
{
    /// <summary>
    /// Provides additional entity-specific repository functionality.
    /// </summary>
    public class BidListRepository : RepositoryBase<BidList>, IBidListRepository
    {
        public BidListRepository(PoseidonContext context) : base(context)
        {
        }

        /// <summary>
        /// Asynchronously gets all BidList entities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BidList>> GetAllAsync() =>
            await base.FindAll()
                .OrderBy(bl => bl.Id)
                .ToListAsync();

    }
}