using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Poseidon.API.Models;
using Poseidon.API.Repositories;
using Poseidon.API.Services.Interfaces;
using Poseidon.Shared.InputModels;

namespace Poseidon.API.Services
{
    public class TradeService : ITradeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TradeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Asynchronously gets all Trade entities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Trade>> GetAllTradesAsync() =>
            await _unitOfWork.TradeRepository.GetAllAsync();

        public async Task<IEnumerable<TradeInputModel>> GetAllTradesAsInputModelsAsync()
        {
            var entities = await _unitOfWork.TradeRepository.GetAllAsync();

            var inputModels = _mapper.Map<IEnumerable<Trade>, IEnumerable<TradeInputModel>>(entities);

            return inputModels;
        }

        /// <summary>
        /// Asynchronously gets a Trade entity by Id.
        /// </summary>
        /// <returns></returns>
        public async Task<Trade> GetTradeByIdAsync(int id) =>
            await _unitOfWork
                .TradeRepository
                .FindByCondition(bl => bl.Id == id)
                .FirstOrDefaultAsync();

        /// <summary>
        /// Asynchronously finds a Trade entity by id, and returns it as a TradeInputModel.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TradeInputModel> GetTradeByIdAsInputModelASync(int id)
        {
            var entity = await _unitOfWork.TradeRepository.GetByIdAsync(id);

            var inputModel = _mapper.Map<TradeInputModel>(entity);

            return inputModel;
        }

        /// <summary>
        /// Asynchronously creates a new Trade entity and persists it to the database. 
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> CreateTrade(TradeInputModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException();
            }

            var entity = _mapper.Map<Trade>(inputModel);

            try
            {
                _unitOfWork
                    .TradeRepository
                    .Insert(entity);

                await _unitOfWork.CommitAsync();
                return entity.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Asynchronously updates an existing Trade entity and persists any
        /// changes made to the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task UpdateTrade(int id, TradeInputModel inputModel)
        {
            var entity = await _unitOfWork.TradeRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception($"No {typeof(Trade)} entity matching the id [{id}] was found.");
            }

            _mapper.Map(inputModel, entity);

            try
            {
                _unitOfWork
                    .TradeRepository
                    .Update(entity);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Removes an existing Trade entity and persists the change to the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteTrade(int id)
        {
            var entity = await _unitOfWork.TradeRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception($"No {typeof(Trade)} entity matching the id '{id}' were found.");
            }

            try
            {
                _unitOfWork
                    .TradeRepository
                    .Delete(entity);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Confirms whether or not a given Trade entity exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool TradeExists(int id) =>
            _unitOfWork.TradeRepository.Exists(id);
    }
}