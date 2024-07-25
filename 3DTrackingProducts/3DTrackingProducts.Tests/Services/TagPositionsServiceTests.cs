using _3DTrackingProducts.Api.Data;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DTrackingProductsTests.Services
{
    public class TagPositionsServiceTests
    {
        private async Task<ApplicationDbContext> GetApplicationDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();
            if (await _context.Tags.CountAsync() <= 0)
            {
                
                for (var i = 0; i < 10; i++)
                {
                    _context.Tags.Add(
                        new Tag
                        {
                            EPC = $"EPC000000{i}"
                        }
                    );

                    int numberPositionRecords = new Random().Next(1, 10);

                    for (var j = 0; j < numberPositionRecords; j++)
                    {
                        double xValue = new Random().NextDouble();
                        double yValue = new Random().NextDouble();
                        _context.TagPositions.Add(
                            new TagPosition
                            {
                                TagEPC = $"EPC000000{i}",
                                x = xValue,
                                y = yValue
                            }
                        );
                    }

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
        public async void GetAllTagPositions_ReturnsListTagPositions_WhenDataFound()
        {
            //Arrange
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).TagPositions;

            //Act
            var result = await service.GetAllTagPositions();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<TagPosition>>();
            result.As<List<TagPosition>>().Should().NotBeEmpty();
        }

        [Fact]
        public async void GetAllTagPositions_ReturnsEmptyListTags_WhenDataNotFound()
        {

            //Arrange
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).TagPositions;

            //Act
            var result = await service.GetAllTagPositions();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<TagPosition>>();
            result.As<List<TagPosition>>().Should().BeEmpty();
        }

        [Fact]
        public async void GetTagPositionsFromTagWithEPC_ReturnsListTagPosition_WhenTagWithEPCExists()
        {

            //Arrange
            var EPC = "EPC0000001";
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).TagPositions;

            //Act
            var result = await service.GetTagPositionsFromTagWithEPC(EPC);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<TagPosition>>();
            result.As<List<TagPosition>>().Should().NotBeEmpty();
        }

        [Fact]
        public async void GetTagPositionsFromTagWithEPC_ReturnsEmptyList_WhenTagWithEPCDoesntExists()
        {

            //Arrange
            var EPC = "EPC0000001";
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).TagPositions;

            //Act
            var result = await service.GetTagPositionsFromTagWithEPC(EPC);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<TagPosition>>();
            result.As<List<TagPosition>>().Should().NotBeNull();
        }

        [Fact]
        public async void GetLastTagPositionsFromTagWithEPC_ReturnNull_WhenDataNotFound()
        {
            var EPC = "EPC0000001";
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).TagPositions;

            var result = await service.GetLastTagPositionsFromTagWithEPC(EPC);
            
            result?.Should().BeNull();
        }

        [Fact]
        public async void GetLastTagPositionsFromTagWithEPC_ReturnTagPosition_WhenDataFound()
        {
            var EPC = "EPC0000001";
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).TagPositions;

            var result = await service.GetLastTagPositionsFromTagWithEPC(EPC);

            result?.Should().NotBeNull();
        }

        [Fact]
        public async void AddTagPosition_ReturnsTrue_WhenTagPositionInserted()
        {

            //Arrange
            TagPosition tagPosition = new TagPosition { TagEPC = "EPC0000001" , x = 1.0, y = 2.0 };
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).TagPositions;

            //Act
            var result = await service.AddTagPosition(tagPosition);

            //Assert
            result.As<bool>().Should().BeTrue();
        }

        [Fact]
        public async void DeleteTagPositionsFromTagWithEPC_ReturnsTrue_WhenTagWithEPCDeleted()
        {

            //Arrange
            string EPC = "EPC0000001";
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).TagPositions;

            //Act
            var result = await service.DeleteTagPositionsFromTagWithEPC(EPC);

            //Assert
            result.As<bool>().Should().BeTrue();
        }

        [Fact]
        public async void DeleteTagPositionsFromTagWithEPC_ReturnsTrue_WhenTagPositionWithEPCDoesntExist()
        {

            //Arrange
            string EPC = "EPC00000020";
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).TagPositions;

            //Act
            var result = await service.DeleteTagPositionsFromTagWithEPC(EPC);

            //Assert
            result.As<bool>().Should().BeTrue();
        }
    }
}
