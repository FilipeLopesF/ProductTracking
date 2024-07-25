using _3DTrackingProducts.Api.Core;
using _3DTrackingProducts.Api.Core.Repositories;
using _3DTrackingProducts.Api.Data;
using _3DTrackingProducts.Api.Dtos;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Persistence.Repositories;
using _3DTrackingProducts.Api.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.Xml;

namespace _3DTrackingProducts.Api.Controllers
{
    [ApiController]
    [Route("api/tags")]
    public class TagController : ControllerBase
    {
        private readonly ILogger<TagController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const int LAST_LOG_MINUTES = 10;

        public TagController(ILogger<TagController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //Get endpoint for returning all tags registerd
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _unitOfWork.Tags.GetAllTags();
            IList<TagDto> tagsDtos = new List<TagDto>();
            foreach(var tag in tags)
            {
                var category = await _unitOfWork.Category.GetByIdAsync(tag.CategoryId);
                tagsDtos.Add(new TagDto ( tag.EPC, category.Name , tag.Description ));
            };
            return Ok(tagsDtos);
        }

        //Get endpoint for return tag with especific epc
        [HttpGet("{epc}")]
        public async Task<IActionResult> GetTag(string epc)
        {
            if(epc.IsNullOrEmpty())
            {
                return BadRequest($"EPC Field cant be empty and must start with 'EPC'");
            }

            Tag? tag = await _unitOfWork.Tags.GetTagWithEPC(epc);
            if(tag == null)
            {
                return NotFound($"No tag registered with epc: {epc}");
            }
            return Ok(tag);
        }

        //Get endpoint to get absolute position of tag throw logs registered in the last 10 minutes
        [HttpGet("{epc}/position")]
        public async Task<IActionResult> GetPositionFromTagWithEPC(string epc)
        {
            Log? log = await _unitOfWork.Logs.GetLastLogFromTagWithEPC(epc);
            if (log == null)
            {
                return NotFound($"No logs found for tag with epc: {epc}");
            }

            PairAntenna? pairAntenna = await _unitOfWork.PairAntennas.getPairAntennaByIPAddress(log.IPAddress);
            if (pairAntenna == null)
            {
                return NotFound($"No pair antenna found with ipaddress: {log.IPAddress}");
            }

            Room? room = await _unitOfWork.Rooms.getRoomById(pairAntenna.idRoom);
            if (room == null)
            {
                return NotFound($"No room found from pair of antennas: {pairAntenna.antenna01IP} and {pairAntenna.antenna02IP}");
            }

            //Search Logs from IPAddress1
            List<Log> logsFromIPAddress1 = await _unitOfWork.Logs.GetLogsFromIPAddressWithTagEPCInLastXMinutes(pairAntenna.antenna01IP, epc, LAST_LOG_MINUTES);
            if (logsFromIPAddress1.Count == 0 || logsFromIPAddress1 == null)
            {
                return NotFound($"No logs found from IPAddress: {pairAntenna.antenna01IP} in the last {LAST_LOG_MINUTES} minutes");
            }

            //Search Lo IPAddress2
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
                    Id = room.id,
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



        //Post endpoint to register new tag
        [HttpPost]
        public async Task<IActionResult> AddTag(TagResource tagResource)
        {
            var newTag = _mapper.Map<TagResource, Tag>(tagResource);

            Tag? tag = await _unitOfWork.Tags.GetTagWithEPC(tagResource.EPC);
            
            if(tag != null)
            {
                return Conflict("Can't register tag with epc: {epc} because there's already a tag registered with that EPC");
            }

            Category? category = await _unitOfWork.Category.GetByIdAsync(tagResource.CategoryId);

            if(category == null)
            {
                return NotFound($"No category with id: {tagResource.CategoryId} found");
            }

            if (!await _unitOfWork.Tags.AddTag(newTag))
            {
                return StatusCode(500, $"There was an error that prevented the service from inserting tag: {newTag}");
            }

            return CreatedAtAction(nameof(AddTag),new { epc = newTag.EPC}, newTag);
        }

        //Delete endpoint to delete tag and logs associated the tag
        [HttpDelete("{epc}")]
        public async Task<IActionResult> DeleteTag(string epc)
        {
            if (epc.IsNullOrEmpty())
            {
                return BadRequest($"EPC field cant be null or empty");
            }

            Tag? tag = await _unitOfWork.Tags.GetTagWithEPC(epc);
            if(tag == null)
            {
                return NotFound("Tried to delete unregistered tag");
            }

            if(!await _unitOfWork.Logs.DeleteLogsFromTagWithEPC(epc))
            {
                return StatusCode(500, $"There was an error that prevented the service from deleting logs from tag: {tag}");
            }

            if(!await _unitOfWork.TagPositions.DeleteTagPositionsFromTagWithEPC(epc))
            {
                return StatusCode(500, $"There was an error that prevented the service from deleting recorder positions from tag: {tag}");
            }

            if(!await _unitOfWork.Tags.DeleteTag(epc))
            {
                return StatusCode(500, $"There was an error that prevented the service from deleting tag: {tag}");
            }

            return NoContent();
        }
      
    }
}
