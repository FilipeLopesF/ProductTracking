using System;
using _3DTrackingProducts.Api.Core;
using _3DTrackingProducts.Api.Core.Repositories;
using _3DTrackingProducts.Api.Data;
using _3DTrackingProducts.Api.Persistence.Repositories;

namespace _3DTrackingProducts.Api.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;
        public IUsersRepository Users { get; set; }
        public ITagRepository Tags { get; set; }
        public ILogRepository Logs { get; set; } 
        public ITagPositionRepository TagPositions { get; set; }
        public ICategoryRepository Category { get; set; }
        public IControlTagRepository ControlTag { get; set; }
        public ITag3DPositionRepository Tag3DPosition { get; set; }
        public IRoomRepository Rooms { get; set; }

        public IPairAntennaRepository PairAntennas { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new UsersRepository(_context);
            Tags = new TagRepository(_context);
            Logs = new LogRepository(_context);
            TagPositions = new TagPositionRepository(_context);
            Category = new CategoryRepository(_context);
            ControlTag = new ControlTagRepository(_context);
            Tag3DPosition = new Tag3DPositionRepository(_context);
            Rooms = new RoomRepository(_context);
            PairAntennas = new PairAntennaRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

