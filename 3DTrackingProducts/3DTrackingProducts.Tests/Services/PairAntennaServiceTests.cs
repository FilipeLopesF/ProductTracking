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
    public class PairAntennaServiceTests
    {
        private async Task<ApplicationDbContext> GetApplicationDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();
            if (await _context.PairAntennas.CountAsync() <= 0)
            {

                for (var i = 0; i < 10; i++)
                {
                    Room room = new Room
                    {
                        Name = $"Room0{i}",
                        Width = i + 10,
                        Length = i + 15,
                    };

                    _context.Rooms.Add(room);

                    string antenna01IP = $"0.0.0.{i}";
                    string antenna02IP = $"0.0.1.{i}";
                    int antenna01X = i + 2;
                    int antenna01Y = i + 1;
                    int antenna02X = i + 3;
                    int antenna02Y = i + 4;

                    _context.PairAntennas.Add(
                        new PairAntenna
                        {
                            antenna01IP = antenna01IP,
                            antenna02IP = antenna02IP,
                            antenna01X = antenna01X,
                            antenna01Y = antenna01Y,
                            antenna02X = antenna02X,
                            antenna02Y = antenna02Y,
                            idRoom = room.id
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
        public async Task AddPairAntenna_returnsTrue_WhenPairAntennaAddedAsync()
        {
            string antenna01IP = "1.0.0.0";
            PairAntenna pairAntenna = new PairAntenna
            {
                antenna01IP = antenna01IP,
                antenna02IP = "2.0.0.0",
                idRoom = Guid.NewGuid()
            };
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).PairAntennas;

            var result = await service.AddPairAntenna(pairAntenna);

            result.Should().BeTrue();

            var addVerificationResult = await service.getPairAntennaByIPAddress(antenna01IP);

            addVerificationResult.Should().NotBeNull();
            addVerificationResult.Should().BeAssignableTo<PairAntenna>();
        }

        [Fact]
        public async void DeletePairAntenna_returnsTrue_WhenPairAntennaDeleted()
        {  
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).PairAntennas;

            PairAntenna pairAntenna = dbContext.PairAntennas.First();

            var result = await service.DeletePairAntenna(pairAntenna.Id);

            result.Should().BeTrue();

            var addVerificationResult = await service.getPairAntennaById(pairAntenna.Id);

            addVerificationResult.Should().BeNull();
        }

        [Fact]
        public async void DeletePairAntenna_returnsFalse_WhenPairAntennaNotFound()
        {
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).PairAntennas;

            var result = await service.DeletePairAntenna(Guid.NewGuid());

            result.Should().BeFalse();
        }

        [Fact]
        public async void getAllPairAntennas_returnsListPairAntenna_WhenDataFound()
        {
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).PairAntennas;

            var result = await service.getAllPairAntennas();

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<PairAntenna>>();
            result.As<List<PairAntenna>>().Should().NotBeEmpty();
        }

        [Fact]
        public async void getAllPairAntennas_returnsEmptyListPairAntenna_WhenDataFound()
        {
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).PairAntennas;

            var result = await service.getAllPairAntennas();

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<PairAntenna>>();
            result.As<List<PairAntenna>>().Should().BeEmpty();
        }

        [Fact]
        public async void getPairAntennaById_returnsPairAntenna_WhenPairAntennaFound()
        {
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).PairAntennas;

            PairAntenna pairAntenna = dbContext.PairAntennas.First();

            var result = await service.getPairAntennaById(pairAntenna.Id);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<PairAntenna>();
        }

        [Fact]
        public async Task getPairAntennaById_returnsNull_WhenPairAntennaNotFoundAsync()
        {
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).PairAntennas;

            var result = await service.getPairAntennaById(Guid.NewGuid());

            result.Should().BeNull();
        }

        [Fact]
        public async void getPairAntennaByIPAddress_returnsPairAntenna_WhenPairAntennaFound()
        {
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).PairAntennas;

            PairAntenna pairAntenna = dbContext.PairAntennas.First();

            var result = await service.getPairAntennaByIPAddress(pairAntenna.antenna01IP);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<PairAntenna>(); ;
        }

        [Fact]
        public async void getPairAntennaByIPAddress_returnsNull_WhenPairAntennaNotFound()
        {
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).PairAntennas;

            var result = await service.getPairAntennaById(Guid.NewGuid());

            result.Should().BeNull();
        }

        [Fact]
        public async void getPairAntennasByRoomId_returnsList_WhenDataNotFound()
        {
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).PairAntennas;

            Room room = dbContext.Rooms.First();

            var result = await service.getPairAntennasByRoomId(room.id);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<PairAntenna>>();
            result.As<List<PairAntenna>>().Should().NotBeEmpty();
        }

        [Fact]
        public async void getPairAntennasByRoomId_returnsEmptyList_WhenDataNotFound()
        {
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).PairAntennas;

            var result = await service.getPairAntennasByRoomId(Guid.NewGuid());

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<PairAntenna>>();
            result.As<List<PairAntenna>>().Should().BeEmpty();
        }

        [Fact]
        public async void UpdatePairAntenna_returnsTrue_WhenPairAntennaUpdated()
        {
            string antenna01IP = "1.0.0.1";
            PairAntenna pairAntenna = new PairAntenna
            {
                antenna01IP = antenna01IP
            };
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).PairAntennas;

            PairAntenna pairAntennaToUpdated = dbContext.PairAntennas.First();

            pairAntennaToUpdated.antenna01IP.Should().NotBe(antenna01IP);

            var result = await service.UpdatePairAntenna(pairAntennaToUpdated.Id,pairAntenna);

            result.Should().BeTrue();

            var updateVerificationResult = await service.getPairAntennaById(pairAntennaToUpdated.Id);

            updateVerificationResult.Should().NotBeNull();
            updateVerificationResult.Should().BeAssignableTo<PairAntenna>();
            updateVerificationResult.As<PairAntenna>().antenna01IP.Should().Be(antenna01IP);

        }


    }
}
