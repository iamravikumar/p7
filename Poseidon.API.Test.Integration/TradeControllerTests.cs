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
    public class TradeControllerTests
    {
        private readonly IMapper _mapper;

        public TradeControllerTests()
        {
            var config =
                new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfiles()); });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task TestGetAllTradePopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<IEnumerable<TradeViewModel>> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                var controller = new TradeController(service);

                // Act
                result = await controller.Get();

                context.Database.EnsureDeleted();
            }

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var objectResult = Assert.IsAssignableFrom<IEnumerable<TradeViewModel>>(actionResult.Value);
            Assert.Equal(3, objectResult.Count());
        }

        [Fact]
        public async Task TestGetAllTradeNotPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<IEnumerable<TradeViewModel>> result;

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                var controller = new TradeController(service);

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

            ActionResult<TradeViewModel> result;

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                var controller = new TradeController(service);

                // Act
                result = await controller.Get(0);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task TestGetTradeNotPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<TradeViewModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                var controller = new TradeController(service);

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

            ActionResult<TradeViewModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                var controller = new TradeController(service);

                // Act
                result = await controller.Get(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var objectResult = Assert.IsAssignableFrom<TradeViewModel>(actionResult.Value);
            Assert.Equal("one account", objectResult.Account);
        }

        [Fact]
        public async Task TestPostInputModelValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<TradeInputModel> result;

            var testEntity = new TradeInputModel
            {
                Account = "test account"
            };

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                var controller = new TradeController(service);

                Assert.Empty(context.Trade);

                // Act
                result = await controller.Post(testEntity);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Single(context.Trade);

                var actionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
                var objectResult = Assert.IsAssignableFrom<TradeInputModel>(actionResult.Value);
                Assert.Equal("test account", objectResult.Account);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestPostInputModelNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<TradeInputModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                var controller = new TradeController(service);

                // Act
                result = await controller.Post(null);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result.Result);
        }


        [Fact]
        public async Task TestPutTradeDoesNotExist()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            var testModel = new TradeInputModel();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                var controller = new TradeController(service);

                // Act
                var result = await controller.Put(10, testModel);

                // Assert
                var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result);
                Assert.Equal("No Trade entity matching the id [10] was found.", objectResult.Value);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestPutValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            var testModel = new TradeInputModel
            {
                Account = "updated account"
            };

            ActionResult result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);
            }

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                var controller = new TradeController(service);

                // Act
                result = await controller.Put(1, testModel);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.IsAssignableFrom<NoContentResult>(result);

                Assert.Equal("updated account", context.Trade.First(x => x.Id == 1).Account);

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

                var service = new TradeService(repositoryWrapper, _mapper);

                var controller = new TradeController(service);

                // Act
                var result = await controller.Delete(0);

                // Assert
                var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteTradeDoesNotExist()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                var controller = new TradeController(service);

                // Act
                var result = await controller.Delete(10);

                // Assert
                var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteTradeExists()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);
            }

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                var controller = new TradeController(service);

                // Act
                var result = await controller.Delete(1);

                // Assert
                Assert.IsAssignableFrom<NoContentResult>(result.Result);
                Assert.Equal(2, context.Trade.Count());
                Assert.DoesNotContain(context.Trade, x => x.Id == 1);

                context.Database.EnsureDeleted();
            }
        }
    }
}