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
    public class RuleNameControllerTests
    {
        private readonly IMapper _mapper;

        public RuleNameControllerTests()
        {
            var config =
                new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfiles()); });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task TestGetAllRuleNamePopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<IEnumerable<RuleNameInputModel>> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                var controller = new RuleNameController(service);

                // Act
                result = await controller.Get();

                context.Database.EnsureDeleted();
            }

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var objectResult = Assert.IsAssignableFrom<IEnumerable<RuleNameInputModel>>(actionResult.Value);
            Assert.Equal(3, objectResult.Count());
        }

        [Fact]
        public async Task TestGetAllRuleNameNotPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<IEnumerable<RuleNameInputModel>> result;

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                var controller = new RuleNameController(service);

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

            ActionResult<RuleNameInputModel> result;

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                var controller = new RuleNameController(service);

                // Act
                result = await controller.Get(0);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task TestGetRuleNameNotPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<RuleNameInputModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                var controller = new RuleNameController(service);

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

            ActionResult<RuleNameInputModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                var controller = new RuleNameController(service);

                // Act
                result = await controller.Get(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var objectResult = Assert.IsAssignableFrom<RuleNameInputModel>(actionResult.Value);
            Assert.Equal("one description", objectResult.Description);
        }

        [Fact]
        public async Task TestPostInputModelValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<RuleNameInputModel> result;

            var testEntity = new RuleNameInputModel
            {
                Description = "test description"
            };

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                var controller = new RuleNameController(service);

                Assert.Empty(context.RuleName);

                // Act
                result = await controller.Post(testEntity);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Single(context.RuleName);

                var actionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
                var objectResult = Assert.IsAssignableFrom<RuleNameInputModel>(actionResult.Value);
                Assert.Equal("test description", objectResult.Description);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestPostInputModelNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<RuleNameInputModel> result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                var controller = new RuleNameController(service);

                // Act
                result = await controller.Post(null);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result.Result);
        }


        [Fact]
        public async Task TestPutRuleNameDoesNotExist()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            var testModel = new RuleNameInputModel();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                var controller = new RuleNameController(service);

                // Act
                var result = await controller.Put(10, testModel);

                // Assert
                var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result);
                Assert.Equal("No RuleName entity matching the id [10] was found.", objectResult.Value);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestPutValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            var testModel = new RuleNameInputModel
            {
                Description = "updated description"
            };

            ActionResult result;

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);
            }

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                var controller = new RuleNameController(service);

                // Act
                result = await controller.Put(1, testModel);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.IsAssignableFrom<NoContentResult>(result);

                Assert.Equal("updated description", context.RuleName.First(x => x.Id == 1).Description);

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

                var service = new RuleNameService(repositoryWrapper, _mapper);

                var controller = new RuleNameController(service);

                // Act
                var result = await controller.Delete(0);

                // Assert
                var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteRuleNameDoesNotExist()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                var controller = new RuleNameController(service);

                // Act
                var result = await controller.Delete(10);

                // Assert
                var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteRuleNameExists()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);
            }

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                var controller = new RuleNameController(service);

                // Act
                var result = await controller.Delete(1);

                // Assert
                Assert.IsAssignableFrom<NoContentResult>(result.Result);
                Assert.Equal(2, context.RuleName.Count());
                Assert.DoesNotContain(context.RuleName, x => x.Id == 1);

                context.Database.EnsureDeleted();
            }
        }
    }
}