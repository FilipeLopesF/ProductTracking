using System;
using _3DTrackingProducts.Api.Core;
using _3DTrackingProducts.Api.Dtos;
using _3DTrackingProducts.Api.Migrations;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _3DTrackingProducts.Api.Controllers
{
	[ApiController]
	[Route("api/controlTag")]
	public class ControlTagController : ControllerBase
	{
		private readonly ILogger<LogController> _logger;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        private const int LAST_LOG_MINUTES = 10;

        public ControlTagController(ILogger<LogController> logger, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		[HttpPost("")]
		public async Task<IActionResult> Create(ControlTagResource controlTagResource)
		{
			var controlTag = _mapper.Map<ControlTagResource, ControlTag>(controlTagResource);

			await _unitOfWork.ControlTag.Add(controlTag);
			await _unitOfWork.ControlTag.Save();

			return Ok();
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAll()
		{
			IList<ControlTagDto> controlTags = new List<ControlTagDto>();
			var result = await _unitOfWork.ControlTag.GetAll();
			foreach(var r in result)
			{
				Room? room = await _unitOfWork.Rooms.getRoomById(r.RoomId);
                
                RoomDto roomDto = new RoomDto
                {
                    Id = room.id,
                    Name = room.Name,
                    Width = room.Width,
                    Length = room.Length
                };

				controlTags.Add(
					new ControlTagDto {
						Epc = r.EPC,
						PositionX = r.PositionX,
						PositionY = r.PositionY,
						Room = roomDto
                    }
				);
			};
		
			return Ok(controlTags);
		}

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetTagsByRoomId(string roomId)
        {
			var result = await _unitOfWork.ControlTag.GetControlTagsByRoomAsync(Guid.Parse(roomId));

			if (result == null)
				return NotFound("The specified room does not have tags associated.");
			
            IList<ControlTagDto> controlTags = new List<ControlTagDto>();
            foreach(var r in result){
                Room? room = await _unitOfWork.Rooms.getRoomById(r.RoomId);
                
                RoomDto roomDto = new RoomDto
                {
                    Id = room.id,
                    Name = room.Name,
                    Width = room.Width,
                    Length = room.Length
                };


                controlTags.Add(
                    new ControlTagDto
                    {
                        Epc = r.EPC,
                        PositionX = r.PositionX,
                        PositionY = r.PositionY,
                        Room = roomDto
                    }
                    );
            };

            return Ok(controlTags);
        }

        [HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string epc)
		{
			var tag = (await _unitOfWork.ControlTag.Find(c => c.EPC == epc)).ToList()[0];

			if (tag != null)
				await _unitOfWork.ControlTag.Remove(tag);
				await _unitOfWork.ControlTag.Save();

			return Ok();
		}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string epc, ControlTagResource controlTagResource)
        {
            var controlTag = _mapper.Map<ControlTagResource, ControlTag>(controlTagResource);
			var updatedControlTag = await _unitOfWork.ControlTag.UpdateControlTagAsync(epc, controlTag);

			if (updatedControlTag == null)
				return NotFound("This tag does not exist.");
			
            return Ok(updatedControlTag);
        }

        [HttpGet("{epc}/antennas/{pairAntennaId}/position")]
        public async Task<IActionResult> GetPositionFromControlTagWithEPCForPairAntennaWithId(string epc, Guid pairAntennaId)
        {
            ControlTag? controlTag = await _unitOfWork.ControlTag.GetControlTagByEPC(epc);
            if (controlTag == null)
            {
                return NotFound($"No control tag found with epc: {epc}");
            }

            PairAntenna? pairAntenna = await _unitOfWork.PairAntennas.getPairAntennaById(pairAntennaId);
            if (pairAntenna == null)
            {
                return NotFound($"No pair antenna found with id: {pairAntennaId}");
            }

            Room? room = await _unitOfWork.Rooms.getRoomById(pairAntenna.idRoom);
            if (pairAntenna == null)
            {
                return NotFound($"No room found for pairAntenna with id: {pairAntennaId}");
            }

            //Search Logs from IPAddress1
            List<Log> logsFromIPAddress1 = await _unitOfWork.Logs.GetLogsFromIPAddressWithTagEPCInLastXMinutes(pairAntenna.antenna01IP, epc, LAST_LOG_MINUTES);
            if (logsFromIPAddress1.Count == 0 || logsFromIPAddress1 == null)
            {
                return NotFound($"No logs found from IPAddress: {pairAntenna.antenna01IP} in the last {LAST_LOG_MINUTES} minutes");
            }

            //Search Logs from IPAddress2
            List<Log> logsFromIPAddress2 = await _unitOfWork.Logs.GetLogsFromIPAddressWithTagEPCInLastXMinutes(pairAntenna.antenna02IP, epc, LAST_LOG_MINUTES);
            if (logsFromIPAddress2.Count == 0 || logsFromIPAddress2 == null)
            {
                return NotFound($"No logs found from IPAddress: {pairAntenna.antenna02IP} in the last {LAST_LOG_MINUTES} minutes");
            }

            try
            {
                Tuple<int, int> antenna01Position = new Tuple<int, int>(pairAntenna.antenna01X, pairAntenna.antenna01Y);
                Tuple<int, int> antenna02Position = new Tuple<int, int>(pairAntenna.antenna02X, pairAntenna.antenna02Y);

                CalculatePositionDto result = CalculatePosition.calculate(logsFromIPAddress1, antenna01Position, logsFromIPAddress2, antenna02Position, room);
                result.pairAntenna = new PairAntennaDto
                {
                    Id = pairAntenna.Id,
                    antenna01IP = pairAntenna.antenna01IP,
                    antenna01X = pairAntenna.antenna01X,
                    antenna01Y = pairAntenna.antenna01Y,
                    antenna02IP = pairAntenna.antenna02IP,
                    antenna02X = pairAntenna.antenna02X,
                    antenna02Y = pairAntenna.antenna02Y
                };

                result.room = new RoomDto
                {
                    Name = room.Name,
                    Width = room.Width,
                    Length = room.Length,
                    imageByte = room.imageByte
                };

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

