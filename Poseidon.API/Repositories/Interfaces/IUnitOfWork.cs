using System.Threading.Tasks;

namespace Poseidon.API.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUnitOfWork
    {
        IBidListRepository BidListRepository { get; }
        ICurvePointRepository CurvePointRepository { get; }
        IRatingRepository RatingRepository { get; }
        IRuleNameRepository RuleNameRepository { get; }
        ITradeRepository TradeRepository { get; }
        IUserRepository UserRepository { get; }

        Task CommitAsync();
        Task RollbackAsync();
    }
}