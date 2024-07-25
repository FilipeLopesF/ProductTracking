using _3DTrackingProducts.Api.Core;
using _3DTrackingProducts.Api.Dtos;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace _3DTrackingProducts.Api.Controllers
{
    [ApiController]
    [Route("api/tags/positions")]
    public class TagPositionController : ControllerBase
    {

        private readonly ILogger<TagPositionController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TagPositionController(ILogger<TagPositionController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTagPositions()
        {
            return Ok(await _unitOfWork.TagPositions.GetAllTagPositions());
        }

        [HttpGet("{epc}")]
        public async Task<IActionResult> GetTagPositionsFromTagWithEPC(string epc)
        {
            if (epc.IsNullOrEmpty() || !epc.StartsWith("EPC"))
            {
                return BadRequest($"EPC Field cant be empty and must start with 'EPC'");
            }

            List<TagPosition> tagPositions = await _unitOfWork.TagPositions.GetTagPositionsFromTagWithEPC(epc);
            if (tagPositions.Count == 0 || tagPositions == null )
            {
                return NotFound($"No tag position registered from tag with epc: {epc}");
            }
            return Ok(tagPositions);
        }

        [HttpGet("{epc}/last")]
        public async Task<IActionResult> GetLastTagPositionFromTagWithEPC(string epc)
        {
            if (epc.IsNullOrEmpty())
            {
                return BadRequest($"EPC Field cant be empty");
            }

            TagPosition? tagPosition = await _unitOfWork.TagPositions.GetLastTagPositionsFromTagWithEPC(epc);
            if (tagPosition == null)
            {
                return NotFound($"No tag position registered from tag with epc: {epc}");
            }

            PairAntenna? pairAntenna = await _unitOfWork.PairAntennas.getPairAntennaById(tagPosition.PairAntennaId);
            if(pairAntenna == null) 
            {
                return NotFound($"No pair antenna found with id: {tagPosition.PairAntennaId}");
            }

            Room? room = await _unitOfWork.Rooms.getRoomById(pairAntenna.idRoom);
            if(room == null)
            {
                return NotFound($"No room found from pair of antennas: {pairAntenna.antenna01IP} and {pairAntenna.antenna02IP}");
            }

            CalculatePositionDto positionDto = new CalculatePositionDto();
            positionDto.locX = tagPosition.x;
            positionDto.locY = tagPosition.y;
            positionDto.room = new RoomDto
            {
                Id = room.id,
                Name = room.Name,
                Width = room.Width,
                Length = room.Length,
                imageByte = room.imageByte
            };
            positionDto.pairAntenna = new PairAntennaDto
            {
                Id = pairAntenna.Id,
                antenna01IP = pairAntenna.antenna01IP,
                antenna01X = pairAntenna.antenna01X,
                antenna01Y = pairAntenna.antenna01Y,
                antenna02IP = pairAntenna.antenna02IP,
                antenna02X = pairAntenna.antenna02X,
                antenna02Y = pairAntenna.antenna02Y
            };
            
            return Ok(positionDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddTagPosition(TagPositionResource tagPosition)
        {
            var newTagPosition = _mapper.Map<TagPositionResource, TagPosition>(tagPosition);

            Tag? tag = await _unitOfWork.Tags.GetTagWithEPC(newTagPosition.TagEPC);

            if (tag == null)
            {
                return NotFound($"Can't register tag position with epc: {tagPosition.TagEPC} because there's is no tag registered with that EPC");
            }

            PairAntenna? pairAntenna = await _unitOfWork.PairAntennas.getPairAntennaById(newTagPosition.PairAntennaId);
            if(pairAntenna == null)
            {
                return NotFound($"Can't register tag position with epc: {tagPosition.TagEPC} because there's is no pairAntenna registered with that Id");
            }

            if(!await _unitOfWork.TagPositions.AddTagPosition(newTagPosition))
            {
                return StatusCode(500, $"There was an error that prevented the service from inserting new tag position: {newTagPosition}");
            }

            return CreatedAtAction(nameof(AddTagPosition), new { epc = tagPosition.TagEPC }, tagPosition);
        }

        [HttpDelete("{epc}")]
        public async Task<IActionResult> DeleteTagPositionsFromTagWithEPC(string epc)
        {
            if (epc.IsNullOrEmpty() || !epc.StartsWith("EPC"))
            {
                return BadRequest($"EPC field cant be empty and must start with 'EPC'");
            }

            List<TagPosition> tagPositions = await _unitOfWork.TagPositions.GetTagPositionsFromTagWithEPC(epc);
            if (tagPositions == null || tagPositions.Count == 0)
            {
                return NotFound($"No tag position registered from tag with epc: {epc}");
            }

            if (!await _unitOfWork.TagPositions.DeleteTagPositionsFromTagWithEPC(epc))
            {
                return StatusCode(500, $"There was an error that prevented the service from deleting the recorded positions from tag with epc: {epc}");
            }

            return NoContent();
        }

    }
}
