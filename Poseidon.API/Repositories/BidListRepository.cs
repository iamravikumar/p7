using Poseidon.API.Data;
using Poseidon.API.Models;

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
    }
}