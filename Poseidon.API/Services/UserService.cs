using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Poseidon.API.Models;
using Poseidon.API.Repositories;
using Poseidon.Shared.InputModels;

namespace Poseidon.API.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Asynchronously gets all User entities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync() =>
            await _unitOfWork.UserRepository.GetAllAsync();

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsViewModelsAsync()
        {
            var entities = await _unitOfWork.UserRepository.GetAllAsync();

            var viewModels = _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(entities);

            return viewModels;
        }

        /// <summary>
        /// Asynchronously gets a User entity by Id.
        /// </summary>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(int id) =>
            await _unitOfWork
                .UserRepository
                .FindByCondition(bl => bl.Id == id)
                .FirstOrDefaultAsync();

        /// <summary>
        /// Asynchronously finds a User entity by id, and returns it as a UserViewModel.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserViewModel> GetUserByIdAsViewModelASync(int id)
        {
            var entity = await _unitOfWork.UserRepository.GetByIdAsync(id);

            var viewModel = _mapper.Map<UserViewModel>(entity);

            return viewModel;
        }

        /// <summary>
        /// Asynchronously creates a new User entity and persists it to the database. 
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> CreateUser(UserInputModel inputModel)
        {
            if (inputModel == null)
            {
                throw new ArgumentNullException();
            }

            var entity = _mapper.Map<User>(inputModel);

            try
            {
                _unitOfWork
                    .UserRepository
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
        /// Asynchronously updates an existing User entity and persists any
        /// changes made to the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task UpdateUser(int id, UserInputModel inputModel)
        {
            var entity = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception($"No {typeof(User)} entity matching the id [{id}] was found.");
            }

            _mapper.Map(inputModel, entity);

            try
            {
                _unitOfWork
                    .UserRepository
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
        /// Removes an existing User entity and persists the change to the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteUser(int id)
        {
            var entity = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception($"No {typeof(User)} entity matching the id '{id}' were found.");
            }

            try
            {
                _unitOfWork
                    .UserRepository
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
        /// Confirms whether or not a given User entity exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UserExists(int id) =>
            _unitOfWork.UserRepository.Exists(id);
    }
}