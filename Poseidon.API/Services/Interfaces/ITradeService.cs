using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.API.Models;
using Poseidon.Shared.InputModels;

namespace Poseidon.API.Services.Interfaces
{
    public interface ITradeService
    {
        Task<IEnumerable<Trade>> GetAllTradesAsync();
        Task<IEnumerable<TradeInputModel>> GetAllTradesAsInputModelsAsync();
        Task<Trade> GetTradeByIdAsync(int id);
        Task<TradeInputModel> GetTradeByIdAsInputModelASync(int id);
        Task<int> CreateTrade(TradeInputModel inputModel);
        Task UpdateTrade(int id, TradeInputModel inputModel);
        Task DeleteTrade(int id);
        bool TradeExists(int id);
    }
}