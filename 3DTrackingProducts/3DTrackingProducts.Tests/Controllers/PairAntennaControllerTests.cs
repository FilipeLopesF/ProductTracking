using _3DTrackingProducts.Api.Controllers;
using _3DTrackingProducts.Api.Persistence;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using _3DTrackingProducts.Api.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using _3DTrackingProducts.Api.Models;
using FluentAssertions;
using _3DTrackingProducts.Api.Resources;

namespace _3DTrackingProductsTests.Controllers
{
    public class PairAntennaControllerTests
    {
        private ILogger<PairAntennaController> _logger;
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        private PairAntennaController _controller;

        public PairAntennaControllerTests()
        {
            _logger = A.Fake<ILogger<PairAntennaController>>();
            _unitOfWork = A.Fake<UnitOfWork>();
            _mapper = A.Fake<IMapper>();

            _unitOfWork.Rooms = A.Fake<IRoomRepository>();
            _unitOfWork.PairAntennas = A.Fake<IPairAntennaRepository>();

            _controller = new PairAntennaController(_logger,_unitOfWork,_mapper);
        }

        [Fact]
        public async void getAllPairAntennas_returnsOk_WhenDataFound()
        {
            List<PairAntenna> pairAntennas = new List<PairAntenna>();
            pairAntennas.Add(new PairAntenna());

            A.CallTo(() => _unitOfWork.PairAntennas.getAllPairAntennas()).Returns(pairAntennas);

            var result = await _controller.getAllPairAntennas();

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void getAllPairAntennas_returnsOk_WhenDataNotFound()
        {
            List<PairAntenna> pairAntennas = new List<PairAntenna>();

            A.CallTo(() => _unitOfWork.PairAntennas.getAllPairAntennas()).Returns(pairAntennas);

            var result = await _controller.getAllPairAntennas();

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void getPairAntennaById_returnsOk_WhenDataFound()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();

            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);

            var result = await _controller.getPairAntennaById(Guid.NewGuid());

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void getPairAntennaById_returnsNotFound_WhenDataNotFound()
        {
            PairAntenna? pairAntenna = null;

            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);

            var result = await _controller.getPairAntennaById(Guid.NewGuid());

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async void getPairAntennasByRoomId_returnsOk_WhenDataFound()
        {
            List<PairAntenna> pairAntennas = new List<PairAntenna>();
            pairAntennas.Add(new PairAntenna());

            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennasByRoomId(A<Guid>._)).Returns(pairAntennas);

            var result = await _controller.getPairAntennasByRoomId(Guid.NewGuid());

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void getPairAntennasByRoomId_returnsOk_WhenDataNotFound()
        {
            List<PairAntenna> pairAntenna = new List<PairAntenna>();

            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennasByRoomId(A<Guid>._)).Returns(pairAntenna);

            var result = await _controller.getPairAntennasByRoomId(Guid.NewGuid());

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void getPairAntennaByIPAddress_returnsOk_WhenDataFound()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();

            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntenna);

            var result = await _controller.getPairAntennaByIPAddress("");

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void getPairAntennaByIPAddress_returnsNotFound_WhenDataNotFound()
        {
            PairAntenna? pairAntenna = null;

            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntenna);

            var result = await _controller.getPairAntennaByIPAddress("");

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async void AddPairAntenna_returnsCreatedAtAction_WhenPairAntennaCreated()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01X = 0;
            pairAntenna.antenna01Y = 5;
            pairAntenna.antenna02X = 1;
            pairAntenna.antenna02Y = 4;
            PairAntenna? pairAntennaVerify = null;
            Room room = A.Fake<Room>();
            room.Width = 10;
            room.Length = 20;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntennaVerify);
            A.CallTo(() => _unitOfWork.PairAntennas.AddPairAntenna(A<PairAntenna>._)).Returns(true);

            var result = await _controller.AddPairAntenna(new PairAntennaResource());
            var resultAction = result as ObjectResult;

            resultAction.Should().NotBeNull();
            resultAction.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async void AddPairAntenna_returnsStatusCode500_WhenPairAntennaNotCreated()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01X = 0;
            pairAntenna.antenna01Y = 5;
            pairAntenna.antenna02X = 1;
            pairAntenna.antenna02Y = 4;
            PairAntenna? pairAntennaVerify = null;
            Room room = A.Fake<Room>();
            room.Width = 10;
            room.Length = 20;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntennaVerify);
            A.CallTo(() => _unitOfWork.PairAntennas.AddPairAntenna(A<PairAntenna>._)).Returns(false);

            var result = await _controller.AddPairAntenna(new PairAntennaResource());
            var resultAction = result as ObjectResult;

            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);
        }

        [Fact]
        public async void AddPairAntenna_returnsConflict_WhenPairAntennaWithIPAddressAlreadyExists()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01X = 0;
            pairAntenna.antenna01Y = 5;
            pairAntenna.antenna02X = 1;
            pairAntenna.antenna02Y = 4;
            PairAntenna pairAntennaVerify = A.Fake<PairAntenna>();
            Room room = A.Fake<Room>();
            room.Width = 10;
            room.Length = 20;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntennaVerify);

            var result = await _controller.AddPairAntenna(new PairAntennaResource());

            result.Should().NotBeNull();
            result.Should().BeOfType<ConflictObjectResult>();
            
        }

        [Fact]
        public async void AddPairAntenna_returnsBadRequest_WhenPairAntennaYPositionBiggerThanRoomLength()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01X = 0;
            pairAntenna.antenna01Y = 25;
            pairAntenna.antenna02X = 1;
            pairAntenna.antenna02Y = 30;
            Room room = A.Fake<Room>();
            room.Width = 10;
            room.Length = 20;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);

            var result = await _controller.AddPairAntenna(new PairAntennaResource());

            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void AddPairAntenna_returnsBadRequest_WhenPairAntennaXPositionBiggerThanRoomWidth()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01X = 20;
            pairAntenna.antenna01Y = 15;
            pairAntenna.antenna02X = 22;
            pairAntenna.antenna02Y = 18;
            Room room = A.Fake<Room>();
            room.Width = 10;
            room.Length = 20;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);

            var result = await _controller.AddPairAntenna(new PairAntennaResource());

            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void AddPairAntenna_returnsBadRequest_WhenPairAntennaYPositionLessThanZero()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01X = 0;
            pairAntenna.antenna01Y = -2;
            pairAntenna.antenna02X = 1;
            pairAntenna.antenna02Y = -4;
            Room room = A.Fake<Room>();
            room.Width = 10;
            room.Length = 20;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);

            var result = await _controller.AddPairAntenna(new PairAntennaResource());

            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void AddPairAntenna_returnsBadRequest_WhenPairAntennaXPositionLessThanZero()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01X = -2;
            pairAntenna.antenna01Y = 5;
            pairAntenna.antenna02X = -4;
            pairAntenna.antenna02Y = 8;
            Room room = A.Fake<Room>();
            room.Width = 10;
            room.Length = 20;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);

            var result = await _controller.AddPairAntenna(new PairAntennaResource());

            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void AddPairAntenna_returnsNotFound_WhenRoomNotFound()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            Room? room = null;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);

            var result = await _controller.AddPairAntenna(new PairAntennaResource());

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async void UpdatePairAntenna_returnsNoContent_WhenPairAntennaUpdated()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01X = 0;
            pairAntenna.antenna01Y = 5;
            pairAntenna.antenna02X = 1;
            pairAntenna.antenna02Y = 4;
            PairAntenna? pairAntennaVerify = null;
            Room room = A.Fake<Room>();
            room.Width = 10;
            room.Length = 20;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntennaVerify);
            A.CallTo(() => _unitOfWork.PairAntennas.UpdatePairAntenna(A<Guid>._,A<PairAntenna>._)).Returns(true);

            var result = await _controller.UpdatePairAntenna(Guid.NewGuid(),new PairAntennaResource());

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async void UpdatePairAntenna_returnsStatusCode500_WhenPairAntennaNotUpdated()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01X = 0;
            pairAntenna.antenna01Y = 5;
            pairAntenna.antenna02X = 1;
            pairAntenna.antenna02Y = 4;
            PairAntenna? pairAntennaVerify = null;
            Room room = A.Fake<Room>();
            room.Width = 10;
            room.Length = 20;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntennaVerify);
            A.CallTo(() => _unitOfWork.PairAntennas.UpdatePairAntenna(A<Guid>._, A<PairAntenna>._)).Returns(false);

            var result = await _controller.UpdatePairAntenna(Guid.NewGuid(), new PairAntennaResource());
            var resultAction = result as ObjectResult;


            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);
        }

        [Fact]
        public async void UpdatePairAntenna_returnsConfict_WhenPairAntennaWithIPAddressAlreadyExists()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01X = 0;
            pairAntenna.antenna01Y = 5;
            pairAntenna.antenna02X = 1;
            pairAntenna.antenna02Y = 4;
            PairAntenna pairAntennaVerify = A.Fake<PairAntenna>();
            Room room = A.Fake<Room>();
            room.Width = 10;
            room.Length = 20;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaByIPAddress(A<string>._)).Returns(pairAntennaVerify);

            var result = await _controller.UpdatePairAntenna(Guid.NewGuid(), new PairAntennaResource());

            result.Should().NotBeNull();
            result.Should().BeOfType<ConflictObjectResult>();
        }

        [Fact]
        public async void UpdatePairAntenna_returnsBadRequest_WhenYPositionBiggerThanRoomLength()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01X = 0;
            pairAntenna.antenna01Y = 15;
            pairAntenna.antenna02X = 1;
            pairAntenna.antenna02Y = 50;
            Room room = A.Fake<Room>();
            room.Width = 10;
            room.Length = 20;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);

            var result = await _controller.UpdatePairAntenna(Guid.NewGuid(), new PairAntennaResource());

            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void UpdatePairAntenna_returnsBadRequest_WhenXPositionBiggerThanRoomWidth()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01X = 20;
            pairAntenna.antenna01Y = 5;
            pairAntenna.antenna02X = 30;
            pairAntenna.antenna02Y = 10;
            Room room = A.Fake<Room>();
            room.Width = 10;
            room.Length = 20;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);

            var result = await _controller.UpdatePairAntenna(Guid.NewGuid(), new PairAntennaResource());

            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void UpdatePairAntenna_returnsBadRequest_WhenYPositionLessThanZero()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01X = 0;
            pairAntenna.antenna01Y = 5;
            pairAntenna.antenna02X = 1;
            pairAntenna.antenna02Y = -2;
            Room room = A.Fake<Room>();
            room.Width = 10;
            room.Length = 20;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);

            var result = await _controller.UpdatePairAntenna(Guid.NewGuid(), new PairAntennaResource());

            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void UpdatePairAntenna_returnsBadRequest_WhenXPositionLessThanZero()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01X = 0;
            pairAntenna.antenna01Y = 5;
            pairAntenna.antenna02X = -1;
            pairAntenna.antenna02Y = 2;
            Room room = A.Fake<Room>();
            room.Width = 10;
            room.Length = 20;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);

            var result = await _controller.UpdatePairAntenna(Guid.NewGuid(), new PairAntennaResource());

            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void UpdatePairAntenna_returnsNotFound_WhenRoomDoesntExist()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01X = 0;
            pairAntenna.antenna01Y = 5;
            pairAntenna.antenna02X = -1;
            pairAntenna.antenna02Y = 2;
            Room? room = null;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);

            var result = await _controller.UpdatePairAntenna(Guid.NewGuid(), new PairAntennaResource());

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async void UpdatePairAntenna_returnsNotFound_WhenPairAntennaNotFound()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            pairAntenna.antenna01X = 0;
            pairAntenna.antenna01Y = 5;
            pairAntenna.antenna02X = -1;
            pairAntenna.antenna02Y = 2;
            PairAntenna? pairAntennaVerify = null;

            A.CallTo(() => _mapper.Map<PairAntennaResource, PairAntenna>(A<PairAntennaResource>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntennaVerify);


            var result = await _controller.UpdatePairAntenna(Guid.NewGuid(), new PairAntennaResource());

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async void DeletePairAntenna_returnsNoContent_WhenPairAntennaDeleted()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();

            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.PairAntennas.DeletePairAntenna(A<Guid>._)).Returns(true);


            var result = await _controller.DeletePairAntenna(Guid.NewGuid());

            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async void DeletePairAntenna_returnsStatusCode500_WhenPairAntennaNotDeleted()
        {
            PairAntenna pairAntenna = A.Fake<PairAntenna>();

            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.PairAntennas.DeletePairAntenna(A<Guid>._)).Returns(false);


            var result = await _controller.DeletePairAntenna(Guid.NewGuid());
            var resultAction = result as ObjectResult;

            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);

        }

        [Fact]
        public async void DeletePairAntenna_returnsNotFound_WhenPairAntennaNotFound()
        {
            PairAntenna? pairAntenna = null;

            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);


            var result = await _controller.DeletePairAntenna(Guid.NewGuid());

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
        }

    }
}
