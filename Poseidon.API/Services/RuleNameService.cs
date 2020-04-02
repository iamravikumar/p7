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
    public class RuleNameService : IRuleNameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RuleNameService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Asynchronously gets all RuleName entities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RuleName>> GetAllRuleNamesAsync() =>
            await _unitOfWork.RuleNameRepository.GetAllAsync();

        public async Task<IEnumerable<RuleNameInputModel>> GetAllRuleNamesAsInputModelsAsync()
        {
            var entities = await _unitOfWork.RuleNameRepository.GetAllAsync();

            var inputModels = _mapper.Map<IEnumerable<RuleName>, IEnumerable<RuleNameInputModel>>(entities);

            return inputModels;
        }

        /// <summary>
        /// Asynchronously gets a RuleName entity by Id.
        /// </summary>
        /// <returns></returns>
        public async Task<RuleName> GetRuleNameByIdAsync(int id) =>
            await _unitOfWork
                .RuleNameRepository
                .FindByCondition(bl => bl.Id == id)
                .FirstOrDefaultAsync();

        /// <summary>
        /// Asynchronously finds a RuleName entity by id, and returns it as a RuleNameInputModel.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RuleNameInputModel> GetRuleNameByIdAsInputModelASync(int id)
        {
            var entity = await _unitOfWork.RuleNameRepository.GetByIdAsync(id);

            var inputModel = _mapper.Map<RuleNameInputModel>(entity);

            return inputModel;
        }

        /// <summary>
        /// Asynchronously creates a new RuleName entity and persists it to the database. 
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> CreateRuleName(RuleNameInputModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException();
            }

            var entity = _mapper.Map<RuleName>(inputModel);

            try
            {
                _unitOfWork
                    .RuleNameRepository
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
        /// Asynchronously updates an existing RuleName entity and persists any
        /// changes made to the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task UpdateRuleName(int id, RuleNameInputModel inputModel)
        {
            var entity = await _unitOfWork.RuleNameRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception($"No {typeof(RuleName)} entity matching the id [{id}] was found.");
            }

            _mapper.Map(inputModel, entity);

            try
            {
                _unitOfWork
                    .RuleNameRepository
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
        /// Removes an existing RuleName entity and persists the change to the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteRuleName(int id)
        {
            var entity = await _unitOfWork.RuleNameRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception($"No {typeof(RuleName)} entity matching the id '{id}' were found.");
            }

            try
            {
                _unitOfWork
                    .RuleNameRepository
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
        /// Confirms whether or not a given RuleName entity exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RuleNameExists(int id) =>
            _unitOfWork.RuleNameRepository.Exists(id);
    }
}