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
    public class RoomServiceTests
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
                    string name = $"{i}";
                    int width = new Random().Next(0, 10);
                    int length = new Random().Next(0, 10);
                    
                    _context.Rooms.Add(
                        new Room
                        {
                            Name = name,
                            Width = width,
                            Length = length
                        }
                    );
                }

                await _context.SaveChangesAsync();
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
        public async void GetAllRooms_returnsList_WhenDataFound()
        {
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Rooms;

            var result = await service.getAllRooms();

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<Room>>();
            result.As<List<Room>>().Should().NotBeEmpty();
        }

        [Fact]
        public async void GetAllRooms_returnsEmptyList_WhenDataNotFound()
        {
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Rooms;

            var result = await service.getAllRooms();

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<Room>>();
            result.As<List<Room>>().Should().BeEmpty();
        }

        [Fact]
        public async void GetRoomById_returnsRoom_WhenDataFound()
        {
    
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Rooms;
            List<Room> rooms = await service.getAllRooms();

            var result = await service.getRoomById(rooms.First().id);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Room>();
          
        }

        [Fact]
        public async void GetRoomById_returnsNull_WhenDataNotFound()
        {

            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Rooms;

            var result = await service.getRoomById(Guid.Empty);

            result.Should().BeNull();

        }

        [Fact]
        public async void GetRoomByName_returnsRoom_WhenDataFound()
        {
            string name = "1";
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Rooms;
            

            var result = await service.getRoomByName(name);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Room>();

        }

        [Fact]
        public async void GetRoomByName_returnsNull_WhenDataNotFound()
        {
            string ipAddress = "1";
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Rooms;


            var result = await service.getRoomByName(ipAddress);

            result.Should().BeNull();

        }

        [Fact]
        public async void AddRoom_returnsTrue_WhenRoomAdded()
        {
            string name = "1";
            Room room = new Room { 
                Name = name
            };
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Rooms;

            var result = await service.AddRoom(room);

            result.Should().BeTrue();

            var resultSearch = await service.getRoomByName(name);

            resultSearch.Should().NotBeNull();
            resultSearch.Should().BeAssignableTo<Room>();

        }

        [Fact]
        public async void UpdateRoom_returnsTrue_WhenRoomUpdated()
        {
            string name = "200";
            Room roomUpdate = new Room {
                Name = name,
            };
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Rooms;
            List<Room> rooms = await service.getAllRooms();
            Guid id = rooms.First().id;

            rooms.First().Name.Should().NotBe(name);

            var result = await service.UpdateRoom(id, roomUpdate);

            result.Should().BeTrue();

            var resultSearch = await service.getRoomById(id);

            resultSearch.Should().NotBeNull();
            resultSearch.Should().BeAssignableTo<Room>();
            resultSearch.As<Room>().Name.Should().Be(name);

        }

        [Fact]
        public async void UpdateRoom_returnsFalse_WhenRoomDoesntExist()
        {
            string name = "1";
            Room roomUpdate = new Room
            {
                Name = name
            };
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Rooms;

            var result = await service.UpdateRoom(Guid.NewGuid(), roomUpdate);

            result.Should().BeFalse();

        }

        [Fact]
        public async void DeleteRoom_returnsTrue_WhenRoomDeleted()
        {
           
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Rooms;
            List<Room> rooms = await service.getAllRooms();
            Guid id = rooms.First().id;

            var result = await service.DeleteRoom(id);

            result.Should().BeTrue();

            var resultSearch = await service.getRoomById(id);

            resultSearch.Should().BeNull();

        }

        [Fact]
        public async void DeleteRoom_returnsFalse_WhenRoomDoesntExist()
        {
           
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Rooms;

            var result = await service.DeleteRoom(Guid.NewGuid());

            result.Should().BeFalse();

        }
    }
}
