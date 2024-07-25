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

namespace _3DTrackingProducts.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LogController(ILogger<LogController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        //Get endpoint to return all records in logs table
        [HttpGet("logs/all")]
        public async Task<IActionResult> GetAllLogs()
        {
            List<Log> logs = await _unitOfWork.Logs.GetAllLogs();
            return Ok(logs);
        }

        //Get endpoint to return records from log table from tag with especific epc
        [HttpGet("logs/{id}")]
        public async Task<IActionResult> GetLogWithId (Guid id)
        {
            Log? log = await _unitOfWork.Logs.GetLogWithId(id);
            if(log == null)
            {
                return NotFound($"No log with {id} found");
            }
            return Ok(log);
        }

        //Get endpoint to return records from log table from tag with especific epc
        [HttpGet("tags/{epc}/logs")]
        public async Task<IActionResult> GetLogsWithEPC (string epc)
        {
            List<Log> epcLogs = await _unitOfWork.Logs.GetLogsFromTagWithEPC(epc);
            if(epcLogs.Count == 0 || epcLogs == null)
            {
                return NotFound($"Tag with {epc} doesn't have any logs");
            }
            return Ok(epcLogs);
        }

        [HttpGet("tags/{epc}/logs/ips")]
        public async Task<IActionResult> GetIPAddresses(string epc)
        {
            List<string> ipAddresses = await _unitOfWork.Logs.GetIPAddressThatRegisteredTagWithEPC(epc);
            return Ok(ipAddresses);
        }

        //Get endpoint to return records from log table from tag with especific epc and from especific ipadress (reader)
        [HttpGet("tags/{epc}/logs/ips/{ipaddress}")]
        public async Task<IActionResult> GetLogsWithEPCFromIPAdress(string epc, string ipaddress)
        {
            List<Log> logs = await _unitOfWork.Logs.GetLogsFromIPAddressWithTagEPC(ipaddress, epc);
            if (logs.Count == 0 || logs == null)
            {
                return NotFound($"No logs found from IPAddress: {ipaddress}");
            }

            return Ok(logs);
        }

        [HttpPost("logs")]
        public async Task<IActionResult> AddLog(LogResource logResource)
        {
            var log = _mapper.Map<LogResource, Log>(logResource);

            Tag? tag = await _unitOfWork.Tags.GetTagWithEPC(log.TagEPC);
            if(tag == null)
            {
                return NotFound($"Can't insert log from non registered tag: {log.TagEPC}");
            }

            PairAntenna? pairAntenna = await _unitOfWork.PairAntennas.getPairAntennaByIPAddress(log.IPAddress);
            if(pairAntenna == null)
            {
                return NotFound($"Can't insert log from non registered antenna IP: {log.IPAddress}");
            }

            if(!await _unitOfWork.Logs.AddLog(log))
            {
                return StatusCode(500, $"There was an error that prevented the service from inserting log: {log}");
            }

            var response = CreatedAtAction(nameof(AddLog), new {id = log.Id}, log);

            return response;
        }

        [HttpDelete("logs/{id}")]
        public async Task<IActionResult> DeleteLogWithId(Guid id)
        {
            Log? log = await _unitOfWork.Logs.GetLogWithId(id);
            if (log == null)
            {
                return NotFound($"No log with {id} found");
            }

            if (!await _unitOfWork.Logs.DeleteLog(id))
            {
                return StatusCode(500, $"There was an error that prevented the service from deleting log with id: {id}");
            }

            return NoContent();
        }

        //Delete endpoint to delete records from log table from tag with especific epc
        [HttpDelete("tags/{epc}/logs")]
        public async Task<IActionResult> DeleteLogsWithEPC(string epc)
        {
            List<Log> epcLogs = await _unitOfWork.Logs.GetLogsFromTagWithEPC(epc);
            if (epcLogs.Count == 0 || epcLogs == null)
            {
                return NotFound($"Tag with {epc} doesn't have any logs");
            }

            if (!await _unitOfWork.Logs.DeleteLogsFromTagWithEPC(epc))
            {
                return StatusCode(500, $"There was an error that prevented the service from deleting logs from tag with EPC: {epc}");
            }

            return NoContent();
        }

    }
}
