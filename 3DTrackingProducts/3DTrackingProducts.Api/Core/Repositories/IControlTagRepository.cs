using System;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Models.Auth;

namespace _3DTrackingProducts.Api.Core.Repositories
{
	public interface IControlTagRepository : IRepository<ControlTag>
	{
        Task<ControlTag?> GetControlTagByEPC(string EPC);
        Task<ControlTag> UpdateControlTagAsync(string epc, ControlTag controlTag);
        Task<List<ControlTag>> GetControlTagsByRoomAsync(Guid roomId);
    }
}

