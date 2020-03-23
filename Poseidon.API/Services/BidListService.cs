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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BidListService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Asynchronously gets all BidList entities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BidList>> GetAllBidListsAsync() =>
            await _unitOfWork.BidListRepository.GetAllAsync();

        public async Task<IEnumerable<BidListViewModel>> GetAllBidListsAsViewModelsAsync()
        {
            var entities = await _unitOfWork.BidListRepository.GetAllAsync();

            var viewModels = _mapper.Map<IEnumerable<BidList>, IEnumerable<BidListViewModel>>(entities);

            return viewModels;
        }

        /// <summary>
        /// Asynchronously gets a BidList entity by Id.
        /// </summary>
        /// <returns></returns>
        public async Task<BidList> GetBidListByIdAsync(int id) =>
            await _unitOfWork
                .BidListRepository
                .FindByCondition(bl => bl.Id == id)
                .FirstOrDefaultAsync();

        /// <summary>
        /// Asynchronously finds a BidList entity by id, and returns it as a BidListViewModel.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BidListViewModel> GetBidListByIdAsViewModelASync(int id)
        {
            var entity = await _unitOfWork.BidListRepository.GetByIdAsync(id);

            var viewModel = _mapper.Map<BidListViewModel>(entity);

            return viewModel;
        }

        /// <summary>
        /// Asynchronously creates a new BidList entity and persists it to the database. 
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> CreateBidList(BidListInputModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException();
            }

            var entity = _mapper.Map<BidList>(inputModel);

            try
            {
                _unitOfWork
                    .BidListRepository
                    .Insert(entity);

                await _unitOfWork.CommitAsync();
                return entity.Id;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Asynchronously updates an existing BidList entity and persists any
        /// changes made to the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task UpdateBidList(int id, BidListInputModel inputModel)
        {
            var entity = await _unitOfWork.BidListRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception($"No {typeof(BidList)} entity matching the id [{id}] was found.");
            }

            _mapper.Map(inputModel, entity);

            try
            {
                _unitOfWork
                    .BidListRepository
                    .Update(entity);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Removes an existing BidList entity and persists the change to the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteBidList(int id)
        {
            var entity = await _unitOfWork.BidListRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception($"No {typeof(BidList)} entity matching the id '{id}' were found.");
            }

            try
            {
                _unitOfWork
                    .BidListRepository
                    .Delete(entity);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Confirms whether or not a given BidList entity exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool BidListExists(int id) =>
            _unitOfWork.BidListRepository.Exists(id);
    }
}