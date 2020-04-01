using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.API.Models;
using Poseidon.Shared.InputModels;

namespace Poseidon.API.Services.Interfaces
{
    public interface IBidListService
    {
        Task<IEnumerable<BidList>> GetAllBidListsAsync();
        Task<IEnumerable<BidListInputModel>> GetAllBidListsAsInputModelsAsync();
        Task<BidList> GetBidListByIdAsync(int id);
        Task<BidListInputModel> GetBidListByIdAsInputModelASync(int id);
        Task<int> CreateBidList(BidListInputModel inputModel);
        Task UpdateBidList(int id, BidListInputModel inputModel);
        Task DeleteBidList(int id);
        bool BidListExists(int id);
    }
}