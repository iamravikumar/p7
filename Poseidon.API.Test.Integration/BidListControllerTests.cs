using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Poseidon.API.Controllers;
using Poseidon.API.Data;
using Poseidon.API.Models;
using Poseidon.API.Profiles;
using Poseidon.API.Repositories;
using Poseidon.API.Services;
using Poseidon.API.Test.Shared;
using Xunit;

// ReSharper disable ConvertToUsingDeclaration

namespace Poseidon.API.Test.Integration
{
    public class BidListControllerTests
    {
        private readonly IMapper _mapper;

        public BidListControllerTests()
        {
            var config = 
                new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MappingProfiles());
                });

            _mapper = config.CreateMapper();
        }
        
        [Fact]
        public async Task TestGetAllBidListPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<IEnumerable<BidListViewModel>> result;
            
            await using (var context = new PoseidonContext(options))
            {
                TestUtilities.SeedTestDbBidList(context);
                
                var repositoryWrapper = new RepositoryWrapper(context);
                
                var service = new BidListService(repositoryWrapper, _mapper);
                
                var controller = new BidListController(service);

                // Act
                result = await controller.Get();

                context.Database.EnsureDeleted();
            }

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var objectResult = Assert.IsAssignableFrom<IEnumerable<BidListViewModel>>(actionResult.Value);
            Assert.Equal(3, objectResult.Count());
        }

        [Fact]
        public async Task TestGetAllBidListNotPopulated()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ActionResult<IEnumerable<BidListViewModel>> result;
            
            await using (var context = new PoseidonContext(options))
            {
                var repositoryWrapper = new RepositoryWrapper(context);
                
                var service = new BidListService(repositoryWrapper, _mapper);
                
                var controller = new BidListController(service);

                // Act
                result = await controller.Get();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

    }
}