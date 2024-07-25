using _3DTrackingProducts.Api.Models;

namespace _3DTrackingProducts.Api.Utils
{
    public class LogUtils
    {
        public static List<Log> filterLogs(List<Log> logs)
        {
            return logs.Where(l => (DateTime.Now - l.Timestamp).Minutes == 10).ToList();
        }

        public static List<List<Log>> filterLogsByIPAddress(List<Log> logs)
        {
            List<List<Log>> logsByIpAddress = new List<List<Log>>();
            List<string> ipAddress = logs.Select(l => l.TagEPC).Distinct().ToList();

            foreach (string ip in ipAddress)
            {
                List<Log> listLog = logs.Where(l => l.IPAddress == ip).ToList();
                logsByIpAddress.Add(filterLogs(listLog));
            }

            return logsByIpAddress;
        }
    }
}
