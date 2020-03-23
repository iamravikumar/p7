using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.API.Models;

namespace Poseidon.API.Services.Interfaces
{
    public interface ITradeService
    {
        Task<IEnumerable<Trade>> GetAllTradesAsync();
        Task<IEnumerable<TradeViewModel>> GetAllTradesAsViewModelsAsync();
        Task<Trade> GetTradeByIdAsync(int id);
        Task<TradeViewModel> GetTradeByIdAsViewModelASync(int id);
        Task<int> CreateTrade(TradeInputModel inputModel);
        Task UpdateTrade(int id, TradeInputModel inputModel);
        Task DeleteTrade(int id);
        bool TradeExists(int id);
    }
}