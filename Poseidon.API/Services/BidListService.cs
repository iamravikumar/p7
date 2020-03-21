using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Poseidon.API.Models;
using Poseidon.API.Repositories;
using Poseidon.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Poseidon.API.Services
{
    public class BidListService : IBidListService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public BidListService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        /// <summary>
        /// Asynchronously gets all BidList entities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BidList>> GetAllBidListsAsync() =>
            await _repositoryWrapper.BidListRepository.GetAllAsync();

        public async Task<IEnumerable<BidListViewModel>> GetAllBidListsAsViewModelsAsync()
        {
            var entities = await _repositoryWrapper.BidListRepository.GetAllAsync();

            var viewModels = _mapper.Map<IEnumerable<BidList>, IEnumerable<BidListViewModel>>(entities);

            return viewModels;
        }

        /// <summary>
        /// Asynchronously gets a BidList entity by Id.
        /// </summary>
        /// <returns></returns>
        public async Task<BidList> GetBidListByIdAsync(int id) =>
            await _repositoryWrapper
                .BidListRepository
                .FindByCondition(bl => bl.Id == id)
                .FirstOrDefaultAsync();

        public async Task<BidListViewModel> GetBidListByIdAsViewModelASync(int id)
        {
            var entity = await _repositoryWrapper.BidListRepository.GetByIdAsync(id);

            var viewModel = _mapper.Map<BidListViewModel>(entity);

            return viewModel;
        }

        public async Task<int> CreateBidList(BidListInputModel inputModel)
        {
            if (inputModel == null)
                throw new ArgumentNullException();

            var entity = _mapper.Map<BidList>(inputModel);
            
            _repositoryWrapper
                .BidListRepository
                .Create(entity);

            await _repositoryWrapper.SaveAsync();

            return entity.Id;
        }

        public async Task UpdateBidList(int id, BidListInputModel inputModel)
        {
            if (id != inputModel.Id)
                throw new ArgumentException("Id mismatch.");
            
            var entity = await _repositoryWrapper.BidListRepository.GetByIdAsync(id);

            if (entity == null)
                throw new Exception($"No {typeof(BidList)} entity matching the id [{id}] was found.");

            _mapper.Map(inputModel, entity);

            _repositoryWrapper
                .BidListRepository
                .Update(entity);

            await _repositoryWrapper.SaveAsync();
        }

        public async Task DeleteBidList(int id)
        {
            var entity = await _repositoryWrapper.BidListRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception($"No {typeof(BidList)} entity matching the id '{id}' were found.");
            }

            _repositoryWrapper
                .BidListRepository
                .Delete(entity);

            await _repositoryWrapper.SaveAsync();
        }

        public bool BidListExists(int id) =>
            _repositoryWrapper.BidListRepository.Exists(id);
    }
}