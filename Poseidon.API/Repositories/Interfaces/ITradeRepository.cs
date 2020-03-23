using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.API.Models;

namespace Poseidon.API.Repositories
{
    /// <summary>
    /// Provides additional entity-specific repository functionality.
    /// </summary>
    public interface ITradeRepository : IRepositoryBase<Trade>
    {
        Task<IEnumerable<Trade>> GetAllAsync();
        Task<Trade> GetByIdAsync(int id);
        void CreateTrade(Trade entity);
        bool Exists(int id);
    }
}