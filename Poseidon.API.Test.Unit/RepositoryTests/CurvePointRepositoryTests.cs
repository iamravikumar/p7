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
    public class CurvePointRepositoryTests
    {
        [Fact]
        public async Task TestGetAllAsync()
        {
            // Arrange
            IEnumerable<CurvePoint> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                result =
                    await repositoryWrapper.CurvePointRepository.GetAllAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<CurvePoint>(result.First());
        }

        [Fact]
        public async Task TestGetByIdValidArgument()
        {
            // Arrange
            CurvePoint result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                result = 
                    await repositoryWrapper.CurvePointRepository.GetByIdAsync(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10D, result.Value);
        }

        [Fact]
        public async Task TestGetByIdInvalidArgument()
        {
            // Arrange
            CurvePoint result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbCurvePoint(context);

                var repositoryWrapper = new UnitOfWork(context);

                // Act
                result = await repositoryWrapper.CurvePointRepository.GetByIdAsync(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestCreateCurvePointArgumentNull()
        {
            // Arrange
            CurvePoint testEntity = null;

            var repositoryWrapper = new UnitOfWork(null);

            // Act
            void TestAction() =>
                repositoryWrapper.CurvePointRepository.CreateCurvePoint(testEntity);

            // Assert
            Assert.Throws<ArgumentNullException>(TestAction);
        }

        [Fact]
        public async Task TestCreateCurvePointArgumentNonNull()
        {
            // Arrange
            var testEntity = new CurvePoint { Value = 100D };

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new UnitOfWork(context);

                // Act
                repositoryWrapper.CurvePointRepository.CreateCurvePoint(testEntity);

                await repositoryWrapper.CommitAsync();
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.NotEmpty(context.CurvePoint);
                Assert.Equal(100D, context.CurvePoint.First().Value);

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
                TestUtilities.SeedTestDbCurvePoint(context);
                
                var repositoryWrapper = new UnitOfWork(context);

                // Act
                exists = repositoryWrapper.CurvePointRepository.Exists(1);

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
                TestUtilities.SeedTestDbCurvePoint(context);
                
                var repositoryWrapper = new UnitOfWork(context);

                // Act
                exists = repositoryWrapper.CurvePointRepository.Exists(100);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.False(exists);
        }
    }
}