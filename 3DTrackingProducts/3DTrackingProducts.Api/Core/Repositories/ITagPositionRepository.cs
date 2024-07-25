using _3DTrackingProducts.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace _3DTrackingProducts.Api.Core.Repositories
{
    public interface ITagPositionRepository : IRepository<TagPosition>
    {
        Task<List<TagPosition>> GetAllTagPositions();

        Task<List<TagPosition>> GetTagPositionsFromTagWithEPC(string epc);

        Task<TagPosition?> GetLastTagPositionsFromTagWithEPC(string epc);

        Task<bool> AddTagPosition(TagPosition newTagPosition);

        Task<bool> DeleteTagPositionsFromTagWithEPC(string epc);
    }
}
