using _3DTrackingProducts.Api.Models;

namespace _3DTrackingProducts.Api.Core.Repositories
{
    public interface IPairAntennaRepository : IRepository<PairAntenna>
    {
        public Task<List<PairAntenna>> getAllPairAntennas();

        public Task<List<PairAntenna>> getPairAntennasByRoomId(Guid id);

        public Task<PairAntenna?> getPairAntennaById(Guid id);

        public Task<PairAntenna?> getPairAntennaByIPAddress(string ipAddress);

        public Task<bool> AddPairAntenna(PairAntenna pairAntenna);

        public Task<bool> UpdatePairAntenna(Guid id,PairAntenna pairAntenna);

        public Task<bool> DeletePairAntenna(Guid id);

    }
}
