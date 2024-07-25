using _3DTrackingProducts.Api.Core.Repositories;
using _3DTrackingProducts.Api.Data;
using _3DTrackingProducts.Api.Models;

namespace _3DTrackingProducts.Api.Persistence.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext context) : base(context) { }

        public async Task<bool> AddRoom(Room room)
        {
            await Add(room);
            return await Save();
        }

        public async Task<bool> DeleteRoom(Guid id)
        {
            Room? room = await getRoomById(id);
            if(room == null)
            {
                return false;
            }
            await Remove(room);
            return await Save();
        }

        public async Task<List<Room>> getAllRooms()
        {
            List<Room> rooms = (List<Room>) await GetAll();
            if(rooms == null)
            {
                rooms = new List<Room>();
            }
            return rooms;
        }

        public async Task<Room?> getRoomById(Guid id)
        {
            return await SingleOrDefault(r => r.id == id);
        }

        public async Task<Room?> getRoomByName(string name)
        {
            return await SingleOrDefault(r => r.Name == name);
        }

        public async Task<bool> UpdateRoom(Guid id, Room room)
        {
            Room? roomToUpdate = await getRoomById(id);
            
            if(roomToUpdate == null)
            {
                return false;
            }

            roomToUpdate.Name = room.Name;
            roomToUpdate.Length = room.Length;
            roomToUpdate.Width = room.Width;
            if (room.imageByte != null)
            {
                roomToUpdate.imageByte = room.imageByte;
            }

            return await Save();
        }
    }
}
