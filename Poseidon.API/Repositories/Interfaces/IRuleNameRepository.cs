using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.API.Models;

namespace Poseidon.API.Repositories
{
    /// <summary>
    /// Provides additional entity-specific repository functionality.
    /// </summary>
    public interface IRuleNameRepository : IRepositoryBase<RuleName>
    {
        Task<IEnumerable<RuleName>> GetAllAsync();
        Task<RuleName> GetByIdAsync(int id);
        void CreateRuleName(RuleName entity);
        bool Exists(int id);
    }
}