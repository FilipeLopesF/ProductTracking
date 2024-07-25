using _3DTrackingProducts.Api.Core.Repositories;
using _3DTrackingProducts.Api.Data;
using _3DTrackingProducts.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;

namespace _3DTrackingProducts.Api.Persistence.Repositories
{
    
    public class LogRepository : Repository<Log>,ILogRepository
    {
        public LogRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Log>> GetAllLogs()
        {
            List<Log> logs = (List<Log>) await GetAll();
            if (logs == null)
            {
                return new List<Log>();
            }

            return logs;
        }

        public async Task<Log?> GetLogWithId(Guid id)
        {
            return await SingleOrDefault(l => l.Id == id);
        }

        public async Task<List<Log>> GetLogsFromTagWithEPC(string epc)
        {
            List<Log> logs = (List<Log>) await Find(l => l.TagEPC == epc);
            if (logs == null)
            {
                return new List<Log>();
            }
            return logs;
        }

        public async Task<List<Log>> GetLogsFromIPAddressWithTagEPC(string ipaddress, string epc)
        {

            List<Log> logs = (List<Log>)await Find(t => t.IPAddress == ipaddress && t.TagEPC == epc);
            if (logs == null)
            {
                return new List<Log>();
            }
            return logs;
        }

        public async Task<bool> AddLog(Log newLog)
        {
            _ = Add(newLog);
            return await Save();
        }

        public async Task<bool> DeleteLog(Guid id)
        {
            Log? log = await GetLogWithId(id);
            if(log == null)
            {
                return false;
            }

            await Remove(log);
            return await Save();
        }

        public async Task<bool> DeleteLogsFromTagWithEPC(string epc)
        {
            List<Log> logs = await GetLogsFromTagWithEPC(epc);

            foreach (Log log in logs)
            {
                await Remove(log);
            }

            return logs.Count > 0 ? await Save() : true;
        }

        public async Task<List<string>> GetIPAddressThatRegisteredTagWithEPC(string epc)
        {
            List<string> ipAdresses = await _context.Logs.Where(l => l.TagEPC == epc).Select(l => l.IPAddress).Distinct().ToListAsync();
            if(ipAdresses == null)
            {
                return new List<string>();
            }

            return ipAdresses;
        }

        public async Task<List<Log>> GetLogsFromIPAddressWithTagEPCInLastXMinutes(string ipaddress, string epc, int x)
        {
            DateTime timeStampMinusXMinutes = DateTime.Now.AddMinutes(-x);
            List<Log> logs = (List<Log>) await Find(t => t.IPAddress == ipaddress && t.TagEPC == epc && t.Timestamp >= timeStampMinusXMinutes);
            if (logs == null)
            {
                return new List<Log>();
            }
            return logs;
        }

        public async Task<Log?> GetLastLogFromTagWithEPC(string epc)
        {
            return await LastOrDefault(l => l.TagEPC == epc, l=> l.Timestamp);
        }
    }
}
