using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.API.Models;
using Poseidon.API.Repositories;
using Poseidon.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Poseidon.API.Services
{
    public class BidListService : IBidListService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public BidListService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Asynchronously gets all BidList entities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BidList>> GetAllBidListsAsync() =>
            await _repositoryWrapper.BidListRepository.GetAllAsync();

        /// <summary>
        /// Asynchronously gets a BidList entity by Id.
        /// </summary>
        /// <returns></returns>
        public async Task<BidList> GetBidListByIdAsync(int id) =>
            await _repositoryWrapper
                .BidListRepository
                .FindByCondition(bl => bl.Id == id)
                .FirstOrDefaultAsync();

        public async Task CreateBidList(BidList entity)
        {
            _repositoryWrapper
                .BidListRepository
                .Create(entity);
            
            await _repositoryWrapper.SaveAsync();
        }

        public async Task UpdateBidList(BidList entity)
        {
            _repositoryWrapper
                .BidListRepository
                .Update(entity);

            await _repositoryWrapper.SaveAsync();
        }

        public async Task DeleteBidList(BidList entity)
        {
            _repositoryWrapper
                .BidListRepository
                .Delete(entity);

            await _repositoryWrapper.SaveAsync();
        }
    }
}