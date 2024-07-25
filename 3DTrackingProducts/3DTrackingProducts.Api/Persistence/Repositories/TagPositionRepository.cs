using _3DTrackingProducts.Api.Core.Repositories;
using _3DTrackingProducts.Api.Data;
using _3DTrackingProducts.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace _3DTrackingProducts.Api.Persistence.Repositories
{
    public class TagPositionRepository : Repository<TagPosition>, ITagPositionRepository
    {
        public TagPositionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<TagPosition>> GetAllTagPositions()
        {
            List<TagPosition> tagPositions = (List<TagPosition>) await GetAll();
            if(tagPositions == null)
            {
                return new List<TagPosition>();
            }

            return tagPositions;
        }

        public async Task<List<TagPosition>> GetTagPositionsFromTagWithEPC(string epc)
        {
            List<TagPosition> tagPositions = (List<TagPosition>) await Find(tp => tp.TagEPC == epc);
            if (tagPositions == null)
            {
                return new List<TagPosition>();
            }

            return tagPositions;
        }

        public async Task<bool> AddTagPosition(TagPosition newTagPosition)
        {
            await Add(newTagPosition);
            return await Save();
        }

        public async Task<bool> DeleteTagPositionsFromTagWithEPC(string epc)
        {
            List<TagPosition> tagPositions = await GetTagPositionsFromTagWithEPC(epc);
            foreach(TagPosition tagPosition in tagPositions)
            {
                await Remove(tagPosition);

            }

            return tagPositions.Count > 0 ? await Save() : true;
        }

        public async Task<TagPosition?> GetLastTagPositionsFromTagWithEPC(string epc)
        {
            return await LastOrDefault(tp => tp.TagEPC == epc,tp => tp.Timestamp);

        }
    }
}
