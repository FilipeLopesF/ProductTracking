using _3DTrackingProducts.Api.Models;

namespace _3DTrackingProducts.Api.Core.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        public Task<List<Tag>> GetAllTags();

        public Task<Tag?> GetTagWithEPC(string epc);

        public Task<bool> AddTag(Tag newTag);

        public Task<bool> DeleteTag(string epc);
    }
}
