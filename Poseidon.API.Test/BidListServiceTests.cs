﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Poseidon.API.Data;
using Poseidon.API.Models;
using Poseidon.API.Profiles;
using Poseidon.API.Repositories;
using Poseidon.API.Services;
using Xunit;

namespace Poseidon.Test
{
    [SuppressMessage("ReSharper", "ConvertToUsingDeclaration")]
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    [Collection("BidList")]
    public class BidListServiceTests
    {
        private readonly IMapper _mapper;

        public BidListServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            });
            
            _mapper = config.CreateMapper();

        }
        
        [Fact]
        public async Task TestGetAllBidListsAsync()
        {
            // Arrange
            IEnumerable<BidList> result;

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);

                var repositoryWrapper = new RepositoryWrapper(context);

                var service = new BidListService(repositoryWrapper, null);

                // Act
                result = await service.GetAllBidListsAsync();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<BidList>(result.First());
        }

        [Fact]
        public async Task TestGetBidListByIdAsyncIdValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);

                var repositoryWrapper = new RepositoryWrapper(context);

                var service = new BidListService(repositoryWrapper, null);

                // Act
                var result = await service.GetBidListByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.IsAssignableFrom<BidList>(result);
                Assert.Equal("one account", result.Account);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestGetBidListByIdAsyncIdInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);

                var repositoryWrapper = new RepositoryWrapper(context);

                var service = new BidListService(repositoryWrapper, null);

                // Act
                var result = await service.GetBidListByIdAsync(100);

                // Assert
                Assert.Null(result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestCreateBidListEntityNotNull()
        {
            // Arrange
            var testEntity = new BidListInputModel { Account = "test account" };

            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new RepositoryWrapper(context);

                var service = new BidListService(repositoryWrapper, _mapper);

                Assert.Empty(context.BidList);

                // Act
                await service.CreateBidList(testEntity);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Single(context.BidList);
                Assert.Equal("test account", context.BidList.First().Account);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestCreateBidListEntityNull()
        {
            // Arrange
            var service = new BidListService(null, null);

            // Act
            async Task TestAction() => await service.CreateBidList(null);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
        }

        [Fact]
        public async Task TestUpdateBidListIdMismatch()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new RepositoryWrapper(context);
                
                var service = new BidListService(repositoryWrapper, _mapper);

                var inputModel = new BidListInputModel { Id = 10 };
            
                // Act
                async Task TestAction() => await service.UpdateBidList(100, inputModel);

                // Assert
                await Assert.ThrowsAsync<ArgumentException>(TestAction);
            }
        }

        [Fact]
        public async Task TestUpdateBidListEntityNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new RepositoryWrapper(context);
                
                var service = new BidListService(repositoryWrapper, _mapper);

                var inputModel = new BidListInputModel { Id = 100 };
            
                // Act
                async Task TestAction() => await service.UpdateBidList(100, inputModel);

                // Assert
                await Assert.ThrowsAsync<Exception>(TestAction);
            }
        }

        [Fact]
        public async Task TestDeleteBidListEntityNotNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);
            }

            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new RepositoryWrapper(context);

                var service = new BidListService(repositoryWrapper, _mapper);

                // Act
                await service.DeleteBidList(1);
            }

            await using (var context = new PoseidonContext(options))
            {
                // Assert
                Assert.Equal(2, context.BidList.Count());
                Assert.DoesNotContain(context.BidList, bl => bl.Id == 1);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestDeleteBidListEntityNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);

                var repositoryWrapper = new RepositoryWrapper(context);

                var service = new BidListService(repositoryWrapper, _mapper);

                // Act
                async Task TestAction() => await service.DeleteBidList(100);

                // Assert
                await Assert.ThrowsAsync<Exception>(TestAction);

                context.Database.EnsureDeleted();
            }
        }
    }
}