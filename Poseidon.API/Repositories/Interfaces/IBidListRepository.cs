using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        Task<BidList> GetByIdAsync(int id);
        void CreateBidList(BidList entity);
        bool Exists(int id);
    }
}