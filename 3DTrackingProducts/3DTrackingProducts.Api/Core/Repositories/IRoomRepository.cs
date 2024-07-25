using _3DTrackingProducts.Api.Models;

namespace _3DTrackingProducts.Api.Core.Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        public Task<List<Room>> getAllRooms();

        public Task<Room?> getRoomById(Guid id);

        public Task<Room?> getRoomByName(string name);

        public Task<bool> AddRoom(Room room);

        public Task<bool> UpdateRoom(Guid id, Room room);

        public Task<bool> DeleteRoom(Guid id);
    }
}
