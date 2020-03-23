﻿using System;
using Microsoft.EntityFrameworkCore;
using Poseidon.API.Data;
using Poseidon.API.Models;

namespace Poseidon.API.Test.Shared
{
    public static class TestUtilities
    {
        public static DbContextOptions<PoseidonContext> BuildTestDbOptions() =>
            new DbContextOptionsBuilder<PoseidonContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

        public static void SeedTestDbBidList(PoseidonContext context)
        {
            context.AddRange(
                new BidList { Id = 1, Account = "one account" },
                new BidList { Id = 2, Account = "two account" },
                new BidList { Id = 3, Account = "three account" }
            );

            context.SaveChanges();
        }

        public static void SeedTestDbCurvePoint(PoseidonContext context)
        {
            context.AddRange(new CurvePoint
                {
                    Id = 1,
                    Value = 10D
                },
                new CurvePoint
                {
                    Id = 2,
                    Value = 20D
                },
                new CurvePoint
                {
                    Id = 3,
                    Value = 30D
                });

            context.SaveChanges();
        }
    }
}