using System.Threading.Tasks;
using Poseidon.API.Data;

namespace Poseidon.API.Repositories
{
    /// <summary>
    /// Wrapper class providing access to concrete entity repository classes.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PoseidonContext _context;
        private IBidListRepository _bidListRepository;
        private ICurvePointRepository _curvePointRepository;
        private IRatingRepository _ratingRepository;
        private IRuleNameRepository _ruleNameRepository;
        private ITradeRepository _tradeRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(PoseidonContext context)
        {
            _context = context;
        }

        public IBidListRepository BidListRepository =>
            _bidListRepository ??= new BidListRepository(_context);

        public ICurvePointRepository CurvePointRepository =>
            _curvePointRepository ??= new CurvePointRepository(_context);

        public IRatingRepository RatingRepository =>
            _ratingRepository ??= new RatingRepository(_context);

        public IRuleNameRepository RuleNameRepository =>
            _ruleNameRepository ??= new RuleNameRepository(_context);

        public ITradeRepository TradeRepository =>
            _tradeRepository ??= new TradeRepository(_context);

        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(_context);

        /// <summary>
        /// Persists all local changes tracked by the context.
        /// </summary>
        public async Task CommitAsync() =>
            await _context.SaveChangesAsync();

        /// <summary>
        /// Rolls back any local changes tracked by the context.
        /// </summary>
        /// <returns></returns>
        public async Task RollbackAsync() =>
            await _context.DisposeAsync();
    }
}