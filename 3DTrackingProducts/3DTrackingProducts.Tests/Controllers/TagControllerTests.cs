using _3DTrackingProducts.Api.Controllers;
using _3DTrackingProducts.Api.Core;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using _3DTrackingProducts.Api.Persistence.Repositories;
using _3DTrackingProducts.Api.Core.Repositories;
using _3DTrackingProducts.Api.Persistence;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Resources;

namespace _3DTrackingProductsTests.Controllers
{
    public class TagControllerTests
    {
        private ILogger<TagController> _logger;
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        private TagController _tagController;

        public TagControllerTests()
        {
            _logger = A.Fake<ILogger<TagController>>();
            _mapper = A.Fake<IMapper>();
            _unitOfWork = A.Fake<UnitOfWork>();

            _unitOfWork.Tags = A.Fake<ITagRepository>();
            _unitOfWork.Category = A.Fake<ICategoryRepository>();
            _unitOfWork.Logs = A.Fake<ILogRepository>();
            _unitOfWork.TagPositions = A.Fake<ITagPositionRepository>();
            _unitOfWork.Rooms = A.Fake<IRoomRepository>();
            _unitOfWork.PairAntennas = A.Fake<IPairAntennaRepository>();

            _tagController = new TagController(_logger,_unitOfWork, _mapper);
        }

        [Fact]
        public async Task GetAllTags_returnsOkAsync()
        {
            List<Tag> tags = A.Fake<List<Tag>>();

            A.CallTo(() => _unitOfWork.Tags.GetAllTags()).Returns(tags);

            var result = await _tagController.GetAllTags();

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetTag_returnsOkAsync()
        {
            string epc = "EPC";
            Tag tag = A.Fake<Tag>();

            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);

            var result = await _tagController.GetTag(epc);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetTag_returnsBadRequest_WhenEPCEmpty()
        {
            string epc = "";        

            var result = await _tagController.GetTag(epc);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetTag_returnsBadRequest_WhenEPCNull()
        {
            string? epc = null;

            var result = await _tagController.GetTag(epc);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetTag_returnsNotFound_WhenTagWithEPCDoesntExist()
        {
            string epc = "EPC";
            Tag? tag = null;

            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);

            var result = await _tagController.GetTag(epc);

            result.Should().BeOfType<NotFoundObjectResult>();
        }


        [Fact]
        public async Task GetPositionFromTagWithEPC_returnOk()
        {
            string epc = "EPC0001";
            Log log = A.Fake<Log>();
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01IP = "127.0.0.1";
            pairAntenna.antenna02IP = "8.8.8.8";
            Room? room = A.Fake<Room>();
            List<Log> logsFromIPAddress1 = new List<Log>();
            logsFromIPAddress1.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = pairAntenna.antenna01IP,
                Angle = 145
            });
            List<Log> logsFromIPAddress2 = new List<Log>();
            logsFromIPAddress2.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = pairAntenna.antenna02IP,
                Angle = 90
            });

            A.CallTo(() => _unitOfWork.Logs.GetLastLogFromTagWithEPC(A<string>._)).Returns(log);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);
            A.CallTo(() => _unitOfWork.Logs.GetLogsFromIPAddressWithTagEPCInLastXMinutes(pairAntenna.antenna01IP, epc, 10)).Returns(logsFromIPAddress1);
            A.CallTo(() => _unitOfWork.Logs.GetLogsFromIPAddressWithTagEPCInLastXMinutes(pairAntenna.antenna02IP, epc, 10)).Returns(logsFromIPAddress2);

            var result = await _tagController.GetPositionFromTagWithEPC(epc);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetPositionFromTagWithEPC_returnNotFound_WhenPairAntennaDoesntExist()
        {
            string epc = "EPC0001";

            PairAntenna? pairAntenna = null;

            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntenna);

            var result = await _tagController.GetPositionFromTagWithEPC(epc);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetPositionFromTagWithEPC_returnNotFound_WhenRoomDoesntExist()
        {
            string epc = "EPC0001";
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            Room? room = null;

            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);


            var result = await _tagController.GetPositionFromTagWithEPC(epc);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetPositionFromTagWithEPC_returnNotFound_WhenLogsAntenna01DoesntExist()
        {
            string epc = "EPC0001";

            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            Room? room = null;
            List<Log> logsFromIPAddress1 = new List<Log>();


            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Logs.GetLogsFromIPAddressWithTagEPCInLastXMinutes(A<string>._, epc, 10)).Returns(logsFromIPAddress1);

            var result = await _tagController.GetPositionFromTagWithEPC(epc);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetPositionFromTagWithEPC_returnNotFound_WhenLogsAntenna02DoesntExist()
        {
            string epc = "EPC0001";

            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            Room? room = null;
            List<Log> logsFromIPAddress1 = new List<Log>();
            logsFromIPAddress1.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 145
            });
            List<Log> logsFromIPAddress2 = new List<Log>();

            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Logs.GetLogsFromIPAddressWithTagEPCInLastXMinutes(logsFromIPAddress1[0].IPAddress, epc, 10)).Returns(logsFromIPAddress1);
            A.CallTo(() => _unitOfWork.Logs.GetLogsFromIPAddressWithTagEPCInLastXMinutes(A<string>._, epc, 10)).Returns(logsFromIPAddress2);

            var result = await _tagController.GetPositionFromTagWithEPC(epc);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetPositionFromTagWithEPC_returnBadRequest_WhenPointDontIntersec()
        {
            string epc = "EPC0001";
            Log log = A.Fake<Log>();
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01IP = "127.0.0.1";
            pairAntenna.antenna02IP = "8.8.8.8";
            Room? room = A.Fake<Room>();
            List<Log> logsFromIPAddress1 = new List<Log>();
            logsFromIPAddress1.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = pairAntenna.antenna01IP,
                Angle = 145
            });
            List<Log> logsFromIPAddress2 = new List<Log>();
            logsFromIPAddress2.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = pairAntenna.antenna02IP,
                Angle = 145
            });

            A.CallTo(() => _unitOfWork.Logs.GetLastLogFromTagWithEPC(A<string>._)).Returns(log);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);
            A.CallTo(() => _unitOfWork.Logs.GetLogsFromIPAddressWithTagEPCInLastXMinutes(pairAntenna.antenna01IP, epc, 10)).Returns(logsFromIPAddress1);
            A.CallTo(() => _unitOfWork.Logs.GetLogsFromIPAddressWithTagEPCInLastXMinutes(pairAntenna.antenna02IP, epc, 10)).Returns(logsFromIPAddress2);

            var result = await _tagController.GetPositionFromTagWithEPC(epc);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task AddTag_returnsCreatedAtAction()
        {
            TagResource tagResource = new TagResource();
            Tag tagToAdd = A.Fake<Tag>();
            Tag? tag = null;
            Category category = A.Fake<Category>();

            A.CallTo(() => _mapper.Map<TagResource, Tag>(A<TagResource>._)).Returns(tagToAdd);
            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);
            A.CallTo(() => _unitOfWork.Category.GetByIdAsync(A<int>._)).Returns(category);
            A.CallTo(() => _unitOfWork.Tags.AddTag(A<Tag>._)).Returns(true);

            var result = await _tagController.AddTag(tagResource);

            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task AddTag_returnsConflict_WhenTagWithEPCFound()
        {
            TagResource tagResource = new TagResource();
            Tag tagToAdd = A.Fake<Tag>();
            Tag? tag = A.Fake<Tag>();
            Category category = A.Fake<Category>();

            A.CallTo(() => _mapper.Map<TagResource, Tag>(A<TagResource>._)).Returns(tagToAdd);
            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);

            var result = await _tagController.AddTag(tagResource);

            result.Should().BeOfType<ConflictObjectResult>();
        }

        [Fact]
        public async Task AddTag_returnsNotFound_WhenCategoryDoesntExist()
        {
            TagResource tagResource = new TagResource();
            Tag tagToAdd = A.Fake<Tag>();
            Tag? tag = null;
            Category? category = null;

            A.CallTo(() => _mapper.Map<TagResource, Tag>(A<TagResource>._)).Returns(tagToAdd);
            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);
            A.CallTo(() => _unitOfWork.Category.GetByIdAsync(A<int>._)).Returns(category);

            var result = await _tagController.AddTag(tagResource);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task AddTag_returnsStatusCode500_WhenCategoryWasntAdded()
        {
            TagResource tagResource = new TagResource();
            Tag tagToAdd = A.Fake<Tag>();
            Tag? tag = null;
            Category category = A.Fake<Category>();

            A.CallTo(() => _mapper.Map<TagResource, Tag>(A<TagResource>._)).Returns(tagToAdd);
            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);
            A.CallTo(() => _unitOfWork.Category.GetByIdAsync(A<int>._)).Returns(category);
            A.CallTo(() => _unitOfWork.Tags.AddTag(A<Tag>._)).Returns(false);

            var result = await _tagController.AddTag(tagResource);
            var resultAction = result as ObjectResult;

            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task DeleteTag_returnsNoContent()
        {
            string epc = "EPC";
            Tag? tag = A.Fake<Tag>();
            Category category = A.Fake<Category>();

            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);
            A.CallTo(() => _unitOfWork.Logs.DeleteLogsFromTagWithEPC(epc)).Returns(true);
            A.CallTo(() => _unitOfWork.TagPositions.DeleteTagPositionsFromTagWithEPC(epc)).Returns(true);
            A.CallTo(() => _unitOfWork.Tags.DeleteTag(A<string>._)).Returns(true);

            var result = await _tagController.DeleteTag(epc);
            
            result.Should().BeAssignableTo<NoContentResult>();
        }

        [Fact]
        public async Task DeleteTag_returnsBadRequest_WhenEPCEmpty()
        {
            string epc = "";
            
            var result = await _tagController.DeleteTag(epc);

            result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async Task DeleteTag_returnsBadRequest_WhenEPCNull()
        {
            string? epc = null;

            var result = await _tagController.DeleteTag(epc);

            result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async Task DeleteTag_returnsNotFound_WhenTagWithEPCDooesntExist()
        {
            string epc = "EPC";
            Tag? tag = null;
            Category category = A.Fake<Category>();

            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);

            var result = await _tagController.DeleteTag(epc);

            result.Should().BeAssignableTo<NotFoundObjectResult>();
        }

        [Fact]
        public async Task DeleteTag_returnsStatus500_WhenDeleteLogsWasNotDone()
        {
            string epc = "EPC";
            Tag? tag = A.Fake<Tag>();
            Category category = A.Fake<Category>();

            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);
            A.CallTo(() => _unitOfWork.Logs.DeleteLogsFromTagWithEPC(epc)).Returns(false);

            var result = await _tagController.DeleteTag(epc);
            var resultAction = result as ObjectResult;

            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task DeleteTag_returnsStatus500_WhenDeleteTagPositionsWasNotDone()
        {
            string epc = "EPC";
            Tag? tag = A.Fake<Tag>();
            Category category = A.Fake<Category>();

            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);
            A.CallTo(() => _unitOfWork.Logs.DeleteLogsFromTagWithEPC(epc)).Returns(true);
            A.CallTo(() => _unitOfWork.TagPositions.DeleteTagPositionsFromTagWithEPC(epc)).Returns(false);

            var result = await _tagController.DeleteTag(epc);
            var resultAction = result as ObjectResult;

            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task DeleteTag_returnsStatus500_WhenDeleteTagWasNotDone()
        {
            string epc = "EPC";
            Tag? tag = A.Fake<Tag>();
            Category category = A.Fake<Category>();

            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);
            A.CallTo(() => _unitOfWork.Logs.DeleteLogsFromTagWithEPC(epc)).Returns(true);
            A.CallTo(() => _unitOfWork.TagPositions.DeleteTagPositionsFromTagWithEPC(epc)).Returns(true);
            A.CallTo(() => _unitOfWork.Tags.DeleteTag(A<string>._)).Returns(false);

            var result = await _tagController.DeleteTag(epc);
            var resultAction = result as ObjectResult;

            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);
        }

    }
}
