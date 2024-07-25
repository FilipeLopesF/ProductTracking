using _3DTrackingProducts.Api.Core.Repositories;
using _3DTrackingProducts.Api.Data;
using _3DTrackingProducts.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace _3DTrackingProducts.Api.Persistence.Repositories
{
    
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<Tag>> GetAllTags()
        {
            List<Tag> tags = (List<Tag>) await GetAll();
            if (tags == null)
            {
                return new List<Tag>();
            }

            return tags;

        }

        public async Task<Tag?> GetTagWithEPC(string epc)
        {
            return await SingleOrDefault(t => t.EPC == epc);
        }

        public async Task<bool> AddTag(Tag newTag)
        {
            await Add(newTag);
            return await Save();
        }

        public async Task<bool> DeleteTag(string epc)
        {

            Tag? tag = await GetTagWithEPC(epc);

            if(tag == null)
            {
                return false;
            }

            await Remove(tag);
            return await Save();
            
        }

    }
}
