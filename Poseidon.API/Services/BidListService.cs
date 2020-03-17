using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.API.Models;
using Poseidon.API.Repositories;
using Poseidon.API.Services.Interfaces;
using System.Linq;

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
        public async Task<IEnumerable<BidList>> GetAllBidListsAsync()
        {
            var result = await _repositoryWrapper.BidListRepository.GetAllAsync();

            return result;
        }

        /// <summary>
        /// Asynchronously gets a BidList entity by Id.
        /// </summary>
        /// <returns></returns>
        public async Task<BidList> GetBidListByIdAsync(int id)
        {
            var result =
                await _repositoryWrapper
                    .BidListRepository
                    .GetByConditionAsync(bl => bl.Id == id);
                    
            return result.FirstOrDefault();
        }
    }
}