using _3DTrackingProducts.Api.Controllers;
using _3DTrackingProducts.Api.Persistence;
using AutoMapper;
using Microsoft.Extensions.Logging;
using FakeItEasy;
using _3DTrackingProducts.Api.Core.Repositories;
using _3DTrackingProducts.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using _3DTrackingProducts.Api.Resources;
using Microsoft.AspNetCore.Http;

namespace _3DTrackingProductsTests.Controllers
{
    public class RoomControllerTests
    {
        private ILogger<RoomController> _logger;
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        private RoomController controller;
        public RoomControllerTests()
        {
            _logger = A.Fake<ILogger<RoomController>>();
            _mapper = A.Fake<IMapper>();
            _unitOfWork = A.Fake<UnitOfWork>();

            _unitOfWork.Rooms = A.Fake<IRoomRepository>();

            controller = new RoomController(_logger, _unitOfWork, _mapper)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
        }

        [Fact]
        public async void GetAllRooms_returnsOk_WhenDataFound()
        {
            List<Room> rooms = new List<Room>();
            rooms.Add(new Room());
            
            A.CallTo(() => _unitOfWork.Rooms.getAllRooms()).Returns(rooms);

            var result = await controller.GetAllRooms();

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetAllRooms_returnsOk_WhenDataNotFound()
        {
            List<Room> rooms = new List<Room>();

            A.CallTo(() => _unitOfWork.Rooms.getAllRooms()).Returns(rooms);

            var result = await controller.GetAllRooms();

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetRoomById_returnsOk_WhenDataFound()
        {
            Room room = A.Fake<Room>();
            Guid id = Guid.NewGuid();
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);

            var result = await controller.GetRoomById(id);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetRoomById_returnsNotFound_WhenDataNotFound()
        {
            Room? room = null;
            Guid id = Guid.NewGuid();
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);

            var result = await controller.GetRoomById(id);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async void AddRoom_returnsCreatedAtAction_WhenRoomAdded()
        {
            Room? room = null;
            RoomResource roomToAdd = A.Fake<RoomResource>();

            A.CallTo(() => _unitOfWork.Rooms.getRoomByName(A<string>._)).Returns(room);
            A.CallTo(() => _unitOfWork.Rooms.AddRoom(A<Room>._)).Returns(true);

            var result = await controller.AddRoom(roomToAdd);

            result.Should().BeOfType<CreatedAtActionResult>();

            A.CallTo(() => _unitOfWork.Rooms.getRoomByName(A<string>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void AddRoom_returnsConflict_WhenRoomWithIPAddressAlreadyExist()
        {
            Room room = A.Fake<Room>();
            RoomResource roomToAdd = A.Fake<RoomResource>();

            A.CallTo(() => _unitOfWork.Rooms.getRoomByName(A<string>._)).Returns(room);

            var result = await controller.AddRoom(roomToAdd);

            result.Should().BeOfType<ConflictObjectResult>();

            A.CallTo(() => _unitOfWork.Rooms.getRoomByName(A<string>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void AddRoom_returnsStatusCode500_WhenRoomNotAdded()
        {
            Room? room = null;
            RoomResource roomToAdd = A.Fake<RoomResource>();

            A.CallTo(() => _unitOfWork.Rooms.getRoomByName(A<string>._)).Returns(room);
            A.CallTo(() => _unitOfWork.Rooms.AddRoom(A<Room>._)).Returns(false);

            var result = await controller.AddRoom(roomToAdd);
            var resultAction = result as ObjectResult;

            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);

            A.CallTo(() => _unitOfWork.Rooms.getRoomByName(A<string>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void UpdateRoom_returnsNoContent_WhenRoomUpdated()
        {
            Room room = A.Fake<Room>();
            Room? roomByName = null;
            RoomResource roomToAdd = A.Fake<RoomResource>();

            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);
            A.CallTo(() => _unitOfWork.Rooms.getRoomByName(A<string>._)).Returns(roomByName);
            A.CallTo(() => _unitOfWork.Rooms.UpdateRoom(A<Guid>._,A<Room>._)).Returns(true);

            var result = await controller.UpdateRoom(Guid.NewGuid(),roomToAdd);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async void UpdateRoom_returnsNotFound_WhenRoomDoesntExist()
        {
            Room? room = null;
            RoomResource roomToAdd = A.Fake<RoomResource>();

            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);

            var result = await controller.UpdateRoom(Guid.NewGuid(), roomToAdd);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async void UpdateRoom_returnsConflict_WhenNameAlreadyTaken()
        {
            Room room = A.Fake<Room>();
            RoomResource roomToAdd = A.Fake<RoomResource>();
            roomToAdd.Name = "X";
            room.Name = "Y";

            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);
            A.CallTo(() => _unitOfWork.Rooms.getRoomByName(A<string>._)).Returns(room);

            var result = await controller.UpdateRoom(Guid.NewGuid(), roomToAdd);

            result.Should().BeOfType<ConflictObjectResult>();
        }

        [Fact]
        public async void UpdateRoom_returnsStatus500_WhenRoomNotUpdated()
        {
            Room room = A.Fake<Room>();
            Room? roomByName = null;
            RoomResource roomToAdd = A.Fake<RoomResource>();

            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);
            A.CallTo(() => _unitOfWork.Rooms.getRoomByName(A<string>._)).Returns(roomByName);
            A.CallTo(() => _unitOfWork.Rooms.UpdateRoom(A<Guid>._, A<Room>._)).Returns(false);

            var result = await controller.UpdateRoom(Guid.NewGuid(), roomToAdd);
            var resultAction = result as ObjectResult;

            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);
        }

        [Fact]
        public async void DeleteRoom_returnsNoContent_WhenRoomDeleted()
        {
            Room room = A.Fake<Room>();
            RoomResource roomToAdd = A.Fake<RoomResource>();

            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);
            A.CallTo(() => _unitOfWork.Rooms.DeleteRoom(A<Guid>._)).Returns(true);

            var result = await controller.DeleteRoom(Guid.NewGuid());


            result.Should().BeOfType<NoContentResult>();
            
        }

        [Fact]
        public async void DeleteRoom_returnsNotFound_WhenRoomDoesntExist()
        {
            Room? room = null;
            RoomResource roomToAdd = A.Fake<RoomResource>();

            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);

            var result = await controller.DeleteRoom(Guid.NewGuid());


            result.Should().BeOfType<NotFoundObjectResult>();

        }

        [Fact]
        public async void DeleteRoom_returnsStatus500_WhenRoomNotDeleted()
        {
            Room room = A.Fake<Room>();
            RoomResource roomToAdd = A.Fake<RoomResource>();

            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);
            A.CallTo(() => _unitOfWork.Rooms.DeleteRoom(A<Guid>._)).Returns(false);

            var result = await controller.DeleteRoom(Guid.NewGuid());
            var resultAction = result as ObjectResult;

            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);

        }
    }
}
