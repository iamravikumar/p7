using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.API.Models;
using Poseidon.Shared.InputModels;

namespace Poseidon.API.Services.Interfaces
{
    public interface ICurvePointService
    {
        Task<IEnumerable<CurvePoint>> GetAllCurvePointsAsync();
        Task<IEnumerable<CurvePointInputModel>> GetAllCurvePointsAsInputModelsAsync();
        Task<CurvePoint> GetCurvePointByIdAsync(int id);
        Task<CurvePointInputModel> GetCurvePointByIdAsInputModelASync(int id);
        Task<int> CreateCurvePoint(CurvePointInputModel model);
        Task UpdateCurvePoint(int id, CurvePointInputModel model);
        Task DeleteCurvePoint(int id);
        bool CurvePointExists(int id);
    }
}