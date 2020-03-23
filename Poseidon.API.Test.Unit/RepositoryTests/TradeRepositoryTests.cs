using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Poseidon.API.Data;
using Poseidon.API.Models;
using Poseidon.API.Repositories;
using Poseidon.API.Test.Shared;
using Xunit;

namespace Poseidon.Test
{
    public class TradeRepositoryTests
    {
        [Fact]
        public async Task TestGetAllAsync()
        {
            // Arrange
            IEnumerable<Trade> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                result =
                    await repositoryWrapper.TradeRepository.GetAllAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<Trade>(result.First());
        }

        [Fact]
        public async Task TestGetByIdValidArgument()
        {
            // Arrange
            Trade result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                result = 
                    await repositoryWrapper.TradeRepository.GetByIdAsync(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.Equal("one account", result.Account);
        }

        [Fact]
        public async Task TestGetByIdInvalidArgument()
        {
            // Arrange
            Trade result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                result = await repositoryWrapper.TradeRepository.GetByIdAsync(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestCreateTradeArgumentNull()
        {
            // Arrange
            Trade testEntity = null;

            var repositoryWrapper = new UnitOfWork(null);

            // Act
            void TestAction() =>
                repositoryWrapper.TradeRepository.CreateTrade(testEntity);

            // Assert
            Assert.Throws<ArgumentNullException>(TestAction);
        }

        [Fact]
        public async Task TestCreateTradeArgumentNonNull()
        {
            // Arrange
            var testEntity = new Trade { Account = "test account" };

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                // Act
                repositoryWrapper.TradeRepository.CreateTrade(testEntity);

                await repositoryWrapper.CommitAsync();
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.NotEmpty(context.Trade);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestExistsIdValid()
        {
            // Arrange
            bool exists;
            
            var options = TestUtilities.BuildTestDbOptions();
            
            using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);
                
                var repositoryWrapper = new UnitOfWork(context);

                // Act
                exists = repositoryWrapper.TradeRepository.Exists(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public void TestExistsIdInvalid()
        {
            // Arrange
            bool exists;
            
            var options = TestUtilities.BuildTestDbOptions();
            
            using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbTrade(context);
                
                var repositoryWrapper = new UnitOfWork(context);

                // Act
                exists = repositoryWrapper.TradeRepository.Exists(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.False(exists);
        }
    }
}