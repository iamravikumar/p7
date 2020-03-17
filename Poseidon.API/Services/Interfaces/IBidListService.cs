using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.API.Models;

namespace Poseidon.API.Services.Interfaces
{
    public interface IBidListService
    {
        Task<IEnumerable<BidList>> GetAllBidListsAsync();
        Task<BidList> GetBidListByIdAsync(int id);
    }
}