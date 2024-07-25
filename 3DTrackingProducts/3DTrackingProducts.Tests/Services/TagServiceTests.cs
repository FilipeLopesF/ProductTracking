using _3DTrackingProducts.Api.Data;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Data.Entity.Infrastructure;

namespace _3DTrackingProductsTests.Services
{
    public class TagServiceTests
    {

        private async Task<ApplicationDbContext> GetApplicationDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();
            if(await _context.Tags.CountAsync() <= 0)
            {
                for(var i = 0; i < 10; i++)
                {
                    _context.Tags.Add(
                        new Tag{
                            EPC = $"EPC000000{i}"
                        }
                    );
                    await _context.SaveChangesAsync();
                }
            }

            return _context;
        }

        private ApplicationDbContext GetEmptyDatabaseApplicationDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();
            return _context;
        }

        [Fact]
        public async void GetAllTags_ReturnsListTags_WhenDataFound()
        { 
            //Arrange
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Tags;

            //Act
            var result = await service.GetAllTags();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<Tag>>();
            result.As<List<Tag>>().Should().NotBeEmpty();
        }

        [Fact]
        public async void GetAllTags_ReturnsEmptyListTags_WhenDataNotFound()
        {

            //Arrange
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Tags;

            //Act
            var result = await service.GetAllTags();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<Tag>>();
            result.As<List<Tag>>().Should().BeEmpty();
        }

        [Fact]
        public async void GetTagWithEPC_ReturnsTag_WhenTagWithEPCExists()
        {

            //Arrange
            var EPC = "EPC0000001";
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Tags;

            //Act
            var result = await service.GetTagWithEPC(EPC);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Tag>();
        }

        [Fact]
        public async void GetTagWithEPC_ReturnsNull_WhenTagWithEPCDoesntExists()
        {

            //Arrange
            var EPC = "EPC0000001";
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Tags;

            //Act
            var result = await service.GetTagWithEPC(EPC);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void AddTag_ReturnsTrue_WhenTagEPCInserted()
        {

            //Arrange
            Tag tag = new Tag { EPC = "EPC00000020" };
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Tags;

            //Act
            var result = await service.AddTag(tag);

            //Assert
            result.As<bool>().Should().BeTrue();
        }

        [Fact]
        public async void DeleteTag_ReturnsTrue_WhenTagWithEPCDeleted()
        {

            //Arrange
            string EPC = "EPC0000001";
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Tags;

            //Act
            var result = await service.DeleteTag(EPC);

            //Assert
            result.As<bool>().Should().BeTrue();
        }

        [Fact]
        public async void DeleteTag_ReturnsFalse_WhenTagWithEPCDoesntExist()
        {

            //Arrange
            string EPC = "EPC00000020";
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Tags;

            //Act
            var result = await service.DeleteTag(EPC);

            //Assert
            result.As<bool>().Should().BeFalse();
        }
    }
}
