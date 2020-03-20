using System;
using Microsoft.EntityFrameworkCore;
using Poseidon.API.Data;

namespace Poseidon.Test
{
    internal static class TestUtilities
    {
        internal static DbContextOptions<PoseidonContext> BuildTestDbOptions() =>
            new DbContextOptionsBuilder<PoseidonContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
    }
}