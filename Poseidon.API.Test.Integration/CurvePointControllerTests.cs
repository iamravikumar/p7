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
using Xunit;

namespace Poseidon.API.Test.Integration
{
    public class CurvePointControllerTests
    {
        private readonly IMapper _mapper;

        public CurvePointControllerTests()
        {
            var config =
                new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfiles()); });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task TestGetAllCurvePointPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<IEnumerable<CurvePointViewModel>> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new CurvePointService(repositoryWrapper, _mapper);

                var controller = new CurvePointController(service);

                // Act
                result = await controller.Get();

                context.Database.EnsureDeleted();
            }

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var objectResult = Assert.IsAssignableFrom<IEnumerable<CurvePointViewModel>>(actionResult.Value);
            Assert.Equal(3, objectResult.Count());
        }

        [Fact]
        public async Task TestGetAllCurvePointNotPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<IEnumerable<CurvePointViewModel>> result;

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new CurvePointService(repositoryWrapper, _mapper);

                var controller = new CurvePointController(service);

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

            ActionResult<CurvePointViewModel> result;

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new CurvePointService(repositoryWrapper, _mapper);

                var controller = new CurvePointController(service);

                // Act
                result = await controller.Get(0);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task TestGetCurvePointNotPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<CurvePointViewModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new CurvePointService(repositoryWrapper, _mapper);

                var controller = new CurvePointController(service);

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

            ActionResult<CurvePointViewModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new CurvePointService(repositoryWrapper, _mapper);

                var controller = new CurvePointController(service);

                // Act
                result = await controller.Get(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var objectResult = Assert.IsAssignableFrom<CurvePointViewModel>(actionResult.Value);
            Assert.Equal(10D, objectResult.Value);
        }

        [Fact]
        public async Task TestPostInputModelValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<CurvePointInputModel> result;

            var testEntity = new CurvePointInputModel
            {
                Value = 10D
            };

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new CurvePointService(repositoryWrapper, _mapper);

                var controller = new CurvePointController(service);

                Assert.Empty(context.CurvePoint);

                // Act
                result = await controller.Post(testEntity);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Single(context.CurvePoint);

                var actionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
                var objectResult = Assert.IsAssignableFrom<CurvePointInputModel>(actionResult.Value);
                Assert.Equal(10D, objectResult.Value);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestPostInputModelNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<CurvePointInputModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new CurvePointService(repositoryWrapper, _mapper);

                var controller = new CurvePointController(service);

                // Act
                result = await controller.Post(null);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result.Result);
        }


        [Fact]
        public async Task TestPutCurvePointDoesNotExist()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            var testModel = new CurvePointInputModel();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new CurvePointService(repositoryWrapper, _mapper);

                var controller = new CurvePointController(service);

                // Act
                var result = await controller.Put(10, testModel);

                // Assert
                var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result);
                Assert.Equal("No CurvePoint entity matching the id [10] was found.", objectResult.Value);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestPutValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            var testModel = new CurvePointInputModel
            {
                Value = 100D
            };

            ActionResult result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);
            }

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new CurvePointService(repositoryWrapper, _mapper);

                var controller = new CurvePointController(service);

                // Act
                result = await controller.Put(1, testModel);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.IsAssignableFrom<NoContentResult>(result);

                Assert.Equal(100D, context.CurvePoint.First(x => x.Id == 1).Value);

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

                var service = new CurvePointService(repositoryWrapper, _mapper);

                var controller = new CurvePointController(service);

                // Act
                var result = await controller.Delete(0);

                // Assert
                var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteCurvePointDoesNotExist()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new CurvePointService(repositoryWrapper, _mapper);

                var controller = new CurvePointController(service);

                // Act
                var result = await controller.Delete(10);

                // Assert
                var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteCurvePointExists()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);
            }

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new CurvePointService(repositoryWrapper, _mapper);

                var controller = new CurvePointController(service);

                // Act
                var result = await controller.Delete(1);

                // Assert
                Assert.IsAssignableFrom<NoContentResult>(result.Result);
                Assert.Equal(2, context.CurvePoint.Count());
                Assert.DoesNotContain(context.CurvePoint, x => x.Id == 1);

                context.Database.EnsureDeleted();
            }
        }
    }
}