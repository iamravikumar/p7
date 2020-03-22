using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Poseidon.API.Data;
using Poseidon.API.Models;
using Poseidon.API.Repositories;
using Poseidon.API.Test.Shared;
using Xunit;

// ReSharper disable ConvertToUsingDeclaration

namespace Poseidon.Test
{
    [Collection("BidList")]
    public class BidListRepositoryTests
    {
        [Fact]
        public async Task TestGetAllAsync()
        {
            // Arrange
            IEnumerable<BidList> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);

                var repositoryWrapper = new RepositoryWrapper(context);

                // Act
                result =
                    await repositoryWrapper.BidListRepository.GetAllAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<BidList>(result.First());
        }

        [Fact]
        public async Task TestGetByIdValidArgument()
        {
            // Arrange
            BidList result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);

                var repositoryWrapper = new RepositoryWrapper(context);

                // Act
                result = 
                    await repositoryWrapper.BidListRepository.GetByIdAsync(1);

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
            BidList result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);

                var repositoryWrapper = new RepositoryWrapper(context);

                // Act
                result = await repositoryWrapper.BidListRepository.GetByIdAsync(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestCreateBidListArgumentNull()
        {
            // Arrange
            BidList testEntity = null;

            var repositoryWrapper = new RepositoryWrapper(null);

            // Act
            void TestAction() =>
                repositoryWrapper.BidListRepository.CreateBidList(testEntity);

            // Assert
            Assert.Throws<ArgumentNullException>(TestAction);
        }

        [Fact]
        public async Task TestCreateBidListArgumentNonNull()
        {
            // Arrange
            var testEntity = new BidList { Account = "test account" };

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new RepositoryWrapper(context);

                // Act
                repositoryWrapper.BidListRepository.CreateBidList(testEntity);

                await repositoryWrapper.SaveAsync();
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.NotEmpty(context.BidList);

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
                TestUtilities.SeedTestDbBidList(context);
                
                var repositoryWrapper = new RepositoryWrapper(context);

                // Act
                exists = repositoryWrapper.BidListRepository.Exists(1);

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
                TestUtilities.SeedTestDbBidList(context);
                
                var repositoryWrapper = new RepositoryWrapper(context);

                // Act
                exists = repositoryWrapper.BidListRepository.Exists(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.False(exists);
        }

    }
}