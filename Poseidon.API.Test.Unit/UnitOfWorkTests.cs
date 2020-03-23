using Poseidon.API.Repositories;
using Xunit;

namespace Poseidon.Test
{
    public class UnitOfWorkTests
    {
        [Fact]
        public void TestBidListRepository()
        {
            // Arrange
            var repositoryWrapper = new UnitOfWork(null);

            // Act
            var result = repositoryWrapper.BidListRepository;

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IBidListRepository>(result);
        }
        
        [Fact]
        public void TestCurvePointRepository()
        {
            // Arrange
            var repositoryWrapper = new UnitOfWork(null);

            // Act
            var result = repositoryWrapper.CurvePointRepository;

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ICurvePointRepository>(result);
        }
        
        [Fact]
        public void TestRatingRepository()
        {
            // Arrange
            var repositoryWrapper = new UnitOfWork(null);

            // Act
            var result = repositoryWrapper.RatingRepository;

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IRatingRepository>(result);
        }
        
        [Fact]
        public void TestRuleNameRepository()
        {
            // Arrange
            var repositoryWrapper = new UnitOfWork(null);

            // Act
            var result = repositoryWrapper.RuleNameRepository;

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IRuleNameRepository>(result);
        }
        
        [Fact]
        public void TestTradeRepository()
        {
            // Arrange
            var repositoryWrapper = new UnitOfWork(null);

            // Act
            var result = repositoryWrapper.TradeRepository;

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ITradeRepository>(result);
        }
        
        [Fact]
        public void TestUserRepository()
        {
            // Arrange
            var repositoryWrapper = new UnitOfWork(null);

            // Act
            var result = repositoryWrapper.UserRepository;

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IUserRepository>(result);
        }
    }
}