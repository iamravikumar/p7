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
    public class UserRepositoryTests
    {
        [Fact]
        public async Task TestGetAllAsync()
        {
            // Arrange
            IEnumerable<User> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                result =
                    await repositoryWrapper.UserRepository.GetAllAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<User>(result.First());
        }

        [Fact]
        public async Task TestGetByIdValidArgument()
        {
            // Arrange
            User result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                result =
                    await repositoryWrapper.UserRepository.GetByIdAsync(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.Equal("one username", result.Username);
        }

        [Fact]
        public async Task TestGetByIdInvalidArgument()
        {
            // Arrange
            User result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                result = await repositoryWrapper.UserRepository.GetByIdAsync(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestCreateUserArgumentNull()
        {
            // Arrange
            var repositoryWrapper = new UnitOfWork(null);

            // Act
            void TestAction() =>
                repositoryWrapper.UserRepository.CreateUser(null);

            // Assert
            Assert.Throws<ArgumentNullException>(TestAction);
        }

        [Fact]
        public async Task TestCreateUserArgumentNonNull()
        {
            // Arrange
            var testEntity = new User { Username = "test username" };

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                // Act
                repositoryWrapper.UserRepository.CreateUser(testEntity);

                await repositoryWrapper.CommitAsync();
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.NotEmpty(context.User);

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
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                exists = repositoryWrapper.UserRepository.Exists(1);

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
                TestUtilities.SeedTestDbUser(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                exists = repositoryWrapper.UserRepository.Exists(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.False(exists);
        }
    }
}