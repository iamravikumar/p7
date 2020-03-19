using System.Threading.Tasks;

namespace Poseidon.API.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRepositoryWrapper
    {
        IBidListRepository BidListRepository { get; }
        ICurvePointRepository CurvePointRepository { get; }
        IRatingRepository RatingRepository { get; }
        IRuleNameRepository RuleNameRepository { get; }
        ITradeRepository TradeRepository { get; }
        IUserRepository UserRepository { get; }

        Task SaveAsync();
    }
}