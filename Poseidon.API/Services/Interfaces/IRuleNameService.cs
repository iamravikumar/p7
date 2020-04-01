using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.API.Models;
using Poseidon.Shared.InputModels;

namespace Poseidon.API.Services.Interfaces
{
    public interface IRuleNameService
    {
        Task<IEnumerable<RuleName>> GetAllRuleNamesAsync();
        Task<IEnumerable<RuleNameInputModel>> GetAllRuleNamesAsInputModelsAsync();
        Task<RuleName> GetRuleNameByIdAsync(int id);
        Task<RuleNameInputModel> GetRuleNameByIdAsInputModelASync(int id);
        Task<int> CreateRuleName(RuleNameInputModel inputModel);
        Task UpdateRuleName(int id, RuleNameInputModel inputModel);
        Task DeleteRuleName(int id);
        bool RuleNameExists(int id);
    }
}