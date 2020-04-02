using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.API.Models;

namespace Poseidon.API.Repositories
{
    /// <summary>
    /// Provides additional entity-specific repository functionality.
    /// </summary>
    public interface IRatingRepository : IRepositoryBase<Rating>
    {
        Task<IEnumerable<Rating>> GetAllAsync();
        Task<Rating> GetByIdAsync(int id);
        void CreateRating(Rating entity);
        bool Exists(int id);
    }
}