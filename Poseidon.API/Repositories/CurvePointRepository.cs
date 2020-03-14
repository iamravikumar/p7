using Poseidon.API.Data;
using Poseidon.API.Models;

namespace Poseidon.API.Repositories
{
    /// <summary>
    /// Provides additional entity-specific repository functionality.
    /// </summary>
    public class CurvePointRepository : RepositoryBase<CurvePoint>, ICurvePointRepository
    {
        public CurvePointRepository(PoseidonContext context) : base(context)
        {
        }
    }
}