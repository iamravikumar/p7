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

// ReSharper disable ConvertToUsingDeclaration

namespace Poseidon.API.Test.Integration
{
    public class BidListControllerTests
    {
        private readonly IMapper _mapper;

        public BidListControllerTests()
        {
            var config =
                new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfiles()); });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task TestGetAllBidListPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<IEnumerable<BidListViewModel>> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new BidListService(repositoryWrapper, _mapper);

                var controller = new BidListController(service);

                // Act
                result = await controller.Get();

                context.Database.EnsureDeleted();
            }

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var objectResult = Assert.IsAssignableFrom<IEnumerable<BidListViewModel>>(actionResult.Value);
            Assert.Equal(3, objectResult.Count());
        }

        [Fact]
        public async Task TestGetAllBidListNotPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<IEnumerable<BidListViewModel>> result;

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new BidListService(repositoryWrapper, _mapper);

                var controller = new BidListController(service);

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

            ActionResult<BidListViewModel> result;

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new BidListService(repositoryWrapper, _mapper);

                var controller = new BidListController(service);

                // Act
                result = await controller.Get(0);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task TestGetBidListNotPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<BidListViewModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new BidListService(repositoryWrapper, _mapper);

                var controller = new BidListController(service);

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

            ActionResult<BidListViewModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new BidListService(repositoryWrapper, _mapper);

                var controller = new BidListController(service);

                // Act
                result = await controller.Get(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var objectResult = Assert.IsAssignableFrom<BidListViewModel>(actionResult.Value);
            Assert.Equal("one account", objectResult.Account);
        }

        [Fact]
        public async Task TestPostInputModelValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<BidListInputModel> result;

            var testEntity = new BidListInputModel
            {
                Account = "test account"
            };

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new BidListService(repositoryWrapper, _mapper);

                var controller = new BidListController(service);

                Assert.Empty(context.BidList);

                // Act
                result = await controller.Post(testEntity);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Single(context.BidList);

                var actionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
                var objectResult = Assert.IsAssignableFrom<BidListInputModel>(actionResult.Value);
                Assert.Equal("test account", objectResult.Account);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestPostInputModelNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<BidListInputModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new BidListService(repositoryWrapper, _mapper);

                var controller = new BidListController(service);

                // Act
                result = await controller.Post(null);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result.Result);
        }


        [Fact]
        public async Task TestPutBidListDoesNotExist()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            var testModel = new BidListInputModel();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new BidListService(repositoryWrapper, _mapper);

                var controller = new BidListController(service);

                // Act
                var result = await controller.Put(10, testModel);

                // Assert
                var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result);
                Assert.Equal("No BidList entity matching the id [10] was found.", objectResult.Value);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestPutValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            var testModel = new BidListInputModel
            {
                Account = "updated account"
            };

            ActionResult result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);
            }

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new BidListService(repositoryWrapper, _mapper);

                var controller = new BidListController(service);

                // Act
                result = await controller.Put(1, testModel);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.IsAssignableFrom<NoContentResult>(result);

                Assert.Equal("updated account", context.BidList.First(x => x.Id == 1).Account);

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

                var service = new BidListService(repositoryWrapper, _mapper);

                var controller = new BidListController(service);

                // Act
                var result = await controller.Delete(0);

                // Assert
                var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteBidListDoesNotExist()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new BidListService(repositoryWrapper, _mapper);

                var controller = new BidListController(service);

                // Act
                var result = await controller.Delete(10);

                // Assert
                var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteBidListExists()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);
            }

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new BidListService(repositoryWrapper, _mapper);

                var controller = new BidListController(service);

                // Act
                var result = await controller.Delete(1);

                // Assert
                Assert.IsAssignableFrom<NoContentResult>(result.Result);
                Assert.Equal(2, context.BidList.Count());
                Assert.DoesNotContain(context.BidList, x => x.Id == 1);

                context.Database.EnsureDeleted();
            }
        }
    }
}