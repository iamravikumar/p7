using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.API.Models;

namespace Poseidon.API.Repositories
{
    /// <summary>
    /// Provides additional entity-specific repository functionality.
    /// </summary>
    public interface IBidListRepository : IRepositoryBase<BidList>
    {
        Task<IEnumerable<BidList>> GetAllAsync();
    }
}