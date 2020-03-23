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
    public class RatingRepositoryTests
    {
        [Fact]
        public async Task TestGetAllAsync()
        {
            // Arrange
            IEnumerable<Rating> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                result =
                    await repositoryWrapper.RatingRepository.GetAllAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<Rating>(result.First());
        }

        [Fact]
        public async Task TestGetByIdValidArgument()
        {
            // Arrange
            Rating result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                result = 
                    await repositoryWrapper.RatingRepository.GetByIdAsync(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.Equal("one rating", result.FitchRating);
        }

        [Fact]
        public async Task TestGetByIdInvalidArgument()
        {
            // Arrange
            Rating result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                result = await repositoryWrapper.RatingRepository.GetByIdAsync(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestCreateRatingArgumentNull()
        {
            // Arrange
            Rating testEntity = null;

            var repositoryWrapper = new UnitOfWork(null);

            // Act
            void TestAction() =>
                repositoryWrapper.RatingRepository.CreateRating(testEntity);

            // Assert
            Assert.Throws<ArgumentNullException>(TestAction);
        }

        [Fact]
        public async Task TestCreateRatingArgumentNonNull()
        {
            // Arrange
            var testEntity = new Rating { FitchRating = "test rating"};

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                // Act
                repositoryWrapper.RatingRepository.CreateRating(testEntity);

                await repositoryWrapper.CommitAsync();
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.NotEmpty(context.Rating);
                Assert.Equal("test rating", context.Rating.First().FitchRating);

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
                TestUtilities.SeedTestDbRating(context);
                
                var repositoryWrapper = new UnitOfWork(context);

                // Act
                exists = repositoryWrapper.RatingRepository.Exists(1);

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
                TestUtilities.SeedTestDbRating(context);
                
                var repositoryWrapper = new UnitOfWork(context);

                // Act
                exists = repositoryWrapper.RatingRepository.Exists(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.False(exists);
        }
    }
}