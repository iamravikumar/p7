using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.API.Models;

namespace Poseidon.API.Services.Interfaces
{
    public interface IRuleNameService
    {
        Task<IEnumerable<RuleName>> GetAllRuleNamesAsync();
        Task<IEnumerable<RuleNameViewModel>> GetAllRuleNamesAsViewModelsAsync();
        Task<RuleName> GetRuleNameByIdAsync(int id);
        Task<RuleNameViewModel> GetRuleNameByIdAsViewModelASync(int id);
        Task<int> CreateRuleName(RuleNameInputModel inputModel);
        Task UpdateRuleName(int id, RuleNameInputModel inputModel);
        Task DeleteRuleName(int id);
        bool RuleNameExists(int id);
    }
}