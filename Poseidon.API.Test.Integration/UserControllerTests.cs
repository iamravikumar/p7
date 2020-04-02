using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Poseidon.API.Controllers;
using Poseidon.API.Data;
using Poseidon.API.Models;
using Poseidon.API.Repositories;
using Poseidon.API.Services;
using Poseidon.API.Test.Shared;
using Poseidon.Shared.InputModels;
using Xunit;

namespace Poseidon.API.Test.Integration
{
    public class UserControllerTests
    {
        private readonly IMapper _mapper;

        public UserControllerTests()
        {
            var config =
                new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfiles()); });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task TestGetAllUserPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<IEnumerable<UserInputModel>> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                var controller = new UserController(service);

                // Act
                result = await controller.Get();

                context.Database.EnsureDeleted();
            }

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var objectResult = Assert.IsAssignableFrom<IEnumerable<UserInputModel>>(actionResult.Value);
            Assert.Equal(3, objectResult.Count());
        }

        [Fact]
        public async Task TestGetAllUserNotPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<IEnumerable<UserInputModel>> result;

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                var controller = new UserController(service);

                // Act
                result = await controller.Get();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task TestGetIdBad()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<UserInputModel> result;

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                var controller = new UserController(service);

                // Act
                result = await controller.Get(0);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task TestGetUserNotPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<UserInputModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                var controller = new UserController(service);

                // Act
                result = await controller.Get(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task TestGetIdValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<UserInputModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                var controller = new UserController(service);

                // Act
                result = await controller.Get(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var objectResult = Assert.IsAssignableFrom<UserInputModel>(actionResult.Value);
            Assert.Equal("one username", objectResult.Username);
        }

        [Fact]
        public async Task TestPostInputModelValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<UserInputModel> result;

            var testEntity = new UserInputModel
            {
                Username = "test username"
            };

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                var controller = new UserController(service);

                Assert.Empty(context.User);

                // Act
                result = await controller.Post(testEntity);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Single(context.User);

                var actionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
                var objectResult = Assert.IsAssignableFrom<UserInputModel>(actionResult.Value);
                Assert.Equal("test username", objectResult.Username);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestPostInputModelNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<UserInputModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                var controller = new UserController(service);

                // Act
                result = await controller.Post(null);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result.Result);
        }


        [Fact]
        public async Task TestPutUserDoesNotExist()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            var testModel = new UserInputModel();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                var controller = new UserController(service);

                // Act
                var result = await controller.Put(10, testModel);

                // Assert
                var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result);
                Assert.Equal("No User entity matching the id [10] was found.", objectResult.Value);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestPutValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            var testModel = new UserInputModel
            {
                Username = "updated username"
            };

            ActionResult result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);
            }

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                var controller = new UserController(service);

                // Act
                result = await controller.Put(1, testModel);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.IsAssignableFrom<NoContentResult>(result);

                Assert.Equal("updated username", context.User.First(x => x.Id == 1).Username);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteIdInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                var controller = new UserController(service);

                // Act
                var result = await controller.Delete(0);

                // Assert
                var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteUserDoesNotExist()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new UserService(repositoryWrapper, _mapper);

                var controller = new UserController(service);

                // Act
                var result = await controller.Delete(10);

                // Assert
                var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteUserExists()
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

                var controller = new UserController(service);

                // Act
                var result = await controller.Delete(1);

                // Assert
                Assert.IsAssignableFrom<NoContentResult>(result.Result);
                Assert.Equal(2, context.User.Count());
                Assert.DoesNotContain(context.User, x => x.Id == 1);

                context.Database.EnsureDeleted();
            }
        }
    }
}