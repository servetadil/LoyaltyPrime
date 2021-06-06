using LoyaltyPrime.Domain.Repository;
using LoyaltyPrime.Infrastructure;
using LoyaltyPrime.Infrastructure.DatabaseFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoyaltyPrime.Application.UnitTests.Base
{
    public class TestBase : IDisposable
    {
        protected readonly ApplicationDbContext _context;

        public TestBase()
        {
            _context = ApplicationDbContextFactory.Create();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(_context);
        }
    }
}
