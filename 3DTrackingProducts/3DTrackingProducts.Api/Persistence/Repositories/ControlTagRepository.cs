using System;
using _3DTrackingProducts.Api.Core.Repositories;
using _3DTrackingProducts.Api.Data;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Models.Auth;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace _3DTrackingProducts.Api.Persistence.Repositories
{
    public class ControlTagRepository : Repository<ControlTag>, IControlTagRepository
    {
        public ControlTagRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ControlTag?> GetControlTagByEPC(string epc)
        {
            return await SingleOrDefault(ct => ct.EPC == epc);
        }

        public async Task<List<ControlTag>> GetControlTagsByRoomAsync(Guid roomId)
        {
            List<ControlTag> controlTagsByRoom = (List<ControlTag>) await Find(ct=>ct.RoomId == roomId);

            return controlTagsByRoom;
        }

        public async Task<ControlTag> UpdateControlTagAsync(string epc, ControlTag controlTag)
        {
            ControlTag? controlTagInDB = await SingleOrDefault(ct => ct.EPC == epc);
            if (controlTagInDB == null)
            {
                return null;
            }
            controlTagInDB.PositionX = controlTag.PositionX;
            controlTagInDB.PositionY = controlTag.PositionY;
            controlTagInDB.Description = controlTag.Description;
            await Save();

            return controlTagInDB;
        }
    }
}

