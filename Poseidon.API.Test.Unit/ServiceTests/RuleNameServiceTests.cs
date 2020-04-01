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
    public class RuleNameServiceTests
    {
        private readonly IMapper _mapper;

        public RuleNameServiceTests()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfiles()); });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task TestGetAllRuleNamesAsync()
        {
            // Arrange
            IEnumerable<RuleName> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, null);

                // Act
                result = await service.GetAllRuleNamesAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<RuleName>(result.First());
        }

        [Fact]
        public async Task TestGetAllRuleNamesAsViewModelsAsync()
        {
            // Arrange
            IEnumerable<RuleNameViewModel> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                // Act
                result = await service.GetAllRuleNamesAsViewModelsAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<RuleNameViewModel>(result.First());
        }

        [Fact]
        public async Task TestGetRuleNameByIdAsyncIdValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, null);

                // Act
                var result = await service.GetRuleNameByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.IsAssignableFrom<RuleName>(result);
                Assert.Equal("one description", result.Description);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestGetRuleNameByIdAsViewModelAsyncIdValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                // Act
                var result = await service.GetRuleNameByIdAsViewModelASync(1);

                // Assert
                Assert.NotNull(result);
                Assert.IsAssignableFrom<RuleNameViewModel>(result);
                Assert.Equal("one description", result.Description);

                context.Database.EnsureDeleted();
            }
        }


        [Fact]
        public async Task TestGetRuleNameByIdAsyncIdInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, null);

                // Act
                var result = await service.GetRuleNameByIdAsync(100);

                // Assert
                Assert.Null(result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestGetRuleNameByIdAsViewModelAsyncIdInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                // Act
                var result = await service.GetRuleNameByIdAsViewModelASync(100);

                // Assert
                Assert.Null(result);

                context.Database.EnsureDeleted();
            }
        }


        [Fact]
        public async Task TestCreateRuleNameEntityNotNull()
        {
            // Arrange
            var testEntity = new RuleNameInputModel { Description = "test description" };

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                Assert.Empty(context.RuleName);

                // Act
                await service.CreateRuleName(testEntity);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Single(context.RuleName);
                Assert.Equal("test description", context.RuleName.First().Description);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestCreateRuleNameEntityNull()
        {
            // Arrange
            var service = new RuleNameService(null, null);

            // Act
            async Task TestAction() => await service.CreateRuleName(null);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
        }


        [Fact]
        public async Task TestUpdateRuleNameEntityNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                var inputModel = new RuleNameInputModel();

                // Act
                async Task TestAction() => await service.UpdateRuleName(100, inputModel);

                // Assert
                await Assert.ThrowsAsync<Exception>(TestAction);
            }
        }

        [Fact]
        public async Task TestDeleteRuleNameEntityNotNull()
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

                // Act
                await service.DeleteRuleName(1);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Equal(2, context.RuleName.Count());
                Assert.DoesNotContain(context.RuleName, bl => bl.Id == 1);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteRuleNameEntityNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, _mapper);

                // Act
                async Task TestAction() => await service.DeleteRuleName(100);

                // Assert
                await Assert.ThrowsAsync<Exception>(TestAction);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestRuleNameExistsIdValid()
        {
            // Arrange
            bool exists;

            var options = TestUtilities.BuildTestDbOptions();

            using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, null);

                // Act
                exists = service.RuleNameExists(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public void TestRuleNameExistsIdInvalid()
        {
            // Arrange
            bool exists;

            var options = TestUtilities.BuildTestDbOptions();

            using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                var service = new RuleNameService(repositoryWrapper, null);

                // Act
                exists = service.RuleNameExists(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.False(exists);
        }
    }
}