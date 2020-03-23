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
    public class RatingControllerTests
    {
        private readonly IMapper _mapper;

        public RatingControllerTests()
        {
            var config =
                new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfiles()); });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task TestGetAllRatingPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<IEnumerable<RatingViewModel>> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RatingService(repositoryWrapper, _mapper);

                var controller = new RatingController(service);

                // Act
                result = await controller.Get();

                context.Database.EnsureDeleted();
            }

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var objectResult = Assert.IsAssignableFrom<IEnumerable<RatingViewModel>>(actionResult.Value);
            Assert.Equal(3, objectResult.Count());
        }

        [Fact]
        public async Task TestGetAllRatingNotPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<IEnumerable<RatingViewModel>> result;

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new RatingService(repositoryWrapper, _mapper);

                var controller = new RatingController(service);

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

            ActionResult<RatingViewModel> result;

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new RatingService(repositoryWrapper, _mapper);

                var controller = new RatingController(service);

                // Act
                result = await controller.Get(0);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task TestGetRatingNotPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<RatingViewModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RatingService(repositoryWrapper, _mapper);

                var controller = new RatingController(service);

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

            ActionResult<RatingViewModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RatingService(repositoryWrapper, _mapper);

                var controller = new RatingController(service);

                // Act
                result = await controller.Get(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var objectResult = Assert.IsAssignableFrom<RatingViewModel>(actionResult.Value);
            Assert.Equal("one rating", objectResult.FitchRating);
        }

        [Fact]
        public async Task TestPostInputModelValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<RatingInputModel> result;

            var testEntity = new RatingInputModel
            {
                FitchRating = "test rating"
            };

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new RatingService(repositoryWrapper, _mapper);

                var controller = new RatingController(service);

                Assert.Empty(context.Rating);

                // Act
                result = await controller.Post(testEntity);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Single(context.Rating);

                var actionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
                var objectResult = Assert.IsAssignableFrom<RatingInputModel>(actionResult.Value);
                Assert.Equal("test rating", objectResult.FitchRating);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestPostInputModelNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<RatingInputModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RatingService(repositoryWrapper, _mapper);

                var controller = new RatingController(service);

                // Act
                result = await controller.Post(null);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result.Result);
        }


        [Fact]
        public async Task TestPutRatingDoesNotExist()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            var testModel = new RatingInputModel
            {
                FitchRating = "test rating"
            };

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RatingService(repositoryWrapper, _mapper);

                var controller = new RatingController(service);

                // Act
                var result = await controller.Put(10, testModel);

                // Assert
                var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result);
                Assert.Equal("No Rating entity matching the id [10] was found.", objectResult.Value);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestPutValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            var testModel = new RatingInputModel
            {
                FitchRating = "updated rating"
            };

            ActionResult result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);
            }

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new RatingService(repositoryWrapper, _mapper);

                var controller = new RatingController(service);

                // Act
                result = await controller.Put(1, testModel);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.IsAssignableFrom<NoContentResult>(result);

                Assert.Equal("updated rating", context.Rating.First(x => x.Id == 1).FitchRating);

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

                var service = new RatingService(repositoryWrapper, _mapper);

                var controller = new RatingController(service);

                // Act
                var result = await controller.Delete(0);

                // Assert
                var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteRatingDoesNotExist()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RatingService(repositoryWrapper, _mapper);

                var controller = new RatingController(service);

                // Act
                var result = await controller.Delete(10);

                // Assert
                var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteRatingExists()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRating(context);
            }

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new RatingService(repositoryWrapper, _mapper);

                var controller = new RatingController(service);

                // Act
                var result = await controller.Delete(1);

                // Assert
                Assert.IsAssignableFrom<NoContentResult>(result.Result);
                Assert.Equal(2, context.Rating.Count());
                Assert.DoesNotContain(context.Rating, x => x.Id == 1);

                context.Database.EnsureDeleted();
            }
        }
    }
}