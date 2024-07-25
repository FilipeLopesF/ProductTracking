using System;
using _3DTrackingProducts.Api.Core;
using _3DTrackingProducts.Api.Dtos;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Models.Auth;
using _3DTrackingProducts.Api.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _3DTrackingProducts.Api.Controllers
{
	[ApiController]
	[Route("api")]
	public class Tag3DPositionController : ControllerBase
	{
        private readonly ILogger<LogController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Tag3DPositionController(ILogger<LogController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("tag3D/position")]
        public async Task<IActionResult> PostPosition(Calculate3DPositionResource calculate3DPositionResource)
        {
            var controlTagLeft = (await _unitOfWork.ControlTag.Find(c=>c.EPC == calculate3DPositionResource.EPC_Left)).FirstOrDefault();
            var controlTagRight = (await _unitOfWork.ControlTag.Find(c => c.EPC == calculate3DPositionResource.EPC_Right)).FirstOrDefault();
            var tagUnknown = (await _unitOfWork.Tags.Find(c => c.EPC == calculate3DPositionResource.EPC_Unknown)).FirstOrDefault();

            var d1 = (float)calculate3DPositionResource.Distance_Left;
            var d2 = (float)calculate3DPositionResource.Distance_Right;
            var d3 = (float)calculate3DPositionResource.Distance_Hight;

            if (tagUnknown != null && controlTagLeft != null && controlTagRight != null)
            {
                Tag3DPosition newTag3DPosition = Calculate3DPosition.Get(tagUnknown, controlTagLeft, controlTagRight, d1, d2, d3);
                await _unitOfWork.Tag3DPosition.Add(newTag3DPosition);
                await _unitOfWork.Tag3DPosition.Save();

                Position3DDto position3DDto = new Position3DDto
                {
                    TagX = newTag3DPosition.RelativePosX,
                    TagY = newTag3DPosition.RelativePosY,
                    TagZ = 0
                };
                
                return Ok(position3DDto);
            }



            return NotFound("One or more epc are missing on request.");
        }

        [HttpGet("tag3D/position/{tagEPC}")]
        public async Task<IActionResult> GetPosition(string tagEPC)
        {
            var position3D = (await _unitOfWork.Tag3DPosition.Find(t=>t.TagEPC == tagEPC)).FirstOrDefault();

            if (position3D == null)
            {
                return NotFound($"Position of Tag {tagEPC} was not found.");
            }

            Position3DDto position3DDto = new Position3DDto();
            position3DDto.TagX = position3D.RelativePosX;
            position3DDto.TagY = position3D.RelativePosY;
            position3DDto.TagZ = position3D.RelativePosZ;


            return Ok(position3DDto);

        }
    }
}

