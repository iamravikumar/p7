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
    public class CurvePointService : ICurvePointService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CurvePointService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Asynchronously gets all CurvePoint entities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CurvePoint>> GetAllCurvePointsAsync() =>
            await _unitOfWork.CurvePointRepository.GetAllAsync();

        public async Task<IEnumerable<CurvePointInputModel>> GetAllCurvePointsAsInputModelsAsync()
        {
            var entities = await _unitOfWork.CurvePointRepository.GetAllAsync();

            var inputModels = _mapper.Map<IEnumerable<CurvePoint>, IEnumerable<CurvePointInputModel>>(entities);

            return inputModels;
        }

        /// <summary>
        /// Asynchronously gets a CurvePoint entity by Id.
        /// </summary>
        /// <returns></returns>
        public async Task<CurvePoint> GetCurvePointByIdAsync(int id) =>
            await _unitOfWork
                .CurvePointRepository
                .FindByCondition(bl => bl.Id == id)
                .FirstOrDefaultAsync();

        /// <summary>
        /// Asynchronously finds a CurvePoint entity by id, and returns it as a CurvePointInputModel.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CurvePointInputModel> GetCurvePointByIdAsInputModelASync(int id)
        {
            var entity = await _unitOfWork.CurvePointRepository.GetByIdAsync(id);

            var inputModel = _mapper.Map<CurvePointInputModel>(entity);

            return inputModel;
        }

        /// <summary>
        /// Asynchronously creates a new CurvePoint entity and persists it to the database. 
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> CreateCurvePoint(CurvePointInputModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException();
            }

            var entity = _mapper.Map<CurvePoint>(inputModel);

            try
            {
                _unitOfWork
                    .CurvePointRepository
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
        /// Asynchronously updates an existing CurvePoint entity and persists any
        /// changes made to the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task UpdateCurvePoint(int id, CurvePointInputModel inputModel)
        {
            var entity = await _unitOfWork.CurvePointRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception($"No {typeof(CurvePoint)} entity matching the id [{id}] was found.");
            }

            _mapper.Map(inputModel, entity);

            try
            {
                _unitOfWork
                    .CurvePointRepository
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
        /// Removes an existing CurvePoint entity and persists the change to the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteCurvePoint(int id)
        {
            var entity = await _unitOfWork.CurvePointRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception($"No {typeof(CurvePoint)} entity matching the id '{id}' were found.");
            }

            try
            {
                _unitOfWork
                    .CurvePointRepository
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
        /// Confirms whether or not a given CurvePoint entity exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CurvePointExists(int id) =>
            _unitOfWork.CurvePointRepository.Exists(id);
    }
}