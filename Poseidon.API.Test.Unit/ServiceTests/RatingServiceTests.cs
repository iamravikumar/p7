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
    public class RatingServiceTests
    {
        private readonly IMapper _mapper;

        public RatingServiceTests()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfiles()); });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task TestGetAllRatingsAsync()
        {
            // Arrange
            IEnumerable<Rating> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var unitOfWork = new UnitOfWork(context);

                var service = new RatingService(unitOfWork, null);

                // Act
                result = await service.GetAllRatingsAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<Rating>(result.First());
        }

        [Fact]
        public async Task TestGetAllRatingsAsInputModelsAsync()
        {
            // Arrange
            IEnumerable<RatingInputModel> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var unitOfWork = new UnitOfWork(context);

                var service = new RatingService(unitOfWork, _mapper);

                // Act
                result = await service.GetAllRatingsAsInputModelsAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<RatingInputModel>(result.First());
        }

        [Fact]
        public async Task TestGetRatingByIdAsyncIdValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var unitOfWork = new UnitOfWork(context);

                var service = new RatingService(unitOfWork, null);

                // Act
                var result = await service.GetRatingByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.IsAssignableFrom<Rating>(result);
                Assert.Equal("one rating", result.FitchRating);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestGetRatingByIdAsInputModelAsyncIdValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var unitOfWork = new UnitOfWork(context);

                var service = new RatingService(unitOfWork, _mapper);

                // Act
                var result = await service.GetRatingByIdAsInputModelASync(1);

                // Assert
                Assert.NotNull(result);
                Assert.IsAssignableFrom<RatingInputModel>(result);
                Assert.Equal("one rating", result.FitchRating);

                context.Database.EnsureDeleted();
            }
        }


        [Fact]
        public async Task TestGetRatingByIdAsyncIdInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var unitOfWork = new UnitOfWork(context);

                var service = new RatingService(unitOfWork, null);

                // Act
                var result = await service.GetRatingByIdAsync(100);

                // Assert
                Assert.Null(result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestGetRatingByIdAsInputModelAsyncIdInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var unitOfWork = new UnitOfWork(context);

                var service = new RatingService(unitOfWork, _mapper);

                // Act
                var result = await service.GetRatingByIdAsInputModelASync(100);

                // Assert
                Assert.Null(result);

                context.Database.EnsureDeleted();
            }
        }


        [Fact]
        public async Task TestCreateRatingEntityNotNull()
        {
            // Arrange
            var testEntity = new RatingInputModel { FitchRating = "test rating" };

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var unitOfWork = new UnitOfWork(context);

                var service = new RatingService(unitOfWork, _mapper);

                Assert.Empty(context.Rating);

                // Act
                await service.CreateRating(testEntity);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Single(context.Rating);
                Assert.Equal("test rating", context.Rating.First().FitchRating);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestCreateRatingEntityNull()
        {
            // Arrange
            var service = new RatingService(null, null);

            // Act
            async Task TestAction() => await service.CreateRating(null);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
        }


        [Fact]
        public async Task TestUpdateRatingEntityNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var unitOfWork = new UnitOfWork(context);

                var service = new RatingService(unitOfWork, _mapper);

                var inputModel = new RatingInputModel();

                // Act
                async Task TestAction() => await service.UpdateRating(100, inputModel);

                // Assert
                await Assert.ThrowsAsync<Exception>(TestAction);
            }
        }

        [Fact]
        public async Task TestDeleteRatingEntityNotNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);
            }

            await using (var context = new PoseidonContext(options))
            {
                var unitOfWork = new UnitOfWork(context);

                var service = new RatingService(unitOfWork, _mapper);

                // Act
                await service.DeleteRating(1);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Equal(2, context.Rating.Count());
                Assert.DoesNotContain(context.Rating, bl => bl.Id == 1);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteRatingEntityNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var unitOfWork = new UnitOfWork(context);

                var service = new RatingService(unitOfWork, _mapper);

                // Act
                async Task TestAction() => await service.DeleteRating(100);

                // Assert
                await Assert.ThrowsAsync<Exception>(TestAction);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestRatingExistsIdValid()
        {
            // Arrange
            bool exists;

            var options = TestUtilities.BuildTestDbOptions();

            using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var unitOfWork = new UnitOfWork(context);

                var service = new RatingService(unitOfWork, null);

                // Act
                exists = service.RatingExists(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public void TestRatingExistsIdInvalid()
        {
            // Arrange
            bool exists;

            var options = TestUtilities.BuildTestDbOptions();

            using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var unitOfWork = new UnitOfWork(context);

                var service = new RatingService(unitOfWork, null);

                // Act
                exists = service.RatingExists(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.False(exists);
        }
    }
}