using Poseidon.API.Data;

namespace Poseidon.API.Repositories
{
    /// <summary>
    /// Wrapper class providing access to concrete entity repository classes.
    /// </summary>
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly PoseidonContext _context;
        private IBidListRepository _bidListRepository;
        private ICurvePointRepository _curvePointRepository;
        private IRatingRepository _ratingRepository;
        private IRuleNameRepository _ruleNameRepository;
        private ITradeRepository _tradeRepository;
        private IUserRepository _userRepository;

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

        public RepositoryWrapper(PoseidonContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Saves all changes made to the context to the database.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}