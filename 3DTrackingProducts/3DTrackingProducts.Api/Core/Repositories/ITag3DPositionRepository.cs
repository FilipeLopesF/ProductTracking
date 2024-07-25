using System;
using _3DTrackingProducts.Api.Models;

namespace _3DTrackingProducts.Api.Core.Repositories
{
	public interface ITag3DPositionRepository : IRepository<Tag3DPosition>
	{
        Task<List<Tag3DPosition>> GetAllTag3DPositions();

        Task<List<Tag3DPosition>> GetTag3DPositionsFromTagWithEPC(string epc);

        Task<bool> AddTag3DPosition(Tag3DPosition newTagPosition);

        Task<bool> DeleteTag3DPositionsFromTagWithEPC(string epc);
    }
}

