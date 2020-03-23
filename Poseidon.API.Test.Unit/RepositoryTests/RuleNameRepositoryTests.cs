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
    public class RuleNameRepositoryTests
    {
        [Fact]
        public async Task TestGetAllAsync()
        {
            // Arrange
            IEnumerable<RuleName> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                result =
                    await repositoryWrapper.RuleNameRepository.GetAllAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<RuleName>(result.First());
        }

        [Fact]
        public async Task TestGetByIdValidArgument()
        {
            // Arrange
            RuleName result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                result =
                    await repositoryWrapper.RuleNameRepository.GetByIdAsync(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.Equal("one description", result.Description);
        }

        [Fact]
        public async Task TestGetByIdInvalidArgument()
        {
            // Arrange
            RuleName result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                result = await repositoryWrapper.RuleNameRepository.GetByIdAsync(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestCreateRuleNameArgumentNull()
        {
            // Arrange
            RuleName testEntity = null;

            var repositoryWrapper = new UnitOfWork(null);

            // Act
            void TestAction() =>
                repositoryWrapper.RuleNameRepository.CreateRuleName(testEntity);

            // Assert
            Assert.Throws<ArgumentNullException>(TestAction);
        }

        [Fact]
        public async Task TestCreateRuleNameArgumentNonNull()
        {
            // Arrange
            var testEntity = new RuleName { Description = "test description" };

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                // Act
                repositoryWrapper.RuleNameRepository.CreateRuleName(testEntity);

                await repositoryWrapper.CommitAsync();
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.NotEmpty(context.RuleName);
                Assert.Equal("test description", context.RuleName.First().Description);

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
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                exists = repositoryWrapper.RuleNameRepository.Exists(1);

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
                TestUtilities.SeedTestDbRuleName(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                exists = repositoryWrapper.RuleNameRepository.Exists(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.False(exists);
        }
    }
}