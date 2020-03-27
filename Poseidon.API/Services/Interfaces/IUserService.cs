using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.API.Models;
using Poseidon.Shared.InputModels;

namespace Poseidon.API.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<UserViewModel>> GetAllUsersAsViewModelsAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<UserViewModel> GetUserByIdAsViewModelASync(int id);
        Task<int> CreateUser(UserInputModel inputModel);
        Task UpdateUser(int id, UserInputModel inputModel);
        Task DeleteUser(int id);
        bool UserExists(int id);
    }
}