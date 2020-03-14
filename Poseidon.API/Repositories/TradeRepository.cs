using Poseidon.API.Data;
using Poseidon.API.Models;

namespace Poseidon.API.Repositories
{
    /// <summary>
    /// Provides additional entity-specific repository functionality.
    /// </summary>
    public class TradeRepository : RepositoryBase<Trade>, ITradeRepository
    {
        public TradeRepository(PoseidonContext context) : base(context)
        {
        }
    }
}