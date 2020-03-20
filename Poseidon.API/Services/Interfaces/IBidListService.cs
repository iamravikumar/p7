using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.API.Models;

namespace Poseidon.API.Services.Interfaces
{
    public interface IBidListService
    {
        Task<IEnumerable<BidList>> GetAllBidListsAsync();
        Task<BidList> GetBidListByIdAsync(int id);
        Task CreateBidList(BidList entity);
        Task UpdateBidList(BidList entity);
        Task DeleteBidList(BidList entity);
    }
}