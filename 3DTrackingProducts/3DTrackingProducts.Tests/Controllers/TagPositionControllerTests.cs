using _3DTrackingProducts.Api.Controllers;
using _3DTrackingProducts.Api.Core;
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
using _3DTrackingProducts.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using _3DTrackingProducts.Api.Resources;

namespace _3DTrackingProductsTests.Controllers
{
    public class TagPositionControllerTests
    {
        private ILogger<TagPositionController> _logger;
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        private TagPositionController _controller;

        public TagPositionControllerTests()
        {
            _logger = A.Fake<ILogger<TagPositionController>>();
            _unitOfWork = A.Fake<UnitOfWork>();
            _mapper = A.Fake<IMapper>();

            _unitOfWork.Tags = A.Fake<ITagRepository>();
            _unitOfWork.PairAntennas = A.Fake<IPairAntennaRepository>();
            _unitOfWork.TagPositions = A.Fake<ITagPositionRepository>();
            _unitOfWork.Rooms = A.Fake<IRoomRepository>();

            _controller = new TagPositionController(_logger,_unitOfWork,_mapper);
        }

        [Fact]
        public async void GetAllTagPositions_returnsOkAsync()
        {
            List<TagPosition> tagPositions = A.Fake<List<TagPosition>>();

            A.CallTo(() => _unitOfWork.TagPositions.GetAllTagPositions()).Returns(tagPositions);

            var result = await _controller.GetAllTagPositions();

            result.Should().BeAssignableTo<OkObjectResult>();
        }

        [Fact]
        public async void GetTagPositionsFromTagWithEPC_returnsOkAsync()
        {
            string epc = "EPC";
            List<TagPosition> tagPositions = A.Fake<List<TagPosition>>();
            tagPositions.Add(new TagPosition());

            A.CallTo(() => _unitOfWork.TagPositions.GetTagPositionsFromTagWithEPC(A<string>._)).Returns(tagPositions);

            var result = await _controller.GetTagPositionsFromTagWithEPC(epc);

            result.Should().BeAssignableTo<OkObjectResult>();
        }

        [Fact]
        public async void GetTagPositionsFromTagWithEPC_returnsBadRequest_WhenEPCEmpty()
        {
            string epc = "";
     
            var result = await _controller.GetTagPositionsFromTagWithEPC(epc);

            result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async void GetTagPositionsFromTagWithEPC_returnsNotFound_WhenTagPositionFromTagWithEPCNotFound()
        {
            string epc = "EPC";
            List<TagPosition> tagPositions = A.Fake<List<TagPosition>>();

            A.CallTo(() => _unitOfWork.TagPositions.GetTagPositionsFromTagWithEPC(A<string>._)).Returns(tagPositions);

            var result = await _controller.GetTagPositionsFromTagWithEPC(epc);

            result.Should().BeAssignableTo<NotFoundObjectResult>();
        }

        [Fact]
        public async void GetLastTagPositionsFromTagWithEPC_returnsOkAsync()
        {
            string epc = "EPC";
            TagPosition tagPositions = A.Fake<TagPosition>();
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            Room room = A.Fake<Room>();

            A.CallTo(() => _unitOfWork.TagPositions.GetLastTagPositionsFromTagWithEPC(A<string>._)).Returns(tagPositions);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.Rooms.getRoomById(A<Guid>._)).Returns(room);

            var result = await _controller.GetLastTagPositionFromTagWithEPC(epc);

            result.Should().BeAssignableTo<OkObjectResult>();
        }

        [Fact]
        public async void GetLastTagPositionFromTagWithEPC_returnsBadRequest_WhenEPCEmpty()
        {
            string epc = "";

            var result = await _controller.GetLastTagPositionFromTagWithEPC(epc);

            result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async void GetLastTagPositionFromTagWithEPC_returnsNotFound_WhenTagPositionFromTagWithEPCNotFound()
        {
            string epc = "EPC";
            TagPosition? tagPositions = null;

            A.CallTo(() => _unitOfWork.TagPositions.GetLastTagPositionsFromTagWithEPC(A<string>._)).Returns(tagPositions);

            var result = await _controller.GetLastTagPositionFromTagWithEPC(epc);

            result.Should().BeAssignableTo<NotFoundObjectResult>();
        }


        [Fact]
        public async void AddTagPosition_returnsCreatedAtAction()
        {
            string epc = "EPC";
            Tag tag = A.Fake<Tag>();
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            TagPositionResource tagPositionResource = A.Fake<TagPositionResource>();
            TagPosition tagPosition = A.Fake<TagPosition>();

            A.CallTo(() => _mapper.Map<TagPositionResource, TagPosition>(A<TagPositionResource>._)).Returns(tagPosition);
            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.TagPositions.AddTagPosition(A<TagPosition>._)).Returns(true);

            var result = await _controller.AddTagPosition(tagPositionResource);

            result.Should().BeAssignableTo<CreatedAtActionResult>();
        }

        [Fact]
        public async void AddTagPosition_returnsNotFound_WhenTagDoesntExist()
        {
            string epc = "EPC";
            Tag? tag = null;
            TagPositionResource tagPositionResource = A.Fake<TagPositionResource>();

            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);

            var result = await _controller.AddTagPosition(tagPositionResource);

            result.Should().BeAssignableTo<NotFoundObjectResult>();
        }

        [Fact]
        public async void AddTagPosition_returnsNotFound_WhenPairAntennaDoesntExist()
        {
            string epc = "EPC";
            Tag tag = A.Fake<Tag>();
            PairAntenna? pairAntenna = null;
            TagPositionResource tagPositionResource = A.Fake<TagPositionResource>();

            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);

            var result = await _controller.AddTagPosition(tagPositionResource);

            result.Should().BeAssignableTo<NotFoundObjectResult>();
        }

        [Fact]
        public async void AddTagPosition_returnsStatusCode500_WhenTagPositionFailedToBeAdded()
        {
            string epc = "EPC";
            Tag tag = A.Fake<Tag>();
            PairAntenna pairAntenna = A.Fake<PairAntenna>();
            TagPositionResource tagPositionResource = A.Fake<TagPositionResource>();
            TagPosition tagPosition = A.Fake<TagPosition>();

            A.CallTo(() => _mapper.Map<TagPositionResource, TagPosition>(A<TagPositionResource>._)).Returns(tagPosition);
            A.CallTo(() => _unitOfWork.Tags.GetTagWithEPC(A<string>._)).Returns(tag);
            A.CallTo(() => _unitOfWork.PairAntennas.getPairAntennaById(A<Guid>._)).Returns(pairAntenna);
            A.CallTo(() => _unitOfWork.TagPositions.AddTagPosition(A<TagPosition>._)).Returns(false);

            var result = await _controller.AddTagPosition(tagPositionResource);
            var resultAction = result as ObjectResult;

            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);
        }

        [Fact]
        public async void DeleteTagPositionsFromTagWithEPC_returnsNoContent()
        {
            string epc = "EPC";
            List<TagPosition> tagPositions = A.Fake<List<TagPosition>>();
            tagPositions.Add(new TagPosition());

            A.CallTo(() => _unitOfWork.TagPositions.GetTagPositionsFromTagWithEPC(A<string>._)).Returns(tagPositions);
            A.CallTo(() => _unitOfWork.TagPositions.DeleteTagPositionsFromTagWithEPC(A<string>._)).Returns(true);

            var result = await _controller.DeleteTagPositionsFromTagWithEPC(epc);

            result.Should().BeAssignableTo<NoContentResult>();
        }

        [Fact]
        public async void DeleteTagPositionsFromTagWithEPC_returnsBadRequest_WhenEPCIsEmpty()
        {
            string epc = "";

            var result = await _controller.DeleteTagPositionsFromTagWithEPC(epc);

            result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async void DeleteTagPositionsFromTagWithEPC_returnsNotFound_WhenNoTagPositionWithEPCFound()
        {
            string epc = "EPC";
            List<TagPosition> tagPositions = A.Fake<List<TagPosition>>();

            A.CallTo(() => _unitOfWork.TagPositions.GetTagPositionsFromTagWithEPC(A<string>._)).Returns(tagPositions);

            var result = await _controller.DeleteTagPositionsFromTagWithEPC(epc);

            result.Should().BeAssignableTo<NotFoundObjectResult>();
        }

        [Fact]
        public async void DeleteTagPositionsFromTagWithEPC_returnsStatusCode500_WhenTagPositionFailedToBeAdded()
        {
            string epc = "EPC";
            List<TagPosition> tagPositions = A.Fake<List<TagPosition>>();
            tagPositions.Add(new TagPosition());

            A.CallTo(() => _unitOfWork.TagPositions.GetTagPositionsFromTagWithEPC(A<string>._)).Returns(tagPositions);
            A.CallTo(() => _unitOfWork.TagPositions.DeleteTagPositionsFromTagWithEPC(A<string>._)).Returns(false);

            var result = await _controller.DeleteTagPositionsFromTagWithEPC(epc);
            var resultAction = result as ObjectResult;

            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);
        }

    }
}
