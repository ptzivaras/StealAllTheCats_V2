using System;
using Microsoft.EntityFrameworkCore;
using StealAllTheCats.Data;  // Adjust this namespace if needed

namespace Cats.Tests.Helpers
{
    public static class InMemoryDbContextFactory
    {
        public static ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB per test
                .Options;
            return new ApplicationDbContext(options);
        }
    }
}
