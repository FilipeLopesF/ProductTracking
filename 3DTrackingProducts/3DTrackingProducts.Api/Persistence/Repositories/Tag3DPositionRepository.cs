using _3DTrackingProducts.Api.Core.Repositories;
using _3DTrackingProducts.Api.Data;
using _3DTrackingProducts.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace _3DTrackingProducts.Api.Persistence.Repositories
{
    public class Tag3DPositionRepository : Repository<Tag3DPosition>, ITag3DPositionRepository
    {
        public Tag3DPositionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Tag3DPosition>> GetAllTag3DPositions()
        {
            List<Tag3DPosition> tagPositions = (List<Tag3DPosition>) await GetAll();
            if(tagPositions == null)
            {
                return new List<Tag3DPosition>();
            }

            return tagPositions;
        }

        public async Task<List<Tag3DPosition>> GetTag3DPositionsFromTagWithEPC(string epc)
        {
            List<Tag3DPosition> tagPositions = (List<Tag3DPosition>) await Find(tp => tp.TagEPC == epc);
            if (tagPositions == null)
            {
                return new List<Tag3DPosition>();
            }

            return tagPositions;
        }

        public async Task<bool> AddTag3DPosition(Tag3DPosition newTag3DPosition)
        {
            await Add(newTag3DPosition);
            return await Save();
        }

        public async Task<bool> DeleteTag3DPositionsFromTagWithEPC(string epc)
        {
            List<Tag3DPosition> tag3DPositions = await GetTag3DPositionsFromTagWithEPC(epc);
            foreach (var tag3DPosition in tag3DPositions)
            {
               await Remove(tag3DPosition);
            }
            
            return await Save();
        }

    }
}
