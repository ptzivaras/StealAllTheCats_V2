using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using StealAllTheCats.Data;    // Contains ApplicationDbContext
using StealAllTheCats.Models;  // Contains CatEntity, etc.
using StealAllTheCats.Services; // Contains CatService
using Cats.Tests.Helpers;      // Our helper class

namespace Cats.Tests.Services
{
    public class CatServiceTests
    {
        [Fact]
        public async Task SaveCatsToDatabaseAsync_ReturnsZero_WhenNoCatsProvided()
        {
            // Arrange
            using var context = InMemoryDbContextFactory.CreateContext();
            var service = new CatService(context);
            var cats = new List<CatEntity>(); // Empty list

            // Act
            var result = await service.SaveCatsToDatabaseAsync(cats);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task SaveCatsToDatabaseAsync_AddsUniqueCat()
        {
            // Arrange
            using var context = InMemoryDbContextFactory.CreateContext();
            var service = new CatService(context);
            var cat = new CatEntity
            {
                CatId = "unique1",
                Width = 300,
                Height = 400,
                ImageUrl = "http://test.com/cat1.jpg",
                Created = DateTime.UtcNow
            };
            var cats = new List<CatEntity> { cat };

            // Act
            var result = await service.SaveCatsToDatabaseAsync(cats);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task SaveCatsToDatabaseAsync_DoesNotAddDuplicateCats()
        {
            // Arrange
            using var context = InMemoryDbContextFactory.CreateContext();
            var service = new CatService(context);
            var cat1 = new CatEntity
            {
                CatId = "duplicate",
                Width = 300,
                Height = 400,
                ImageUrl = "http://test.com/cat1.jpg",
                Created = DateTime.UtcNow
            };
            var cat2 = new CatEntity
            {
                CatId = "duplicate",
                Width = 350,
                Height = 450,
                ImageUrl = "http://test.com/cat2.jpg",
                Created = DateTime.UtcNow
            };
            var cats = new List<CatEntity> { cat1, cat2 };

            // Act
            var result = await service.SaveCatsToDatabaseAsync(cats);

            // Assert
            // Only one unique cat should be added despite duplicates
            Assert.Equal(1, result);
        }
    }
}
