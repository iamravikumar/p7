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
using Xunit;

namespace Poseidon.Test
{
    public class CurvePointServiceTests
    {
        private readonly IMapper _mapper;

        public CurvePointServiceTests()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfiles()); });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task TestGetAllCurvePointsAsync()
        {
            // Arrange
            IEnumerable<CurvePoint> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);

                var unitOfWork = new UnitOfWork(context);

                var service = new CurvePointService(unitOfWork, null);

                // Act
                result = await service.GetAllCurvePointsAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<CurvePoint>(result.First());
        }

        [Fact]
        public async Task TestGetAllCurvePointsAsViewModelsAsync()
        {
            // Arrange
            IEnumerable<CurvePointViewModel> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);

                var unitOfWork = new UnitOfWork(context);

                var service = new CurvePointService(unitOfWork, _mapper);

                // Act
                result = await service.GetAllCurvePointsAsViewModelsAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<CurvePointViewModel>(result.First());
        }

        [Fact]
        public async Task TestGetCurvePointByIdAsyncIdValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);

                var unitOfWork = new UnitOfWork(context);

                var service = new CurvePointService(unitOfWork, null);

                // Act
                var result = await service.GetCurvePointByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.IsAssignableFrom<CurvePoint>(result);
                Assert.Equal(10D, result.Value);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestGetCurvePointByIdAsViewModelAsyncIdValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);

                var unitOfWork = new UnitOfWork(context);

                var service = new CurvePointService(unitOfWork, _mapper);

                // Act
                var result = await service.GetCurvePointByIdAsViewModelASync(1);

                // Assert
                Assert.NotNull(result);
                Assert.IsAssignableFrom<CurvePointViewModel>(result);
                Assert.Equal(10D, result.Value);

                context.Database.EnsureDeleted();
            }
        }


        [Fact]
        public async Task TestGetCurvePointByIdAsyncIdInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);

                var unitOfWork = new UnitOfWork(context);

                var service = new CurvePointService(unitOfWork, null);

                // Act
                var result = await service.GetCurvePointByIdAsync(100);

                // Assert
                Assert.Null(result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestGetCurvePointByIdAsViewModelAsyncIdInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);

                var unitOfWork = new UnitOfWork(context);

                var service = new CurvePointService(unitOfWork, _mapper);

                // Act
                var result = await service.GetCurvePointByIdAsViewModelASync(100);

                // Assert
                Assert.Null(result);

                context.Database.EnsureDeleted();
            }
        }


        [Fact]
        public async Task TestCreateCurvePointEntityNotNull()
        {
            // Arrange
            var testEntity = new CurvePointInputModel { Value = 100D };

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var unitOfWork = new UnitOfWork(context);

                var service = new CurvePointService(unitOfWork, _mapper);

                Assert.Empty(context.CurvePoint);

                // Act
                await service.CreateCurvePoint(testEntity);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Single(context.CurvePoint);
                Assert.Equal(100D, context.CurvePoint.First().Value);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestCreateCurvePointEntityNull()
        {
            // Arrange
            var service = new CurvePointService(null, null);

            // Act
            async Task TestAction() => await service.CreateCurvePoint(null);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
        }

        [Fact]
        public async Task TestUpdateCurvePointIdMismatch()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var unitOfWork = new UnitOfWork(context);

                var service = new CurvePointService(unitOfWork, _mapper);

                var inputModel = new CurvePointInputModel { Id = 10 };

                // Act
                async Task TestAction() => await service.UpdateCurvePoint(100, inputModel);

                // Assert
                await Assert.ThrowsAsync<ArgumentException>(TestAction);
            }
        }

        [Fact]
        public async Task TestUpdateCurvePointEntityNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var unitOfWork = new UnitOfWork(context);

                var service = new CurvePointService(unitOfWork, _mapper);

                var inputModel = new CurvePointInputModel { Id = 100 };

                // Act
                async Task TestAction() => await service.UpdateCurvePoint(100, inputModel);

                // Assert
                await Assert.ThrowsAsync<Exception>(TestAction);
            }
        }

        [Fact]
        public async Task TestDeleteCurvePointEntityNotNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);
            }

            await using (var context = new PoseidonContext(options))
            {
                var unitOfWork = new UnitOfWork(context);

                var service = new CurvePointService(unitOfWork, _mapper);

                // Act
                await service.DeleteCurvePoint(1);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Equal(2, context.CurvePoint.Count());
                Assert.DoesNotContain(context.CurvePoint, bl => bl.Id == 1);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteCurvePointEntityNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);

                var unitOfWork = new UnitOfWork(context);

                var service = new CurvePointService(unitOfWork, _mapper);

                // Act
                async Task TestAction() => await service.DeleteCurvePoint(100);

                // Assert
                await Assert.ThrowsAsync<Exception>(TestAction);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestCurvePointExistsIdValid()
        {
            // Arrange
            bool exists;

            var options = TestUtilities.BuildTestDbOptions();

            using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);
                
                var unitOfWork = new UnitOfWork(context);

                var service = new CurvePointService(unitOfWork, null);

                // Act
                exists = service.CurvePointExists(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public void TestCurvePointExistsIdInvalid()
        {
            // Arrange
            bool exists;

            var options = TestUtilities.BuildTestDbOptions();

            using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);
                
                var unitOfWork = new UnitOfWork(context);

                var service = new CurvePointService(unitOfWork, null);

                // Act
                exists = service.CurvePointExists(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.False(exists);
        }
    }
}