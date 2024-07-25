using _3DTrackingProducts.Api.Core.Repositories;
using _3DTrackingProducts.Api.Data;
using _3DTrackingProducts.Api.Models;

namespace _3DTrackingProducts.Api.Persistence.Repositories
{
    public class PairAntennaRepository : Repository<PairAntenna>, IPairAntennaRepository
    {
        public PairAntennaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> AddPairAntenna(PairAntenna pairAntenna)
        {
            await Add(pairAntenna);
            return await Save();
        }

        public async Task<bool> DeletePairAntenna(Guid id)
        {
            PairAntenna? pairAntenna = await getPairAntennaById(id);
            if(pairAntenna == null)
            {
                return false;
            }

            await Remove(pairAntenna);
            return await Save();
        }

        public async Task<List<PairAntenna>> getAllPairAntennas()
        {
            List<PairAntenna> pairAntennas = (List<PairAntenna>) await GetAll();
            if(pairAntennas == null)
            {
                pairAntennas = new List<PairAntenna>();
            }
            return pairAntennas;
        }

        public async Task<PairAntenna?> getPairAntennaById(Guid id)
        {
            return await SingleOrDefault(pa => pa.Id == id);
        }

        public async Task<PairAntenna?> getPairAntennaByIPAddress(string ipAddress)
        {
            return await SingleOrDefault(pa => pa.antenna01IP == ipAddress || pa.antenna02IP == ipAddress); 
        }

        public async Task<List<PairAntenna>> getPairAntennasByRoomId(Guid id)
        {
            List<PairAntenna> pairAntennas = (List<PairAntenna>)await Find(pa => pa.idRoom == id);
            if(pairAntennas == null)
            {
                pairAntennas = new List<PairAntenna>();
            }
            return pairAntennas;
        }

        public async Task<bool> UpdatePairAntenna(Guid id,PairAntenna pairAntenna)
        {
            PairAntenna? pairAntennaToUpdate = await getPairAntennaById(id);

            if(pairAntennaToUpdate == null)
            {
                return false;
            }

            pairAntennaToUpdate.antenna01IP = pairAntenna.antenna01IP;
            pairAntennaToUpdate.antenna02IP = pairAntenna.antenna02IP;
            pairAntennaToUpdate.antenna01X = pairAntenna.antenna01X;
            pairAntennaToUpdate.antenna01Y = pairAntenna.antenna01Y;
            pairAntennaToUpdate.antenna02X = pairAntenna.antenna02X;
            pairAntennaToUpdate.antenna02Y = pairAntenna.antenna02Y;
            pairAntennaToUpdate.DetectingState = pairAntenna.DetectingState;
            pairAntennaToUpdate.LastVerificationTimeStamp = pairAntenna.LastVerificationTimeStamp;

            return await Save();

        }
    }
}
