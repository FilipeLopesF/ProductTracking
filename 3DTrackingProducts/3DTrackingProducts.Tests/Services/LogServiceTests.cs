using _3DTrackingProducts.Api.Data;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace _3DTrackingProductsTests.Services
{
    public class LogServiceTests
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

                    int numberPositionRecords = 10;
                    string[] ipaddresses = { "127.0.0.1", "192.168.0.1", "8.8.8.8" };
                    for (var j = 0; j < numberPositionRecords; j++)
                    {
                        int rssi = new Random().Next(0,300);
                        double angle = new Random().Next(1,180);
                        for(var z = 0; z < ipaddresses.Length; z++)
                        {
                            _context.Logs.Add(
                                new Log
                                {
                                    TagEPC = $"EPC000000{i}",
                                    RSSI = rssi,
                                    Angle = angle,
                                    IPAddress = ipaddresses[z]
                                }
                            ); ;
                        }
                       
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
        public async void GetAllLogs_returnsList_WhenDataFoundAsync()
        {
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Logs;

            var records = await service.GetAllLogs();

            records.Should().NotBeNull();
            records.Should().BeAssignableTo<List<Log>>();
            records.As<List<Log>>().Should().NotBeEmpty();

        }

        [Fact]
        public async void GetAllLogs_returnsEmptyList_WhenDataNotFoundAsync()
        {
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Logs;

            var records = await service.GetAllLogs();

            records.Should().NotBeNull();
            records.Should().BeAssignableTo<List<Log>>();
            records.As<List<Log>>().Should().BeEmpty();
        }

        [Fact]
        public async void GetLogWithId_returnsLog_WhenDataFound()
        {
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Logs;
            List<Log> logs = await service.GetAllLogs();

            var records = await service.GetLogWithId(logs.First().Id);

            records.Should().NotBeNull();
            records.Should().BeAssignableTo<Log>();
        }

        [Fact]
        public async void GetLogWithId_returnsNull_WhenDataNotFound()
        {
            Guid id = Guid.NewGuid();
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Logs;

            var records = await service.GetLogWithId(id);

            records.Should().BeNull();
        }

        [Fact]
        public async void GetLogsFromTagWithEPC_returnsList_WhenDataNotFound()
        {
            string epc = "EPC0000001";
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Logs;

            var records = await service.GetLogsFromTagWithEPC(epc);

            records.Should().NotBeNull();
            records.Should().BeAssignableTo<List<Log>>();
            records.As<List<Log>>().Should().NotBeEmpty();
        }

        [Fact]
        public async void GetLogsFromTagWithEPC_returnsEmptyList_WhenDataNotFound()
        {
            string epc = "EPC0000050";
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Logs;

            var records = await service.GetLogsFromTagWithEPC(epc);

            records.Should().NotBeNull();
            records.Should().BeAssignableTo<List<Log>>();
            records.As<List<Log>>().Should().BeEmpty();
        }

        [Fact]
        public async void GetLogsFromIPAddressWithTagEPC_returnsList_WhenDataFound()
        {
            string ipaddress = "127.0.0.1";
            string epc = "EPC0000001";
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Logs;

            var records = await service.GetLogsFromIPAddressWithTagEPC(ipaddress,epc);

            records.Should().NotBeNull();
            records.Should().BeAssignableTo<List<Log>>();
            records.As<List<Log>>().Should().NotBeEmpty();
        }

        [Fact]
        public async void GetLogsFromIPAddress_returnsEmptyList_WhenDataNotFound()
        {
            string ipaddress = "127.0.0.1";
            string epc = "EPC0000001";
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Logs;

            var records = await service.GetLogsFromIPAddressWithTagEPC(ipaddress,epc);

            records.Should().NotBeNull();
            records.Should().BeAssignableTo<List<Log>>();
            records.As<List<Log>>().Should().BeEmpty();
        }

        [Fact]
        public async void AddLog_ReturnsTrue_WhenLogInserted()
        {
            Log log = new Log { TagEPC = "EPC0000001", RSSI = 100, Angle = 2.0, IPAddress = "127.0.0.1" };
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Logs;

            //Act
            var result = await service.AddLog(log);

            //Assert
            result.As<bool>().Should().BeTrue();
        }

        [Fact]
        public async void DeleteLog_ReturnsTrue_WhenLogFoundDeleted()
        {
                                  
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Logs;

            List<Log> logs = await service.GetAllLogs();
            
            //Act
            var result = await service.DeleteLog(logs.First().Id);

            //Assert
            result.As<bool>().Should().BeTrue();
        }

        [Fact]
        public async void DeleteLog_ReturnsFalse_WhenLogNotFound()
        {
            Guid id = Guid.NewGuid();
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Logs;

            //Act
            var result = await service.DeleteLog(id);

            //Assert
            result.As<bool>().Should().BeFalse();
        }

        [Fact]
        public async void DeleteLogsFromTagWithEPC_ReturnsTrue_WhenLogFoundDeleted()
        {
            string epc = "EPC0000001";
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Logs;

            //Act
            var result = await service.DeleteLogsFromTagWithEPC(epc);

            //Assert
            result.As<bool>().Should().BeTrue();
        }

        [Fact]
        public async void DeleteLogsFromTagWithEPC_ReturnsFalse_WhenLogNotFoundInserted()
        {
            string epc = "EPC0000001";
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Logs;

            //Act
            var result = await service.DeleteLogsFromTagWithEPC(epc);

            //Assert
            result.As<bool>().Should().BeTrue();
        }

        [Fact]
        public async void GetIPAddressThatRegisteredTagWithEPC_returnsList_WhenTagFound()
        {
            string epc = "EPC0000001";
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Logs;

            //Act
            var result = await service.GetIPAddressThatRegisteredTagWithEPC(epc);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<string>>();
            result.As<List<string>>().Should().NotBeEmpty();
        }

        [Fact]
        public async void GetIPAddressThatRegisteredTagWithEPC_returnsEmptyList_WhenTagNotFound()
        {
            string epc = "EPC0000001";
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Logs;

            //Act
            var result = await service.GetIPAddressThatRegisteredTagWithEPC(epc);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<string>>();
            result.As<List<string>>().Should().BeEmpty();
        }
    }
}
