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
    [Route("api/rooms")]
    public class RoomController : ControllerBase
    {

        private readonly ILogger<RoomController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomController(ILogger<RoomController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRooms()
        {
            return Ok(await _unitOfWork.Rooms.getAllRooms());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(Guid id)
        {
            Room? room = await _unitOfWork.Rooms.getRoomById(id);
            if (room == null)
            {
                return NotFound($"No Room with id: {id} found");
            }
            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom([FromForm] RoomResource roomResource)
        {
            try
            {
                Room? room = await _unitOfWork.Rooms.getRoomByName(roomResource.Name);
                if (room != null)
                {
                    return Conflict($"Room with name: {roomResource.Name}");
                }

                Room roomToAdd = new Room
                {
                    Name = roomResource.Name,
                    Length = roomResource.Length,
                    Width = roomResource.Width
                };

                if (roomResource.imageByte != null)
                {
                    IFormFile file = roomResource.imageByte;
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    roomToAdd.imageByte = ms.ToArray();
                    ms.Close();
                    ms.Dispose();
                }

                if (!await _unitOfWork.Rooms.AddRoom(roomToAdd))
                {
                    return StatusCode(500, $"There was an error that prevented the service from inserting room: {room}");
                }

                return CreatedAtAction(nameof(AddRoom), new { Name = roomToAdd.Name }, roomToAdd);

            }catch(Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, $"There was an error that prevented the service from inserting room: {roomResource}");
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(Guid id, [FromForm] RoomResource roomResource)
        {
            try 
            {

                Room? roomToUpdate = await _unitOfWork.Rooms.getRoomById(id);
                if (roomToUpdate == null)
                {
                    return NotFound($"No Room with id: {id} found");
                }
            
                if (roomResource.Name != roomToUpdate.Name)
                {
                    Room? roomNameVerification = await _unitOfWork.Rooms.getRoomByName(roomResource.Name);
                    if (roomNameVerification != null)
                    {
                        return Conflict($"Room with name: {roomResource.Name} already exists");
                    }   
                }

                Room room = new Room
                {
                    Name = roomResource.Name,
                    Length = roomResource.Length,
                    Width = roomResource.Width
                };

                if (roomResource.imageByte != null)
                {
                
                    using(MemoryStream ms = new MemoryStream())
                    {
                        IFormFile file = roomResource.imageByte;
                        file.CopyTo(ms);

                        if (ms.ToArray() != roomToUpdate.imageByte)
                        {
                            roomToUpdate.imageByte = ms.ToArray();
                        }

                        ms.Close();
                        ms.Dispose();
                    }
                }

                if (!await _unitOfWork.Rooms.UpdateRoom(id, room))
                {
                    return StatusCode(500, $"There was an error that prevented the service from updating room with id: {id}");
                }
                
                return NoContent();

            }catch(Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, $"There was an error that prevented the service from inserting room: {roomResource}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            Room? roomToUpdate = await _unitOfWork.Rooms.getRoomById(id);
            if (roomToUpdate == null)
            {
                return NotFound($"No Room with id: {id} found");
            }

            if (!await _unitOfWork.Rooms.DeleteRoom(id))
            {
                return StatusCode(500, $"There was an error that prevented the service from deleting room with id: {id}");
            }
            return NoContent();
        }
    }
}
