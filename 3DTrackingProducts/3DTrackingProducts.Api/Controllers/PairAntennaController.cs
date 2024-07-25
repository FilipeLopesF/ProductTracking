using _3DTrackingProducts.Api.Core;
using _3DTrackingProducts.Api.Migrations;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace _3DTrackingProducts.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class PairAntennaController : ControllerBase
    {
        private readonly ILogger<PairAntennaController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PairAntennaController(ILogger<PairAntennaController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("antennas/all")]
        public async Task<IActionResult> getAllPairAntennas()
        {
            return Ok(await _unitOfWork.PairAntennas.getAllPairAntennas());
        }

        [HttpGet("antennas/{id}")]
        public async Task<IActionResult> getPairAntennaById(Guid id)
        {
            PairAntenna? pairAntenna = await _unitOfWork.PairAntennas.getPairAntennaById(id);
            if(pairAntenna == null)
            {
                return NotFound($"No Pair Antenna with id: {id} not found");
            }
            return Ok(pairAntenna);
        }

        [HttpGet("rooms/{id}/antennas")]
        public async Task<IActionResult> getPairAntennasByRoomId(Guid id)
        {
            return Ok(await _unitOfWork.PairAntennas.getPairAntennasByRoomId(id));
        }

        [HttpGet("antennas/ips/{ipAddress}")]
        public async Task<IActionResult> getPairAntennaByIPAddress(string ipAddress)
        {
            PairAntenna? pairAntenna = await _unitOfWork.PairAntennas.getPairAntennaByIPAddress(ipAddress);
            if (pairAntenna == null)
            {
                return NotFound($"No Pair Antenna with ipAddress: {ipAddress} found");
            }
            return Ok(pairAntenna);
        }

        [HttpPost("antennas")]
        public async Task<IActionResult> AddPairAntenna(PairAntennaResource pairAntennaResource)
        {
            try
            {

                var pairAntenna = _mapper.Map<PairAntennaResource, PairAntenna>(pairAntennaResource);

                Room? room = await _unitOfWork.Rooms.getRoomById(pairAntenna.idRoom);
                if (room == null)
                {
                    return NotFound($"No Room with id: {pairAntenna.idRoom} found");
                }

                if (pairAntenna.antenna01X < 0 || pairAntenna.antenna02X < 0)
                {
                    return BadRequest("X Position of one of the antennas is less that 0");
                }

                if (pairAntenna.antenna01Y < 0 || pairAntenna.antenna02Y < 0)
                {
                    return BadRequest("Y Position of one of the antennas is less that 0");
                }

                if (pairAntenna.antenna01X > room.Width || pairAntenna.antenna02X > room.Width)
                {
                    return BadRequest("X Position of one of the antennas is bigger than the room width");
                }

                if (pairAntenna.antenna01Y > room.Length || pairAntenna.antenna02Y > room.Length)
                {
                    return BadRequest("Y Position of one of the antennas is bigger than the room length");
                }

                PairAntenna? pairAntennaVerifyIpAddress = await _unitOfWork.PairAntennas.getPairAntennaByIPAddress(pairAntenna.antenna01IP);
                if (pairAntennaVerifyIpAddress != null)
                {
                    return Conflict($"Pair Antenna with ipAddress: {pairAntenna.antenna01IP} already exists");
                }

                pairAntennaVerifyIpAddress = await _unitOfWork.PairAntennas.getPairAntennaByIPAddress(pairAntenna.antenna02IP);
                if (pairAntennaVerifyIpAddress != null)
                {
                    return Conflict($"Pair Antenna with ipAddress: {pairAntenna.antenna02IP} already exists");
                }

                if (!await _unitOfWork.PairAntennas.AddPairAntenna(pairAntenna))
                {
                    return StatusCode(500, $"There was an error that prevented the service from inserting pair antenna: {pairAntenna}");
                }

                var result = CreatedAtAction(nameof(AddPairAntenna), new { id = pairAntenna.Id }, pairAntenna);

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, $"There was an error that prevented the service from inserting pair antenna");
            }

        }

        [HttpPut("antennas/{id}")]
        public async Task<IActionResult> UpdatePairAntenna(Guid id,PairAntennaResource pairAntennaResource)
        {
            try
            {
                var pairAntenna = _mapper.Map<PairAntennaResource, PairAntenna>(pairAntennaResource);

                PairAntenna? pairAntennaVerifyId = await _unitOfWork.PairAntennas.getPairAntennaById(id);
                if (pairAntennaVerifyId == null)
                {
                    return NotFound($"No Pair Antenna with id: {id} found");
                }

                Room? room = await _unitOfWork.Rooms.getRoomById(pairAntenna.idRoom);
                if (room == null)
                {
                    return NotFound($"No Room with id: {pairAntenna.idRoom} found");
                }

                if (pairAntenna.antenna01X < 0 || pairAntenna.antenna02X < 0)
                {
                    return BadRequest("X Position of one of the antennas is less that 0");
                }

                if (pairAntenna.antenna01Y < 0 || pairAntenna.antenna02Y < 0)
                {
                    return BadRequest("Y Position of one of the antennas is less that 0");
                }

                if (pairAntenna.antenna01X > room.Width || pairAntenna.antenna02X > room.Width)
                {
                    return BadRequest("X Position of one of the antennas is bigger than the room width");
                }

                if (pairAntenna.antenna01Y > room.Length || pairAntenna.antenna02Y > room.Length)
                {
                    return BadRequest("Y Position of one of the antennas is bigger than the room length");
                }


                PairAntenna? pairAntennaVerifyIpAddress = await _unitOfWork.PairAntennas.getPairAntennaByIPAddress(pairAntenna.antenna01IP);
                if (pairAntennaVerifyIpAddress != null && pairAntennaVerifyIpAddress.Id != id)
                {
                    return Conflict($"Pair Antenna with ipAddress: {pairAntenna.antenna01IP} already exists");
                }

                pairAntennaVerifyIpAddress = await _unitOfWork.PairAntennas.getPairAntennaByIPAddress(pairAntenna.antenna02IP);
                if (pairAntennaVerifyIpAddress != null && pairAntennaVerifyIpAddress.Id != id)
                {
                    return Conflict($"Pair Antenna with ipAddress: {pairAntenna.antenna02IP} already exists");
                }

                if (!await _unitOfWork.PairAntennas.UpdatePairAntenna(id,pairAntenna))
                {
                    return StatusCode(500, $"There was an error that prevented the service from inserting pair antenna: {pairAntenna}");
                }

                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, $"There was an error that prevented the service from updating pair antenna");
            }
        }

        [HttpPut("antennas/{id}/detection")]
        public async Task<IActionResult> UpdatePairAntennaDetection(Guid id, PairAntennaDetectionResource pairAntennaDetectionResource)
        {
            try
            {
                var pairAntenna = _mapper.Map<PairAntennaDetectionResource, PairAntenna>(pairAntennaDetectionResource);

                PairAntenna? pairAntennaVerifyId = await _unitOfWork.PairAntennas.getPairAntennaById(id);
                if (pairAntennaVerifyId == null)
                {
                    return NotFound($"No Pair Antenna with id: {id} found");
                }

                pairAntenna.LastVerificationTimeStamp = DateTime.Now;

                if (!await _unitOfWork.PairAntennas.UpdatePairAntenna(id, pairAntenna))
                {
                    return StatusCode(500, $"There was an error that prevented the service from inserting pair antenna: {pairAntenna}");
                }

                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, $"There was an error that prevented the service from updating pair antenna");
            }
        }

        [HttpDelete("antennas/{id}")]
        public async Task<IActionResult> DeletePairAntenna(Guid id)
        {
            try
            {

                PairAntenna? pairAntenna = await _unitOfWork.PairAntennas.getPairAntennaById(id);
                if (pairAntenna == null)
                {
                    return NotFound($"No Pair Antenna with id: {id} found");
                }

                if (!await _unitOfWork.PairAntennas.DeletePairAntenna(id))
                {
                    return StatusCode(500, $"There was an error that prevented the service from deleting pair antenna: {pairAntenna}");
                }

                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, $"There was an error that prevented the service from deleting pair antenna");
            }
        }
    }
}
