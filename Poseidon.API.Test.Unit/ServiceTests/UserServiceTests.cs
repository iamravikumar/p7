using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Poseidon.API.Data;
using Poseidon.API.Models;
using Poseidon.API.Repositories;
using Poseidon.API.Services;
using Poseidon.API.Test.Shared;
using Poseidon.Shared.InputModels;
using Xunit;
// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable ConvertToUsingDeclaration

namespace Poseidon.Test
{
    public class UserServiceTests
    {
        private readonly IMapper _mapper;

        public UserServiceTests()
        {
            var config = 
                    new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new MappingProfiles());
                    });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task TestGetAllUsersAsync()
        {
            // Arrange
            IEnumerable<User> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, null);

                // Act
                result = await service.GetAllUsersAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<User>(result.First());
        }

        [Fact]
        public async Task TestGetAllUsersAsInputModelsAsync()
        {
            // Arrange
            IEnumerable<UserInputModel> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                // Act
                result = await service.GetAllUsersAsInputModelsAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<UserInputModel>(result.First());
        }

        [Fact]
        public async Task TestGetUserByIdAsyncIdValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, null);

                // Act
                var result = await service.GetUserByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.IsAssignableFrom<User>(result);
                Assert.Equal("one username", result.Username);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestGetUserByIdAsInputModelAsyncIdValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                // Act
                var result = await service.GetUserByIdAsInputModelASync(1);

                // Assert
                Assert.NotNull(result);
                Assert.IsAssignableFrom<UserInputModel>(result);
                Assert.Equal("one username", result.Username);

                context.Database.EnsureDeleted();
            }
        }


        [Fact]
        public async Task TestGetUserByIdAsyncIdInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, null);

                // Act
                var result = await service.GetUserByIdAsync(100);

                // Assert
                Assert.Null(result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestGetUserByIdAsInputModelAsyncIdInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                // Act
                var result = await service.GetUserByIdAsInputModelASync(100);

                // Assert
                Assert.Null(result);

                context.Database.EnsureDeleted();
            }
        }


        [Fact]
        public async Task TestCreateUserEntityNotNull()
        {
            // Arrange
            var testEntity = new UserInputModel { Username = "test username" };

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                Assert.Empty(context.User);

                // Act
                await service.CreateUser(testEntity);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Single(context.User);
                Assert.Equal("test username", context.User.First().Username);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestCreateUserEntityNull()
        {
            // Arrange
            var service = new UserService(null, null);

            // Act
            async Task TestAction() => await service.CreateUser(null);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
        }


        [Fact]
        public async Task TestUpdateUserEntityNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                var inputModel = new UserInputModel();

                // Act
                async Task TestAction() => await service.UpdateUser(100, inputModel);

                // Assert
                await Assert.ThrowsAsync<Exception>(TestAction);
            }
        }

        [Fact]
        public async Task TestDeleteUserEntityNotNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);
            }

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                // Act
                await service.DeleteUser(1);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Equal(2, context.User.Count());
                Assert.DoesNotContain(context.User, bl => bl.Id == 1);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteUserEntityNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                // Act
                async Task TestAction() => await service.DeleteUser(100);

                // Assert
                await Assert.ThrowsAsync<Exception>(TestAction);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestUserExistsIdValid()
        {
            // Arrange
            bool exists;

            var options = TestUtilities.BuildTestDbOptions();

            using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, null);

                // Act
                exists = service.UserExists(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public void TestUserExistsIdInvalid()
        {
            // Arrange
            bool exists;

            var options = TestUtilities.BuildTestDbOptions();

            using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, null);

                // Act
                exists = service.UserExists(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.False(exists);
        }
    }
}