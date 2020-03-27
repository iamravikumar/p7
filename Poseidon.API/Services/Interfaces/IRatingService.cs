using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.API.Models;
using Poseidon.Shared.InputModels;

namespace Poseidon.API.Services.Interfaces
{
    public interface IRatingService
    {
        Task<IEnumerable<Rating>> GetAllRatingsAsync();
        Task<IEnumerable<RatingViewModel>> GetAllRatingsAsViewModelsAsync();
        Task<Rating> GetRatingByIdAsync(int id);
        Task<RatingViewModel> GetRatingByIdAsViewModelASync(int id);
        Task<int> CreateRating(RatingInputModel inputModel);
        Task UpdateRating(int id, RatingInputModel inputModel);
        Task DeleteRating(int id);
        bool RatingExists(int id);
    }
}