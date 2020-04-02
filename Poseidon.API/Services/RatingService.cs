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
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RatingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Asynchronously gets all Rating entities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Rating>> GetAllRatingsAsync() =>
            await _unitOfWork.RatingRepository.GetAllAsync();

        public async Task<IEnumerable<RatingInputModel>> GetAllRatingsAsInputModelsAsync()
        {
            var entities = await _unitOfWork.RatingRepository.GetAllAsync();

            var inputModels = _mapper.Map<IEnumerable<Rating>, IEnumerable<RatingInputModel>>(entities);

            return inputModels;
        }

        /// <summary>
        /// Asynchronously gets a Rating entity by Id.
        /// </summary>
        /// <returns></returns>
        public async Task<Rating> GetRatingByIdAsync(int id) =>
            await _unitOfWork
                .RatingRepository
                .FindByCondition(bl => bl.Id == id)
                .FirstOrDefaultAsync();

        /// <summary>
        /// Asynchronously finds a Rating entity by id, and returns it as a RatingInputModel.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RatingInputModel> GetRatingByIdAsInputModelASync(int id)
        {
            var entity = await _unitOfWork.RatingRepository.GetByIdAsync(id);

            var inputModel = _mapper.Map<RatingInputModel>(entity);

            return inputModel;
        }

        /// <summary>
        /// Asynchronously creates a new Rating entity and persists it to the database. 
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> CreateRating(RatingInputModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException();
            }

            var entity = _mapper.Map<Rating>(inputModel);

            try
            {
                _unitOfWork
                    .RatingRepository
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
        /// Asynchronously updates an existing Rating entity and persists any
        /// changes made to the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task UpdateRating(int id, RatingInputModel inputModel)
        {
            var entity = await _unitOfWork.RatingRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception($"No {typeof(Rating)} entity matching the id [{id}] was found.");
            }

            _mapper.Map(inputModel, entity);

            try
            {
                _unitOfWork
                    .RatingRepository
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
        /// Removes an existing Rating entity and persists the change to the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteRating(int id)
        {
            var entity = await _unitOfWork.RatingRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception($"No {typeof(Rating)} entity matching the id '{id}' were found.");
            }

            try
            {
                _unitOfWork
                    .RatingRepository
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
        /// Confirms whether or not a given Rating entity exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RatingExists(int id) =>
            _unitOfWork.RatingRepository.Exists(id);
    }
}