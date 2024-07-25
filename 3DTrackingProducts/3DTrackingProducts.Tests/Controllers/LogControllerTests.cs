using _3DTrackingProducts.Api.Controllers;
using _3DTrackingProducts.Api.Core;
using _3DTrackingProducts.Api.Core.Repositories;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Persistence;
using _3DTrackingProducts.Api.Resources;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace _3DTrackingProductsTests.Controllers
{
    public class LogControllerTests
    {
        private ILogger<LogController> _logger;
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        private LogController _controller;

        public LogControllerTests()
        {
            _logger = A.Fake<ILogger<LogController>>();
            _unitOfWork = A.Fake<UnitOfWork>();
            _mapper = A.Fake<IMapper>();

            _unitOfWork.Logs = A.Fake<ILogRepository>();
            _unitOfWork.Tags = A.Fake<ITagRepository>();
            _unitOfWork.PairAntennas = A.Fake<IPairAntennaRepository>();
            _unitOfWork.Rooms = A.Fake<IRoomRepository>();

            _controller = new LogController(_logger, _unitOfWork, _mapper);
        }

        [Fact]
        public async Task GetAllLogs_returnsOkAsync()
        {
            List<Log> logs = A.Fake<List<Log>>();

            A.CallTo(() => _unitOfWork.Logs.GetAllLogs()).Returns(logs);

            var result = await _controller.GetAllLogs();

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetLogWithId_returnsOkAsync()
        {
            Guid logId = Guid.NewGuid();
            Log log = new Log
            {
                Id = logId,
            };

            A.CallTo(() => _unitOfWork.Logs.GetLogWithId(A<Guid>._)).Returns(log);

            var result = await _controller.GetLogWithId(logId);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetLogWithId_returnsNotFound_WhenLogDoesntExist()
        {
            Guid logId = Guid.NewGuid();
            Log? log = null;

            A.CallTo(() => _unitOfWork.Logs.GetLogWithId(A<Guid>._)).Returns(log);

            var result = await _controller.GetLogWithId(logId);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetLogsWithEPC_returnsOkAsync()
        {
            string epc = "EPC";
            List<Log> logs = A.Fake<List<Log>>();
            logs.Add(new Log());

            A.CallTo(() => _unitOfWork.Logs.GetLogsFromTagWithEPC(A<string>._)).Returns(logs);

            var result = await _controller.GetLogsWithEPC(epc);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetLogsWithEPC_returnsNotFound_WhenDataNotFound()
        {
            string epc = "EPC";
            List<Log> logs = A.Fake<List<Log>>();

            A.CallTo(() => _unitOfWork.Logs.GetLogsFromTagWithEPC(A<string>._)).Returns(logs);

            var result = await _controller.GetLogsWithEPC(epc);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetIPAddresses_returnsOkAsync()
        {
            string epc = "EPC";
            List<string> ipaddresses = A.Fake<List<string>>();

            A.CallTo(() => _unitOfWork.Logs.GetIPAddressThatRegisteredTagWithEPC(A<string>._)).Returns(ipaddresses);

            var result = await _controller.GetIPAddresses(epc);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetLogsWithEPCFromIPAdress_returnsOkAsync()
        {
            string epc = "EPC";
            string ipaddress = "127.0.0.1";
            List<Log> logsFromIPAddress = A.Fake<List<Log>>();
            logsFromIPAddress.Add(new Log
            {
                TagEPC = "EPC"
            });

            A.CallTo(() => _unitOfWork.Logs.GetLogsFromIPAddressWithTagEPC(A<string>._, A<string>._)).Returns(logsFromIPAddress);

            var result = await _controller.GetLogsWithEPCFromIPAdress(epc, ipaddress);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetLogsFromIPAddressWithTagEPC_returnsNotFound_WhenNoLogsWithIPAddressFound()
        {
            string epc = "EPC";
            string ipaddress = "127.0.0.1";
            List<Log> logsFromIPAddress = A.Fake<List<Log>>();

            A.CallTo(() => _unitOfWork.Logs.GetLogsFromIPAddressWithTagEPC(A<string>._,A<string>._)).Returns(logsFromIPAddress);

            var result = await _controller.GetLogsWithEPCFromIPAdress(epc, ipaddress);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetLogsWithEPCFromIPAdress_returnsNotFound_WhenNoLogsWithIPAddressAndEPCFound()
        {
            string epc = "EPC";
            string ipaddress = "127.0.0.1";
            List<Log> logsFromIPAddress = A.Fake<List<Log>>();

            A.CallTo(() => _unitOfWork.Logs.GetLogsFromIPAddressWithTagEPC(A<string>._,A<string>._)).Returns(logsFromIPAddress);

            var result = await _controller.GetLogsWithEPCFromIPAdress(epc, ipaddress);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task AddLog_returnsCreatedAtAction()
        {
            Log log = A.Fake<Log>();
            Tag tag = A.Fake<Tag>();
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            LogResource logResource = new LogResource();

            A.CallTo(() => _mapper.Map<LogResource, Log>(A<LogResource>._)).Returns(log);

            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Logs.AddLog(A<Log>._)).Returns(true);

            var result = await _controller.AddLog(logResource);

            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task AddLog_returnsNotFound_WhenTagWithEPCDoesntExist()
        {
            Log log = A.Fake<Log>();
            Tag? tag = null;
            LogResource logResource = new LogResource();

            A.CallTo(() => _mapper.Map<LogResource, Log>(A<LogResource>._)).Returns(log);

            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);

            var result = await _controller.AddLog(logResource);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task AddLog_returnsNotFound_WhenPairAntennaNotFound()
        {
            Log log = A.Fake<Log>();
            Tag tag = A.Fake<Tag>();
            PairAntenna? pairAntenna = null;
            LogResource logResource = new LogResource();

            A.CallTo(() => _mapper.Map<LogResource, Log>(A<LogResource>._)).Returns(log);

            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntenna);

            var result = await _controller.AddLog(logResource);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task AddLog_returnsStatusCode500_WhenAddLogFailed()
        {
            Log log = A.Fake<Log>();
            Tag tag = A.Fake<Tag>();
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            LogResource logResource = new LogResource();

            A.CallTo(() => _mapper.Map<LogResource, Log>(A<LogResource>._)).Returns(log);

            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Logs.AddLog(A<Log>._)).Returns(false);

            var result = await _controller.AddLog(logResource);
            var resultAction = result as ObjectResult;

            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task DeleteLogWithId_returnsNoContent()
        {
            Guid id = Guid.NewGuid();
            Log log = new Log
            {
                Id = id
            };

            A.CallTo(() => _unitOfWork.Logs.GetLogWithId(A<Guid>._)).Returns(log);

            A.CallTo(() => _unitOfWork.Logs.DeleteLog(A<Guid>._)).Returns(true);

            var result = await _controller.DeleteLogWithId(id);

            result.Should().BeAssignableTo<NoContentResult>();
        }

        [Fact]
        public async Task DeleteLogWithId_returnsNotFound_WhenLogNotFound()
        {
            Guid id = Guid.NewGuid();
            Log? log = null;

            A.CallTo(() => _unitOfWork.Logs.GetLogWithId(A<Guid>._)).Returns(log);

            var result = await _controller.DeleteLogWithId(id);

            result.Should().BeAssignableTo<NotFoundObjectResult>();
        }

        [Fact]
        public async Task DeleteLogWithId_returnsStatusCode500_WhenLogFailedToBeDeleted()
        {
            Guid id = Guid.NewGuid();
            Log log = new Log
            {
                Id = id
            };

            A.CallTo(() => _unitOfWork.Logs.GetLogWithId(A<Guid>._)).Returns(log);

            A.CallTo(() => _unitOfWork.Logs.DeleteLog(A<Guid>._)).Returns(false);

            var result = await _controller.DeleteLogWithId(id);
            var resultAction = result as ObjectResult;

            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task DeleteLogsWithEPC_returnsNoContent()
        {
            List<Log> logs = A.Fake<List<Log>>();
            logs.Add(new Log());

            string epc = "EPC";

            A.CallTo(() => _unitOfWork.Logs.GetLogsFromTagWithEPC(A<string>._)).Returns(logs);

            A.CallTo(() => _unitOfWork.Logs.DeleteLogsFromTagWithEPC(A<string>._)).Returns(true);

            var result = await _controller.DeleteLogsWithEPC(epc);

            result.Should().BeAssignableTo<NoContentResult>();
        }

        [Fact]
        public async Task DeleteLogsWithEPC_returnsNotFound_WhenNoLogsForTagEPCFound()
        {
            List<Log> logs = A.Fake<List<Log>>();

            string epc = "EPC";

            A.CallTo(() => _unitOfWork.Logs.GetLogsFromTagWithEPC(A<string>._)).Returns(logs);


            var result = await _controller.DeleteLogsWithEPC(epc);

            result.Should().BeAssignableTo<NotFoundObjectResult>();
        }

        [Fact]
        public async Task DeleteLogsWithEPC_returnsNotFound_WhenDeleteFailed()
        {
            List<Log> logs = A.Fake<List<Log>>();
            logs.Add(new Log());

            string epc = "EPC";

            A.CallTo(() => _unitOfWork.Logs.GetLogsFromTagWithEPC(A<string>._)).Returns(logs);

            A.CallTo(() => _unitOfWork.Logs.DeleteLogsFromTagWithEPC(A<string>._)).Returns(false);

            var result = await _controller.DeleteLogsWithEPC(epc);
            var resultAction = result as ObjectResult;

            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);
        }
    }
}
