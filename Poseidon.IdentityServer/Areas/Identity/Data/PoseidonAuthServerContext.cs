using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Poseidon.Client.Areas.Identity.Data
{
    public class PoseidonAuthServerContext : IdentityDbContext<PoseidonAuthServerUser>
    {
        public PoseidonAuthServerContext(DbContextOptions<PoseidonAuthServerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}