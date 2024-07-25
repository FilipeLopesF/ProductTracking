using _3DTrackingProducts.Api.Models;

namespace _3DTrackingProducts.Api.Core.Repositories
{
    public interface ILogRepository : IRepository<Log>
    {
        Task<List<Log>> GetAllLogs();

        Task<Log?> GetLogWithId(Guid id);

        Task<List<Log>> GetLogsFromTagWithEPC(string epc);

        Task<List<string>> GetIPAddressThatRegisteredTagWithEPC(string epc);

        Task<List<Log>> GetLogsFromIPAddressWithTagEPC(string ipaddress, string epc);

        Task<Log?> GetLastLogFromTagWithEPC(string epc);

        Task<List<Log>> GetLogsFromIPAddressWithTagEPCInLastXMinutes(string ipaddress, string epc, int x);

        Task<bool> AddLog(Log newLog);

        Task<bool> DeleteLog(Guid id);
        Task<bool> DeleteLogsFromTagWithEPC(string epc);
    }
}
