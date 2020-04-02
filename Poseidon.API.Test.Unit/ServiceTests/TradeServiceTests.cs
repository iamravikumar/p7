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

namespace Poseidon.Test
{
    public class TradeServiceTests
    {
        private readonly IMapper _mapper;

        public TradeServiceTests()
        {
            var config = 
                    new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new MappingProfiles());
                    });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task TestGetAllTradesAsync()
        {
            // Arrange
            IEnumerable<Trade> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, null);

                // Act
                result = await service.GetAllTradesAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<Trade>(result.First());
        }

        [Fact]
        public async Task TestGetAllTradesAsInputModelsAsync()
        {
            // Arrange
            IEnumerable<TradeInputModel> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                // Act
                result = await service.GetAllTradesAsInputModelsAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<TradeInputModel>(result.First());
        }

        [Fact]
        public async Task TestGetTradeByIdAsyncIdValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, null);

                // Act
                var result = await service.GetTradeByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.IsAssignableFrom<Trade>(result);
                Assert.Equal("one account", result.Account);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestGetTradeByIdAsInputModelAsyncIdValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                // Act
                var result = await service.GetTradeByIdAsInputModelASync(1);

                // Assert
                Assert.NotNull(result);
                Assert.IsAssignableFrom<TradeInputModel>(result);
                Assert.Equal("one account", result.Account);

                context.Database.EnsureDeleted();
            }
        }


        [Fact]
        public async Task TestGetTradeByIdAsyncIdInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, null);

                // Act
                var result = await service.GetTradeByIdAsync(100);

                // Assert
                Assert.Null(result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestGetTradeByIdAsInputModelAsyncIdInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                // Act
                var result = await service.GetTradeByIdAsInputModelASync(100);

                // Assert
                Assert.Null(result);

                context.Database.EnsureDeleted();
            }
        }


        [Fact]
        public async Task TestCreateTradeEntityNotNull()
        {
            // Arrange
            var testEntity = new TradeInputModel { Account = "test account" };

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                Assert.Empty(context.Trade);

                // Act
                await service.CreateTrade(testEntity);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Single(context.Trade);
                Assert.Equal("test account", context.Trade.First().Account);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestCreateTradeEntityNull()
        {
            // Arrange
            var service = new TradeService(null, null);

            // Act
            async Task TestAction() => await service.CreateTrade(null);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
        }


        [Fact]
        public async Task TestUpdateTradeEntityNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                var inputModel = new TradeInputModel();

                // Act
                async Task TestAction() => await service.UpdateTrade(100, inputModel);

                // Assert
                await Assert.ThrowsAsync<Exception>(TestAction);
            }
        }

        [Fact]
        public async Task TestDeleteTradeEntityNotNull()
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

                // Act
                await service.DeleteTrade(1);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Equal(2, context.Trade.Count());
                Assert.DoesNotContain(context.Trade, bl => bl.Id == 1);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteTradeEntityNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, _mapper);

                // Act
                async Task TestAction() => await service.DeleteTrade(100);

                // Assert
                await Assert.ThrowsAsync<Exception>(TestAction);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestTradeExistsIdValid()
        {
            // Arrange
            bool exists;

            var options = TestUtilities.BuildTestDbOptions();

            using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, null);

                // Act
                exists = service.TradeExists(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public void TestTradeExistsIdInvalid()
        {
            // Arrange
            bool exists;

            var options = TestUtilities.BuildTestDbOptions();

            using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new TradeService(repositoryWrapper, null);

                // Act
                exists = service.TradeExists(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.False(exists);
        }
    }
}